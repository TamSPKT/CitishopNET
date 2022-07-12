using AutoMapper;
using CitishopNET.Business.Extensions;
using CitishopNET.Business.Repository;
using CitishopNET.DataAccess.Models;
using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.UserDeliveryAddress;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.EntityFrameworkCore;

namespace CitishopNET.Business.Services
{
	public class UserDeliveryAddressService : IUserDeliveryAddressService
	{
		private readonly IBaseRepository<UserDeliveryAddress> _addressRepository;
		private readonly IMapper _mapper;

		public UserDeliveryAddressService(IBaseRepository<UserDeliveryAddress> addressRepository, IMapper mapper)
		{
			_addressRepository = addressRepository;
			_mapper = mapper;
		}

		public async Task<PagedModel<UserDeliveryAddressDto>> GetUserDeliveryAddressesAsync(UserDeliveryAddressQueryCriteria criteria)
		{
			var query = _addressRepository.Entities.AsNoTracking()
				.Where(x => x.UserId == criteria.UserId || criteria.IgnoreUserId)
				.OrderBy(x => x.Id);
			var dtoQuery = query.Select(x => _mapper.Map<UserDeliveryAddressDto>(x));
			var pagedAddresses = await dtoQuery.PaginateAsync(criteria.Page, criteria.Limit);
			return pagedAddresses;
		}

		public async Task<UserDeliveryAddressDto?> GetByIdAsync(string userId, int id)
		{
			var query = _addressRepository.Entities.AsNoTracking();
			var address = await query.SingleOrDefaultAsync(x => x.UserId == userId && x.Id == id);
			return address != null
				? _mapper.Map<UserDeliveryAddressDto>(address)
				: null;
		}

		public async Task<UserDeliveryAddressDto?> AddAsync(CreateUserDeliveryAddressDto createDto)
		{
			var address = _mapper.Map<UserDeliveryAddress>(createDto);
			return await _addressRepository.AddAsync(address)
				? _mapper.Map<UserDeliveryAddressDto>(address)
				: null;
		}

		public async Task<UserDeliveryAddressDto?> UpdateAsync(string userId, int id, EditUserDeliveryAddressDto editDto)
		{
			var address = await _addressRepository.Entities.FindAsync(userId, id);
			if (address == null)
			{
				return null;
			}
			address = _mapper.Map<EditUserDeliveryAddressDto, UserDeliveryAddress>(editDto, address);
			await _addressRepository.UpdateAsync(address);
			return _mapper.Map<UserDeliveryAddressDto>(address);
		}

		public async Task<UserDeliveryAddressDto?> DeleteAsync(string userId, int id)
		{
			var address = await _addressRepository.Entities.FindAsync(userId, id);
			if (address == null)
			{
				return null;
			}
			await _addressRepository.DeleteAsync(address);
			return _mapper.Map<UserDeliveryAddressDto>(address);
		}
	}
}
