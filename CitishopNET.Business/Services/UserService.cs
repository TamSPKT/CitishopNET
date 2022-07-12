using AutoMapper;
using CitishopNET.Business.Extensions;
using CitishopNET.Business.Repository;
using CitishopNET.DataAccess.Models;
using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.ApplicationUser;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CitishopNET.Business.Services
{
	public class UserService : IUserService
	{
		private readonly IBaseRepository<ApplicationUser> _userRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IUserStore<ApplicationUser> _userStore;
		private readonly IUserEmailStore<ApplicationUser> _userEmailStore;
		private readonly IUserPhoneNumberStore<ApplicationUser> _userPhoneNumberStore;

		public UserService(IBaseRepository<ApplicationUser> userRepository, IMapper mapper, UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
		{
			_userRepository = userRepository;
			_mapper = mapper;
			_userManager = userManager;
			_userStore = userStore;
			_userEmailStore = GetEmailStore();
			_userPhoneNumberStore = GetPhoneNumberStore();
		}

		public async Task<PagedModel<UserDto>> GetUsersAsync(BaseQueryCriteria criteria)
		{
			var query = _userRepository.Entities.AsNoTracking().OrderBy(x => x.UserName);
			var dtoQuery = query.Select(x => _mapper.Map<UserDto>(x));
			var pagedUsers = await dtoQuery.PaginateAsync(criteria.Page, criteria.Limit);
			return pagedUsers;
		}

		public async Task<UserDto?> GetUserByEmailAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			return user != null
				? _mapper.Map<UserDto>(user)
				: null;
		}

		public async Task<(IdentityResult, ApplicationUser)> RegisterAsync(RegisterUserDto registerUserDto)
		{
			var user = CreateUser();

			await _userStore.SetUserNameAsync(user, registerUserDto.Email, CancellationToken.None);
			await _userEmailStore.SetEmailAsync(user, registerUserDto.Email, CancellationToken.None);
			user = _mapper.Map<RegisterUserDto, ApplicationUser>(registerUserDto, user);
			var result = await _userManager.CreateAsync(user, registerUserDto.Password);

			return (result, user);
		}

		public async Task<UserDto?> UpdateAsync(string email, EditUserDto editUserDto)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return null;
			}
			await _userPhoneNumberStore.SetPhoneNumberAsync(user, editUserDto.PhoneNumber, CancellationToken.None);
			user = _mapper.Map<EditUserDto, ApplicationUser>(editUserDto, user);
			var result = await _userManager.UpdateAsync(user);

			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto?> UpdateUserRoleAsync(string email, EditUserRoleDto dto)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return null;
			}
			user.IsAdmin = dto.IsAdmin;
			var result = await _userManager.UpdateAsync(user);

			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto?> DeleteAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return null;
			}
			await _userManager.DeleteAsync(user);
			return _mapper.Map<UserDto>(user);
		}

		private ApplicationUser CreateUser()
		{
			try
			{
				return Activator.CreateInstance<ApplicationUser>();
			}
			catch
			{
				throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
					$"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor");
			}
		}

		private IUserEmailStore<ApplicationUser> GetEmailStore()
		{
			if (!_userManager.SupportsUserEmail)
			{
				throw new NotSupportedException("Requires a user store with email support.");
			}
			return (IUserEmailStore<ApplicationUser>)_userStore;
		}

		private IUserPhoneNumberStore<ApplicationUser> GetPhoneNumberStore()
		{
			if (!_userManager.SupportsUserPhoneNumber)
			{
				throw new NotSupportedException("Requires a user store with phone number support.");
			}
			return (IUserPhoneNumberStore<ApplicationUser>)_userStore;
		}
	}
}
