using CitishopNET.Business.Options;
using CitishopNET.Business.Services;
using CitishopNET.Shared.Dtos.Invoice;
using CitishopNET.Shared.EnumDtos;
using CitishopNET.Shared.MomoDtos;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Encodings.Web;

namespace CitishopNET.Controllers
{
	[ApiController]
	[EnableCors("MyPolicy")]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class InvoiceController : ControllerBase
	{
		private readonly IInvoiceService _invoiceService;
		private readonly ILogger<InvoiceController> _logger;
		private readonly MomoPaymentOptions _momoOptions;

		public InvoiceController(IInvoiceService invoiceService, ILogger<InvoiceController> logger, IOptions<MomoPaymentOptions> momoOptionsAccessor)
		{
			_invoiceService = invoiceService;
			_logger = logger;
			_momoOptions = momoOptionsAccessor.Value;
		}

		/// <summary>
		/// Lấy danh sách Invoice được pagination
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		/// <response code="200">Trả về danh sách Invoice</response>
		// GET api/<InvoiceController>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAsync([FromQuery] InvoiceQueryCriteria query)
		{
			var carts = await _invoiceService.GetInvoicesAsync(query);
			return Ok(carts);
		}

		/// <summary>
		/// Lấy Invoice theo Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <response code="200">Tìm thấy Invoice</response>
		/// <response code="404">Không tìm thấy Invoice</response>
		// GET api/<InvoiceController>/5
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var invoiceDto = await _invoiceService.GetInvoiceByIdAsync(id);
			return invoiceDto == null ? NotFound() : Ok(invoiceDto);
		}

		/// <summary>
		/// Lấy Invoice theo Id (for Admin)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <response code="200">Tìm thấy Invoice</response>
		/// <response code="404">Không tìm thấy Invoice</response>
		// GET api/<InvoiceController>/5
		[HttpGet("{id}/details")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetDetailsByIdAsync(Guid id)
		{
			var invoiceDto = await _invoiceService.GetFullInvoiceByIdAsync(id);
			return invoiceDto == null ? NotFound() : Ok(invoiceDto);
		}

		/// <summary>
		/// Thêm Invoice
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="201">Trả về Invoice mới tạo hoặc Response từ Momo Payment</response>
		/// <response code="400">Input không hợp lệ</response>
		/// <response code="500">Lỗi server</response>
		// POST api/<InvoiceController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> CreateAsync([FromBody] CreateInvoiceDto value)
		{
			var notifyUrl = new Uri($"{Request.Scheme}://{Request.Host.Value}/api/Invoice/NotifyUrl").AbsoluteUri;
			string encodedNotifyUrl = HtmlEncoder.Default.Encode(notifyUrl!);
			_logger.LogInformation("NotifyUrl : {EncodedUrl}", encodedNotifyUrl);
			//string encodedNotifyUrl = string.Empty; // Update Invoice using ReturnUrl instead

			var returnUrl = new Uri($"{Request.Scheme}://{Request.Host.Value}/api/Invoice/ReturnUrl").AbsoluteUri;
			string encodedReturnUrl = HtmlEncoder.Default.Encode(returnUrl!);
			_logger.LogInformation("NotifyUrl : {EncodedUrl}", encodedReturnUrl);

			(var status, var response) = await _invoiceService.AddAsync(value, returnUrl: encodedReturnUrl, notifyUrl: encodedNotifyUrl);

			return response switch
			{
				MomoPaymentResponseDto dto => CreatedAtAction(nameof(CreateAsync), new { PaymentStatus = status, MomoResponse = dto }),
				InvoiceDto dto => CreatedAtAction(nameof(CreateAsync), new { dto.Id }, dto),
				ArgumentNullException ex => BadRequest(ex),
				Exception ex => Problem(statusCode: StatusCodes.Status500InternalServerError, detail: ex.Message),
				_ => Problem(statusCode: StatusCodes.Status500InternalServerError),
			};
		}

		/// <summary>
		/// Redirect payment result from Momo Payment to Frontend page (Also update invoice info)
		/// </summary>
		/// <returns></returns>
		// GET api/<InvoiceController>
		[HttpGet("ReturnUrl")]
		public async Task<IActionResult> RedirectMomoPaymentResultAsync()
		{
			string queryString = Request.QueryString.ToString();
			int length = queryString.IndexOf("signature") - 1;
			string parameters = queryString.Substring(
				length > 0 ? 1 : 0,
				length > 0 ? length : 0);
			//string signature = MomoPaymentExtension.SignSHA256(parameters, _momoOptions.SecretKey!);

			if (Guid.TryParse(Request.Query["orderId"].ToString(), out Guid orderId)) // Thông tin Request hợp lệ
			{
				if (Request.Query["errorCode"].Equals("0")) // Thanh toán thành công
				{
					await _invoiceService.UpdatePaymentStatusAsync(orderId, PaymentStatusDto.Succeeded);
				}
				else // Thanh toán thất bại
				{
					await _invoiceService.UpdatePaymentStatusAsync(orderId, PaymentStatusDto.Failed);
				}
			}
			return Redirect(new Uri("https://citishop.azurewebsites.net/").AbsoluteUri);
		}

		/// <summary>
		/// Capture request to notify payment result from Momo Payment
		/// </summary>
		/// <returns></returns>
		// POST api/<InvoiceController>
		[HttpPost("NotifyUrl")]
		public async Task<IActionResult> CaptureMomoPaymentResultAsync()
		{
			string requestBody;
			using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
			{
				requestBody = await reader.ReadToEndAsync();
			}
			_logger.LogInformation("Momo Payment Result : {RequestBody}", requestBody);

			return NoContent();
		}

		/// <summary>
		/// Chỉnh sửa Invoice theo Id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="200">Trả về Invoice đã chỉnh sửa</response>
		/// <response code="400">Input không hợp lệ</response>
		/// <response code="404">Không tìm thấy Invoice</response>
		// PUT api/<InvoiceController>/5
		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EditInvoiceDto value)
		{
			var invoiceDto = await _invoiceService.UpdateAsync(id, value);
			return invoiceDto == null ? NotFound() : Ok(invoiceDto);
		}

		/// <summary>
		/// Xoá Invoice
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <response code="204">Xoá Invoice thành công</response>
		/// <response code="404">Không tìm thấy Invoice</response>
		// DELETE api/<InvoiceController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var invoiceDto = await _invoiceService.DeleteAsync(id);
			return invoiceDto == null ? NotFound() : NoContent();
		}

	}
}
