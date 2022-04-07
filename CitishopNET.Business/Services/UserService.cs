using AutoMapper;
using CitishopNET.Business.Extensions;
using CitishopNET.Business.Repository;
using CitishopNET.DataAccess.Models;
using CitishopNET.Shared;
using CitishopNET.Shared.Dtos.ApplicationUser;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.EntityFrameworkCore;

namespace CitishopNET.Business.Services
{
	public class UserService : IUserService
	{
		private readonly IBaseRepository<ApplicationUser> _userRepository;
		private readonly IMapper _mapper;

		public UserService(IBaseRepository<ApplicationUser> userRepository, IMapper mapper)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<PagedModel<UserDto>> GetUsersAsync(BaseQueryCriteria criteria)
		{
			var query = _userRepository.Entities.AsNoTracking().OrderBy(x => x.UserName);
			var dtoQuery = query.Select(x => _mapper.Map<UserDto>(x));
			var pagedUsers = await dtoQuery.PaginateAsync(criteria.Page, criteria.Limit);
			return pagedUsers;
		}
	}
}
