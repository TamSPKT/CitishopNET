using CitishopNET.Business.Repository;
using CitishopNET.DataAccess.Enums;
using CitishopNET.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CitishopNET.Business.Services
{
	public class ReportService : IReportService
	{
		private readonly IBaseRepository<ApplicationUser> _userRepository;
		private readonly IBaseRepository<Invoice> _invoiceRepository;

		public ReportService(IBaseRepository<ApplicationUser> userRepository, IBaseRepository<Invoice> invoiceRepository)
		{
			_userRepository = userRepository;
			_invoiceRepository = invoiceRepository;
		}

		public async Task<List<decimal>> GetAnalyticsDataAsync()
		{
			var list = await _invoiceRepository.Entities.AsNoTracking()
				.Where(x => x.PaymentStatus == PaymentStatus.Succeeded && EF.Functions.DateDiffMonth(x.DateOrdered, DateTime.UtcNow) == 0)
				.GroupBy(x => x.DateOrdered.Day)
				.Select(g => new { Day = g.Key, TotalPayment = g.Sum(x => x.TotalCost + x.TotalFee - x.Discount) })
				.OrderBy(x => x.Day)
				.ToListAsync();

			//list.ForEach(x => Console.WriteLine($"{x.Day}, {x.TotalPayment}"));
			var result = new List<decimal>();
			for (int i = 1; i <= DateTime.UtcNow.Day; i++)
			{
				result.Add(list.Find(x => x.Day == i)?.TotalPayment ?? 0);
			}
			return result;
		}

		public async Task<List<decimal>> GetDashboardReportAsync()
		{
			var totalRevenue = await _invoiceRepository.Entities.AsNoTracking()
				.Where(x => x.PaymentStatus == PaymentStatus.Succeeded && EF.Functions.DateDiffMonth(x.DateOrdered, DateTime.UtcNow) == 0)
				.SumAsync(x => x.TotalCost + x.TotalFee - x.Discount);
			var totalUsers = await _userRepository.Entities.AsNoTracking()
				.CountAsync();
			var totalInvoices = await _invoiceRepository.Entities.AsNoTracking()
				.CountAsync();
			var totalInvoicesByType = await _invoiceRepository.Entities.AsNoTracking()
				.GroupBy(x => x.PaymentStatus)
				.Select(g => new { PaymentStatus = g.Key, Count = g.Count() })
				.ToListAsync();

			var waitingInvoices = totalInvoicesByType.Find(x => x.PaymentStatus == PaymentStatus.Waiting)?.Count ?? 0;
			var succeededInvoices = totalInvoicesByType.Find(x => x.PaymentStatus == PaymentStatus.Succeeded)?.Count ?? 0;
			var failedInvoices = totalInvoicesByType.Find(x => x.PaymentStatus == PaymentStatus.Failed)?.Count ?? 0;

			return new List<decimal> { totalRevenue, totalUsers, totalInvoices, waitingInvoices, succeededInvoices, failedInvoices };
		}

		public async Task<List<decimal>> GetUserReportAsync(string email)
		{
			var user = await _userRepository.Entities.AsNoTracking().Where(x => x.Email == email).SingleOrDefaultAsync();
			if (user == null)
			{
				return new List<decimal> { 0, 0, 0, 0 };
			}

			var totalInvoices = await _invoiceRepository.Entities.AsNoTracking()
				.Where(x => x.UserId == user.Id)
				.CountAsync();
			var totalInvoicesByType = await _invoiceRepository.Entities.AsNoTracking()
				.Where(x => x.UserId == user.Id)
				.GroupBy(x => x.DeliveryStatus)
				.Select(g => new { DeliveryStatus = g.Key, Count = g.Count() })
				.ToListAsync();

			var waitingInvoices = totalInvoicesByType.Find(x => x.DeliveryStatus == DeliveryStatus.WaitingForConfirmation)?.Count ?? 0;
			var deliveringInvoices = totalInvoicesByType.Find(x => x.DeliveryStatus == DeliveryStatus.InDelivery)?.Count ?? 0;
			var deliveredInvoices = totalInvoicesByType.Find(x => x.DeliveryStatus == DeliveryStatus.Delivered)?.Count ?? 0;

			// Tích luỹ
			var totalPayments = await _invoiceRepository.Entities.AsNoTracking()
				.Where(x => x.UserId == user.Id && x.PaymentStatus == PaymentStatus.Succeeded)
				.SumAsync(x => x.TotalCost + x.TotalFee - x.Discount);

			return new List<decimal> { totalInvoices, waitingInvoices, deliveringInvoices, deliveredInvoices, totalPayments };
		}
	}
}
