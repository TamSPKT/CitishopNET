using CitishopNET.Business.Services;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.AspNetCore.Mvc;

namespace CitishopNET.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly ILogger<UserController> _logger;

		public UserController(IUserService userService, ILogger<UserController> logger)
		{
			_userService = userService;
			_logger = logger;
		}

		/// <summary>
		/// Lấy danh sách User được pagination
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		/// <response code="200">Trả về danh sách Product</response>
		// GET api/<UserController>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Get([FromQuery] BaseQueryCriteria query)
		{
			var users = await _userService.GetUsersAsync(query);
			return Ok(users);
		}
	}
}
