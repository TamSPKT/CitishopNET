using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.ApplicationUser;
using CitishopNET.Shared.QueryCriteria;

namespace CitishopNET.Business.Services
{
	public interface IUserService
	{
		Task<PagedModel<UserDto>> GetUsersAsync(BaseQueryCriteria criteria);
	}
}
