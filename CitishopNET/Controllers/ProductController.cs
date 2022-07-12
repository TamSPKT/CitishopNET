using CitishopNET.Business.Services;
using CitishopNET.Shared.Dtos.Product;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CitishopNET.Controllers
{
	[ApiController]
	[EnableCors("MyPolicy")]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly ILogger<ProductController> _logger;

		public ProductController(IProductService productService, ILogger<ProductController> logger)
		{
			_productService = productService;
			_logger = logger;
		}

		/// <summary>
		/// Lấy danh sách Product được pagination
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		/// <response code="200">Trả về danh sách Product</response>
		// GET api/<ProductController>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Get([FromQuery] ProductQueryCriteria query)
		{
			var products = _productService.GetProducts(query);
			return Ok(products);
		}

		/// <summary>
		/// Lấy Random danh sách Product được pagination
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		/// <response code="200">Trả về danh sách Product</response>
		// GET api/<ProductController>/Random/5
		[HttpGet("Random/")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetRandomAsync([FromQuery] int count = 5)
		{
			var products = await _productService.GetRandomProductsAsync(count);
			return Ok(products);
		}

		/// <summary>
		/// Lấy Product theo Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <response code="200">Tìm thấy Product</response>
		/// <response code="404">Không tìm thấy Product</response>
		// GET api/<ProductController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var productDto = await _productService.GetProductByIdAsync(id);
			return productDto == null ? NotFound() : Ok(productDto);
		}

		/// <summary>
		/// Thêm Product
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="201">Trả về Product mới tạo</response>
		/// <response code="400">Input không hợp lệ</response>
		/// <response code="500">Lỗi server</response>
		// POST api/<ProductController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> CreateAsync([FromBody] CreateProductDto value)
		{
			var productDto = await _productService.AddAsync(value);
			return productDto == null
				? Problem(statusCode: StatusCodes.Status500InternalServerError)
				: CreatedAtAction(nameof(CreateAsync), new { productDto.Id }, productDto);
		}

		/// <summary>
		/// Chỉnh sửa Product theo Id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="200">Trả về Product đã chỉnh sửa</response>
		/// <response code="400">Input không hợp lệ</response>
		/// <response code="404">Không tìm thấy Product</response>
		// PUT api/<ProductController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EditProductDto value)
		{
			var productDto = await _productService.UpdateAsync(id, value);
			return productDto == null ? NotFound() : Ok(productDto);
		}

		/// <summary>
		/// Xoá Product
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <response code="204">Xoá Product thành công</response>
		/// <response code="404">Không tìm thấy Product</response>
		// DELETE api/<ProductController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var productDto = await _productService.DeleteAsync(id);
			return productDto == null ? NotFound() : NoContent();
		}
	}
}
