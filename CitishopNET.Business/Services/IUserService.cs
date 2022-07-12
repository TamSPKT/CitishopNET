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
		Task<UserDto?> GetUserByEmailAsync(string email);
		Task<(IdentityResult, ApplicationUser)> RegisterAsync(RegisterUserDto registerUserDto);
		Task<UserDto?> UpdateAsync(string email, EditUserDto editUserDto);
		Task<UserDto?> UpdateUserRoleAsync(string email, EditUserRoleDto dto);
		Task<UserDto?> DeleteAsync(string email);
	}
}
