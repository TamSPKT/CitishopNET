namespace CitishopNET.Business.Services
{
	public interface IReportService
	{
		Task<List<decimal>> GetAnalyticsDataAsync(); // Doanh số trong tháng
		Task<List<decimal>> GetDashboardReportAsync(); // Tổng doanh thu, Tổng khách hàng, Đơn hàng (Tổng, Chờ thanh toán, Thành công, Thất bại)
		Task<List<decimal>> GetUserReportAsync(string email); // Đơn hàng (Tổng, Chờ xác nhận, Đang giao, Đã giao)
	}
}
