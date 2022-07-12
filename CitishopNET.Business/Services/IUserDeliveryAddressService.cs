using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.UserDeliveryAddress;
using CitishopNET.Shared.QueryCriteria;

namespace CitishopNET.Business.Services
{
	public interface IUserDeliveryAddressService
	{
		Task<PagedModel<UserDeliveryAddressDto>> GetUserDeliveryAddressesAsync(UserDeliveryAddressQueryCriteria criteria);
		Task<UserDeliveryAddressDto?> GetByIdAsync(string userId, int id);
		Task<UserDeliveryAddressDto?> AddAsync(CreateUserDeliveryAddressDto createDto);
		Task<UserDeliveryAddressDto?> UpdateAsync(string userId, int id, EditUserDeliveryAddressDto editDto);
		Task<UserDeliveryAddressDto?> DeleteAsync(string userId, int id);
	}
}
