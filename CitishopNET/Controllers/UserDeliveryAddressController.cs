using CitishopNET.Business.Services;
using CitishopNET.Shared.Dtos.UserDeliveryAddress;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CitishopNET.Controllers
{
	[ApiController]
	[EnableCors("MyPolicy")]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class UserDeliveryAddressController : ControllerBase
	{
		private readonly IUserDeliveryAddressService _addressService;
		private readonly ILogger<UserDeliveryAddressController> _logger;

		public UserDeliveryAddressController(IUserDeliveryAddressService addressService, ILogger<UserDeliveryAddressController> logger)
		{
			_addressService = addressService;
			_logger = logger;
		}

		/// <summary>
		/// Lấy danh sách Address được pagination
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		/// <response code="200">Trả về danh sách Address</response>
		// GET: api/<UserDeliveryAddressController>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAsync([FromQuery] UserDeliveryAddressQueryCriteria query)
		{
			var addresses = await _addressService.GetUserDeliveryAddressesAsync(query);
			return Ok(addresses);
		}

		/// <summary>
		/// Lấy Address theo Id
		/// </summary>
		/// <param name="userId">UserId</param>
		/// <param name="id">Id</param>
		/// <returns></returns>
		// GET api/<UserDeliveryAddressController>/5
		[HttpGet("{userId}/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetByIdAsync(string userId, int id)
		{
			var addressDto = await _addressService.GetByIdAsync(userId, id);
			return addressDto == null ? NotFound() : Ok(addressDto);
		}

		/// <summary>
		/// Thêm Address
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="201">Trả về Address mới tạo</response>
		/// <response code="400">Input không hợp lệ</response>
		/// <response code="500">Lỗi server</response>
		// POST api/<CategoryController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> CreateAsync([FromBody] CreateUserDeliveryAddressDto value)
		{
			var addressDto = await _addressService.AddAsync(value);
			return addressDto == null
				? Problem(statusCode: StatusCodes.Status500InternalServerError)
				: CreatedAtAction(nameof(CreateAsync), new { addressDto.UserId, addressDto.Id }, addressDto);
		}

		/// <summary>
		/// Chỉnh sửa Address theo Id
		/// </summary>
		/// <param name="userId">UserId</param>
		/// <param name="id">Id</param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="200">Trả về Address đã chỉnh sửa</response>
		/// <response code="400">Input không hợp lệ</response>
		/// <response code="404">Không tìm thấy Address</response>
		// PUT api/<CategoryController>/5
		[HttpPut("{userId}/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateAsync(string userId, int id, [FromBody] EditUserDeliveryAddressDto value)
		{
			var addressDto = await _addressService.UpdateAsync(userId, id, value);
			return addressDto == null ? NotFound() : Ok(addressDto);
		}

		/// <summary>
		/// Xoá Address
		/// </summary>
		/// <param name="userId">UserId</param>
		/// <param name="id">Id</param>
		/// <returns></returns>
		/// <response code="204">Xoá Address thành công</response>
		/// <response code="404">Không tìm thấy Address</response>
		// DELETE api/<CategoryController>/5
		[HttpDelete("{userId}/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> DeleteAsync(string userId, int id)
		{
			var addressDto = await _addressService.DeleteAsync(userId, id);
			return addressDto == null ? NotFound() : NoContent();
		}
	}
}
