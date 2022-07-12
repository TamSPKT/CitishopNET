using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using CitishopNET.DataAccess.Enums;
using CitishopNET.DataAccess.Models;
using CitishopNET.Shared.Dtos.ApplicationUser;
using CitishopNET.Shared.Dtos.Category;
using CitishopNET.Shared.Dtos.Invoice;
using CitishopNET.Shared.Dtos.Product;
using CitishopNET.Shared.Dtos.UserDeliveryAddress;
using CitishopNET.Shared.EnumDtos;
using CitishopNET.Shared.MomoDtos;

namespace CitishopNET.Business
{
	internal class AutoMapperProfile : AutoMapper.Profile
	{
		public AutoMapperProfile()
		{
			FromPresentationLayer();
			FromDataAccessLayer();
		}

		private void FromPresentationLayer()
		{
			CreateMap<CreateCategoryDto, Category>(MemberList.None) // <Source, Dest>
				.ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));

			CreateMap<EditCategoryDto, Category>(MemberList.None) // <Source, Dest>
				.ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));

			CreateMap<CreateProductDto, Product>(MemberList.None) // <Source, Dest>
				.ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
				.ForMember(dst => dst.DiscountPrice, opt => opt.MapFrom(src => src.DiscountPrice))
				.ForMember(dst => dst.Quantity, opt => opt.MapFrom(src => src.Quantity))
				.ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

			CreateMap<EditProductDto, Product>(MemberList.None) // <Source, Dest>
				.ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
				.ForMember(dst => dst.DiscountPrice, opt => opt.MapFrom(src => src.DiscountPrice))
				.ForMember(dst => dst.Quantity, opt => opt.MapFrom(src => src.Quantity))
				.ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
				.ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

			CreateMap<RegisterUserDto, ApplicationUser>(MemberList.None) // <Source, Dest>
				.ForMember(dst => dst.FullName, opt => opt.MapFrom(src => src.FullName));

			CreateMap<EditUserDto, ApplicationUser>(MemberList.None) // <Source, Dest>
				.ForMember(dst => dst.FullName, opt => opt.MapFrom(src => src.FullName))
				.ForMember(dst => dst.DOB, opt => opt.MapFrom(src => src.DateOfBirth));

			CreateMap<CreateUserDeliveryAddressDto, UserDeliveryAddress>(MemberList.None) // <Source, Dest>
				.ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.UserId))
				.ForMember(dst => dst.ReceiverName, opt => opt.MapFrom(src => src.ReceiverName))
				.ForMember(dst => dst.DeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress))
				.ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

			CreateMap<EditUserDeliveryAddressDto, UserDeliveryAddress>(MemberList.None) // <Source, Dest>
				.ForMember(dst => dst.ReceiverName, opt => opt.MapFrom(src => src.ReceiverName))
				.ForMember(dst => dst.DeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress))
				.ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

			CreateMap<CreateInvoiceDto, Invoice>(MemberList.None) // <Source, Dest>
				.ForMember(dst => dst.DateOrdered, opt => opt.MapFrom(src => DateTime.UtcNow)) // Default value
				.ForMember(dst => dst.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
				.ForMember(dst => dst.PaymentStatus, opt => opt.MapFrom(src => PaymentStatus.Waiting)) // Default value
				.ForMember(dst => dst.DeliveryStatus, opt => opt.MapFrom(src => DeliveryStatus.WaitingForConfirmation)) // Default value
				.ForMember(dst => dst.ReceiverName, opt => opt.MapFrom(src => src.ReceiverName))
				.ForMember(dst => dst.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
				.ForMember(dst => dst.DeliveryAddress, opt => opt.MapFrom(src => src.DeliveryAddress))
				.ForMember(dst => dst.DeliveryDescription, opt => opt.MapFrom(src => src.DeliveryDescription))
				.ForMember(dst => dst.TotalCost, opt => opt.MapFrom(src => src.TotalCost))
				.ForMember(dst => dst.TotalFee, opt => opt.MapFrom(src => src.TotalFee))
				.ForMember(dst => dst.Discount, opt => opt.MapFrom(src => src.Discount));

			CreateMap<EditInvoiceDto, Invoice>(MemberList.None) // <Source, Dest>
				.ForMember(dst => dst.DeliveryStatus, opt => opt.MapFrom(src => src.DeliveryStatus))
				.ForMember(dst => dst.DeliveryDescription, opt => opt.MapFrom(src => src.DeliveryDescription));
		}

		private void FromDataAccessLayer()
		{
			CreateMap<Category, CategoryDto>() // <Source, Dest>
				.ReverseMap();

			CreateMap<Category, CategoryProductsDto>() // <Source, Dest>
				.ReverseMap();

			CreateMap<Product, ProductDto>() // <Source, Dest>
				.ReverseMap();

			CreateMap<ApplicationUser, UserDto>() // <Source, Dest>
				.ForMember(dst => dst.DateOfBirth, opt => opt.MapFrom(src => src.DOB))
				.ReverseMap();

			CreateMap<UserDeliveryAddress, UserDeliveryAddressDto>() // <Source, Dest>
				.ReverseMap();

			CreateMap<PaymentStatus, PaymentStatusDto>() // <Source, Dest>
				.ConvertUsingEnumMapping(opt => opt.MapByValue())
				.ReverseMap();

			CreateMap<PaymentType, PaymentTypeDto>() // <Source, Dest>
				.ConvertUsingEnumMapping(opt => opt.MapByValue())
				.ReverseMap();

			CreateMap<DeliveryStatus, DeliveryStatusDto>() // <Source, Dest>
				.ConvertUsingEnumMapping(opt => opt.MapByValue())
				.ReverseMap();

			CreateMap<PaymentStatus, string>() // <Source, Dest>
				.ConvertUsing(src => ((PaymentStatusDto)(int)src).GetDescription());

			CreateMap<PaymentType, string>() // <Source, Dest>
				.ConvertUsing(src => ((PaymentTypeDto)(int)src).GetDescription());

			CreateMap<DeliveryStatus, string>() // <Source, Dest>
				.ConvertUsing(src => ((DeliveryStatusDto)(int)src).GetDescription());

			CreateMap<Invoice, InvoiceDto>() // <Source, Dest>
				.ForMember(dst => dst.TotalPayment, opt => opt.MapFrom(src => src.TotalCost + src.TotalFee - src.Discount))
				.ReverseMap();

			CreateMap<Invoice, InvoiceDetailDto>() // <Source, Dest>
				.ForMember(dst => dst.TotalPayment, opt => opt.MapFrom(src => src.TotalCost + src.TotalFee - src.Discount))
				.ReverseMap();

			CreateMap<Invoice, InvoiceFullDetailDto>() // <Source, Dest>
				.ForMember(dst => dst.TotalPayment, opt => opt.MapFrom(src => src.TotalCost + src.TotalFee - src.Discount))
				.ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.User.Email))
				.ReverseMap();

			CreateMap<InvoiceProduct, InvoiceProductDto>() // <Source, Dest>
				.ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Product.Name))
				.ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
				.ForMember(dst => dst.Cost, opt => opt.MapFrom(src => src.CostPerItem * src.Amount))
				.ReverseMap();

			CreateMap<Invoice, MomoPaymentRequestDto>() // <Source, Dest>
				.ForMember(dst => dst.RequestId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
				.ForMember(dst => dst.Amount, opt => opt.MapFrom(src => (src.TotalCost + src.TotalFee - src.Discount).ToString()))
				.ForMember(dst => dst.OrderId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dst => dst.OrderInfo, opt => opt.MapFrom(src => $"email={src.User.Email}"));
		}
	}
}
