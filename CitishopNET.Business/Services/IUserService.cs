using CitishopNET.DataAccess.Models;
using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.ApplicationUser;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.AspNetCore.Identity;

namespace CitishopNET.Business.Services
{
	public interface IUserService
	{
		Task<PagedModel<UserDto>> GetUsersAsync(BaseQueryCriteria criteria);
		Task<UserDto?> GetUserByNameAsync(string userName);
		Task<(IdentityResult, ApplicationUser)> RegisterAsync(RegisterUserDto registerUserDto);
		Task<UserDto?> UpdateAsync(string userName, EditUserDto editUserDto);
		Task<UserDto?> DeleteAsync(string userName);
	}
}
