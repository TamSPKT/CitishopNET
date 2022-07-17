using AutoMapper;
using CitishopNET.Business.Extensions;
using CitishopNET.Business.Repository;
using CitishopNET.DataAccess.Enums;
using CitishopNET.DataAccess.Models;
using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.Invoice;
using CitishopNET.Shared.EnumDtos;
using CitishopNET.Shared.MomoDtos;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CitishopNET.Business.Services
{
	public class InvoiceService : IInvoiceService
	{
		private readonly IBaseRepository<Invoice> _invoiceRepository;
		private readonly IBaseRepository<Product> _productRepository;
		private readonly IBaseRepository<InvoiceProduct> _invoiceProductRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMomoService _momoService;

		public InvoiceService(IBaseRepository<Invoice> invoiceRepository, IBaseRepository<Product> productRepository, IBaseRepository<InvoiceProduct> invoiceProductRepository, IMapper mapper, UserManager<ApplicationUser> userManager, IMomoService momoService)
		{
			_invoiceRepository = invoiceRepository;
			_productRepository = productRepository;
			_invoiceProductRepository = invoiceProductRepository;
			_mapper = mapper;
			_userManager = userManager;
			_momoService = momoService;
		}

		public async Task<PagedModel<InvoiceDto>> GetInvoicesAsync(InvoiceQueryCriteria criteria)
		{
			var query = _invoiceRepository.Entities.AsNoTracking()
				.Where(x => x.UserId == criteria.UserId || criteria.IgnoreUserId)
				.OrderByDescending(x => x.DateOrdered);
			var dtoQuery = query.Select(x => _mapper.Map<InvoiceDto>(x));
			var pagedInvoices = await dtoQuery.PaginateAsync(criteria.Page, criteria.Limit);
			return pagedInvoices;
		}

		public async Task<InvoiceDetailDto?> GetInvoiceByIdAsync(Guid id)
		{
			var query = _invoiceRepository.Entities.Include(x => x.InvoiceProducts)
				.ThenInclude(x => x.Product)
				.AsNoTracking();
			var invoice = await query.SingleOrDefaultAsync(x => x.Id == id);
			return invoice != null
				? _mapper.Map<InvoiceDetailDto>(invoice)
				: null;
		}

		public async Task<InvoiceFullDetailDto?> GetFullInvoiceByIdAsync(Guid id)
		{
			var query = _invoiceRepository.Entities.Include(x => x.InvoiceProducts)
				.ThenInclude(x => x.Product)
				.Include(x => x.User)
				.AsNoTracking();
			var invoice = await query.SingleOrDefaultAsync(x => x.Id == id);
			return invoice != null
				? _mapper.Map<InvoiceFullDetailDto>(invoice)
				: null;
		}

		public async Task<(PaymentStatusDto, object)> AddAsync(CreateInvoiceDto dto, string returnUrl, string notifyUrl)
		{
			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null)
			{
				return (PaymentStatusDto.Failed, new ArgumentNullException(nameof(user), "User does not exist"));
			}
			var cartProductsIds = dto.CartItems.Select(x => x.ProductId);
			var products = await _productRepository.Entities.Where(x => cartProductsIds.Contains(x.Id)).ToListAsync();

			if (products.Count != cartProductsIds.Count())
			{
				return (PaymentStatusDto.Failed, new Exception("Cart Items have duplicated or non-existed products"));
			}
			if (dto.TotalCost != products.Sum(x => x.DiscountPrice ?? x.Price))
			{
				return (PaymentStatusDto.Failed, new Exception("Total Cost not match sum of Product Prices"));
			}
			List<bool> validBuyAmounts = products.Join(dto.CartItems,
				product => product.Id, cartItem => cartItem.ProductId,
				(product, cartItem) => product.Quantity >= cartItem.Quantity && cartItem.Quantity > 0)
				.ToList();

			if (!validBuyAmounts.TrueForAll(x => x))
			{
				return (PaymentStatusDto.Failed, new Exception("Invalid purchase amount for Cart Items"));
			}
			products = products.Join(dto.CartItems,
				product => product.Id, cartItem => cartItem.ProductId,
				(product, cartItem) =>
				{
					product.Quantity -= cartItem.Quantity;
					return product;
				}).ToList();

			_productRepository.Entities.UpdateRange(products);
			await _productRepository.SaveChangesAsync();

			var invoiceProducts = products.Join(dto.CartItems,
				product => product.Id, cartItem => cartItem.ProductId,
				(product, cartItem) => new InvoiceProduct
				{
					ProductId = product.Id,
					CostPerItem = product.DiscountPrice ?? product.Price,
					Amount = cartItem.Quantity,
				}).ToList();

			var invoice = _mapper.Map<Invoice>(dto);
			invoice.UserId = user.Id;
			invoice.InvoiceProducts = invoiceProducts;
			await _invoiceRepository.AddAsync(invoice);

			return invoice.PaymentType switch
			{
				PaymentType.OnDelivery => (PaymentStatusDto.Waiting, _mapper.Map<InvoiceDto>(invoice)),
				PaymentType.MomoWallet => await SendMomoPaymentRequest(invoice, returnUrl: returnUrl, notifyUrl: notifyUrl),
				_ => (PaymentStatusDto.Failed, new Exception("Unknown Payment Type")),
			};
		}

		public async Task<InvoiceDto?> UpdateAsync(Guid id, EditInvoiceDto dto)
		{
			var invoice = await _invoiceRepository.Entities.SingleOrDefaultAsync(x => x.Id == id);
			if (invoice == null)
			{
				return null;
			}
			invoice = _mapper.Map<EditInvoiceDto, Invoice>(dto, invoice);
			switch (dto.DeliveryStatus)
			{
				case DeliveryStatusDto.Delivered:
					invoice.DateDelivered = DateTime.UtcNow;
					if (invoice.PaymentType == PaymentType.OnDelivery)
					{
						invoice.PaymentStatus = PaymentStatus.Succeeded;
					}
					break;
				case DeliveryStatusDto.Cancelled:
					if (invoice.PaymentType == PaymentType.OnDelivery)
					{
						invoice.PaymentStatus = PaymentStatus.Failed;
					}
					break;
				default:
					invoice.DateDelivered = null;
					if (invoice.PaymentType == PaymentType.OnDelivery)
					{
						invoice.PaymentStatus = PaymentStatus.Waiting;
					}
					break;
			}
			await _invoiceRepository.UpdateAsync(invoice);
			return _mapper.Map<InvoiceDto>(invoice);
		}

		public async Task<bool> UpdatePaymentStatusAsync(Guid id, PaymentStatusDto dto)
		{
			var invoice = await _invoiceRepository.Entities.FindAsync(id);
			if (invoice == null)
			{
				return false;
			}
			invoice.PaymentStatus = (PaymentStatus)(int)dto;
			return await _invoiceRepository.UpdateAsync(invoice);
		}

		public async Task<InvoiceDto?> DeleteAsync(Guid id)
		{
			var invoice = await _invoiceRepository.Entities.SingleOrDefaultAsync(x => x.Id == id);
			if (invoice == null)
			{
				return null;
			}
			await _invoiceRepository.DeleteAsync(invoice);
			return _mapper.Map<InvoiceDto>(invoice);
		}

		private async Task<(PaymentStatusDto, object)> SendMomoPaymentRequest(Invoice invoice, string returnUrl, string notifyUrl)
		{
			var paymentRequest = _mapper.Map<MomoPaymentRequestDto>(invoice);
			paymentRequest.ReturnUrl = returnUrl;
			paymentRequest.NotifyUrl = notifyUrl;

			// TODO: Testing Momo Payment
			// paymentRequest.Amount = "1000"; // Testing

			//await Task.Delay(250);
			//throw new NotImplementedException("Temporary prevent payment using Momo");
			var paymentResponse = await _momoService.SendPaymentRequestAsync(paymentRequest);
			return paymentResponse != null
				? (PaymentStatusDto.Waiting, paymentResponse)
				: (PaymentStatusDto.Failed, new Exception("Unknown Momo Response"));
		}
	}
}
