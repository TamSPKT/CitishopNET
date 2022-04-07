using AutoMapper;
using CitishopNET.DataAccess.Models;
using CitishopNET.Shared.Dtos.ApplicationUser;
using CitishopNET.Shared.Dtos.Product;

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

		}

		private void FromDataAccessLayer()
		{
			CreateMap<Product, ProductDto>() // <Source, Dest>
				.ReverseMap();

			CreateMap<ApplicationUser, UserDto>() // <Source, Dest>
				.ForMember(dst => dst.DateOfBirth, opt => opt.MapFrom(src => src.DOB))
				.ReverseMap();
		}
	}
}
