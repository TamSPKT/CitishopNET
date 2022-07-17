using CitishopNET.Business.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CitishopNET.Controllers
{
	[ApiController]
	[EnableCors("MyPolicy")]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class ReportController : ControllerBase
	{
		private readonly IReportService _reportService;
		private readonly ILogger<InvoiceController> _logger;

		public ReportController(IReportService reportService, ILogger<InvoiceController> logger)
		{
			_reportService = reportService;
			_logger = logger;
		}

		// GET: api/<ReportController>/Analytics
		[HttpGet("Analytics")]
		public async Task<IActionResult> GetAnalyticsDataAsync()
		{
			var list = await _reportService.GetAnalyticsDataAsync();
			return Ok(list);
		}

		// GET api/<ReportController>/Dashboard
		[HttpGet("Dashboard")]
		public async Task<IActionResult> GetDashboardReportAsync()
		{
			var list = await _reportService.GetDashboardReportAsync();
			return Ok(list);
		}

		// GET api/<ReportController>/5
		[HttpGet("{email}")]
		public async Task<IActionResult> GetUserReportAsync(string email)
		{
			var list = await _reportService.GetUserReportAsync(email);
			return Ok(list);
		}
	}
}
