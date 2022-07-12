using CitishopNET.Business.Services;
using CitishopNET.Shared.Dtos.Category;
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
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly ILogger<CategoryController> _logger;

		public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
		{
			_categoryService = categoryService;
			_logger = logger;
		}

		/// <summary>
		/// Lấy danh sách Category được pagination
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		/// <response code="200">Trả về danh sách Category</response>
		// GET api/<CategoryController>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAsync([FromQuery] BaseQueryCriteria query)
		{
			var categories = await _categoryService.GetCategoriesAsync(query);
			return Ok(categories);
		}

		/// <summary>
		/// Lấy Category theo Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <response code="200">Tìm thấy Category</response>
		/// <response code="404">Không tìm thấy Category</response>
		// GET api/<CategoryController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var categoryDto = await _categoryService.GetCategoryByIdAsync(id);
			return categoryDto == null ? NotFound() : Ok(categoryDto);
		}

		/// <summary>
		/// Thêm Category
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="201">Trả về Category mới tạo</response>
		/// <response code="400">Input không hợp lệ</response>
		/// <response code="500">Lỗi server</response>
		// POST api/<CategoryController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryDto value)
		{
			var categoryDto = await _categoryService.AddAsync(value);
			return categoryDto == null
				? Problem(statusCode: StatusCodes.Status500InternalServerError)
				: CreatedAtAction(nameof(CreateAsync), new { categoryDto.Id }, categoryDto);
		}

		/// <summary>
		/// Chỉnh sửa Category theo Id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="200">Trả về Category đã chỉnh sửa</response>
		/// <response code="400">Input không hợp lệ</response>
		/// <response code="404">Không tìm thấy Category</response>
		// PUT api/<CategoryController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EditCategoryDto value)
		{
			var categoryDto = await _categoryService.UpdateAsync(id, value);
			return categoryDto == null ? NotFound() : Ok(categoryDto);
		}

		/// <summary>
		/// Xoá Category
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <response code="204">Xoá Category thành công</response>
		/// <response code="404">Không tìm thấy Category</response>
		// DELETE api/<CategoryController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var categoryDto = await _categoryService.DeleteAsync(id);
			return categoryDto == null ? NotFound() : NoContent();
		}
	}
}
