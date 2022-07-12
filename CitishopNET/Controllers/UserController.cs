using CitishopNET.Business.Services;
using CitishopNET.DataAccess.Models;
using CitishopNET.Shared.Dtos.ApplicationUser;
using CitishopNET.Shared.QueryCriteria;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;

namespace CitishopNET.Controllers
{
	[ApiController]
	[EnableCors("MyPolicy")]
	[Route("api/[controller]")]
	[Produces("application/json")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly ILogger<UserController> _logger;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IEmailSender _emailSender;

		public UserController(
			IUserService userService,
			ILogger<UserController> logger,
			SignInManager<ApplicationUser> signInManager,
			UserManager<ApplicationUser> userManager,
			IEmailSender emailSender)
		{
			_userService = userService;
			_logger = logger;
			_signInManager = signInManager;
			_userManager = userManager;
			_emailSender = emailSender;
		}

		/// <summary>
		/// Lấy danh sách User được pagination
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		/// <response code="200">Trả về danh sách User</response>
		// GET api/<UserController>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAsync([FromQuery] BaseQueryCriteria query)
		{
			var users = await _userService.GetUsersAsync(query);
			return Ok(users);
		}

		/// <summary>
		/// Lấy User theo Email
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		/// <response code="200">Trả về User</response>
		/// <response code="404">Không tìm thấy User</response>
		// GET api/<UserController>/5
		[HttpGet("{email}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetByEmailAsync(string email)
		{
			var userDto = await _userService.GetUserByEmailAsync(email);
			return userDto == null ? NotFound() : Ok(userDto);
		}

		/// <summary>
		/// Đăng ký User &amp; gửi xác nhận đến email dùng để đăng ký
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="200">Đăng ký thành công &amp; đã gửi xác nhận đến email</response>
		/// <response code="400">Có lỗi khi đăng ký</response>
		// POST api/<UserController>/Register
		[HttpPost("Register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDto value)
		{
			(var result, var user) = await _userService.RegisterAsync(value);

			if (result.Succeeded)
			{
				_logger.LogInformation("User created a new account with password.");

				var userId = await _userManager.GetUserIdAsync(user);
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

				//TODO: Change to frontend page
				var callbackUrl = Url.ActionLink(
					action: "ConfirmEmail",
					values: new { area = "api", userId, code },
					protocol: Request.Scheme);
				var encodedUrl = HtmlEncoder.Default.Encode(callbackUrl!);
				await _emailSender.SendEmailAsync(value.Email,
				"Xác nhận email",
				$"Vui lòng xác nhận email bằng cách click link này <a href='{encodedUrl}'>{encodedUrl}</a>");

				return Ok(new { userId, code });
			}
			return BadRequest(result.Errors);
		}

		/// <summary>
		/// Gửi lại xác nhận đến email
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="200">Đăng ký thành công &amp; đã gửi xác nhận đến email (Vẫn trả về StatusCode 200 nếu email không tồn tại)</response>
		// POST api<UserController>/ResendEmailConfirmation
		[HttpPost("ResendEmailConfirmation")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> ResendEmailConfirmationAsync([FromBody] UserEmailDto value)
		{
			var user = await _userManager.FindByEmailAsync(value.Email);
			if (user == null)
			{
				// Don't reveal that the user does not exist
				return Ok();
			}

			var userId = await _userManager.GetUserIdAsync(user);
			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

			//TODO: Change to frontend page
			var callbackUrl = Url.ActionLink(
					action: "ConfirmEmail",
					values: new { userId, code },
					protocol: Request.Scheme);
			var encodedUrl = HtmlEncoder.Default.Encode(callbackUrl!);
			await _emailSender.SendEmailAsync(value.Email,
				"Xác nhận email",
				$"Vui lòng xác nhận email bằng cách click link này <a href='{encodedUrl}'>{encodedUrl}</a>");

			return Ok(new { userId, code });
		}

		/// <summary>
		/// Xác nhận email bằng userId &amp; code được gửi từ email
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="code"></param>
		/// <returns></returns>
		/// <response code="200">Xác nhận thành công, cho phép User đăng nhập</response>
		/// <response code="400">Có lỗi khi xác nhận</response>
		/// <response code="404">Không tìm thấy User</response>
		// GET api/<UserController>/ConfirmEmail
		[HttpGet("ConfirmEmail")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> ConfirmEmailAsync(string userId, string code)
		{
			if (userId == null || code == null)
			{
				return BadRequest();
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound();
			}

			code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
			var result = await _userManager.ConfirmEmailAsync(user, code);
			return result.Succeeded
				? Ok("Xác nhận email thành công.")
				: BadRequest(result.Errors);
		}

		/// <summary>
		/// Đăng nhập User (Không cho đăng nhập nếu chưa xác nhận email)
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="200">Đăng nhập thành công</response>
		/// <response code="400">Có lỗi khi đăng nhập</response>
		// POST api/<UserController>/Login
		[HttpPost("Login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto value)
		{
			var result = await _signInManager.PasswordSignInAsync(value.Email, value.Password, isPersistent: false, lockoutOnFailure: false);
			if (result.Succeeded)
			{
				var userDto = await _userService.GetUserByEmailAsync(value.Email);
				return Ok(userDto);
			}
			if (result.IsNotAllowed) // User's email hasn't been confirmed
			{
				return BadRequest("Tài khoản hiện tại không đăng nhập được.");
			}
			return BadRequest("Tên đăng nhập hoặc mật khẩu không đúng.");
		}

		/// <summary>
		/// Đăng xuất
		/// </summary>
		/// <returns></returns>
		// POST api/<UserController>/Logout
		[HttpPost("Logout")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> LogoutAsync()
		{
			await _signInManager.SignOutAsync();
			return Ok();
		}

		/// <summary>
		/// Thay đổi mật khẩu User
		/// </summary>
		/// <param name="email"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="204">Thay đổi mật khẩu thành công</response>
		/// <response code="400">Có lỗi khi thay đổi mật khẩu</response>
		/// <response code="404">Không tìm thấy User</response>
		// PUT api/<UserController>/ChangePassword/{userName}
		[HttpPut("ChangePassword/{email}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> ChangePasswordAsync(string email, [FromBody] ChangePasswordDto value)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return NotFound();
			}

			var changePasswordResult = await _userManager.ChangePasswordAsync(user, value.OldPassword, value.NewPassword);
			if (changePasswordResult.Succeeded)
			{
				await _signInManager.RefreshSignInAsync(user);
				_logger.LogInformation("User changed their password successfully.");
				return NoContent();
			}
			return BadRequest(changePasswordResult.Errors);
		}

		/// <summary>
		/// Gửi email quên mật khẩu để User đặt lại mật khẩu mới
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="200">Đã gửi email (Vẫn trả về StatusCode 200 nếu email không tồn tại hoặc User chưa xác nhận email)</response>
		// POST api/<UserController>/ForgotPassword
		[HttpPost("ForgotPassword")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> ForgotPasswordAsync([FromBody] UserEmailDto value)
		{
			var user = await _userManager.FindByEmailAsync(value.Email);
			if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
			{
				// Don't reveal that the user does not exist or is not confirmed
				return Ok();
			}

			var code = await _userManager.GeneratePasswordResetTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

			// TODO: Change to URL link to frontend page
			await _emailSender.SendEmailAsync(value.Email,
				"Quên mật khẩu",
				$"Mã code đặt lại mật khẩu của bạn là: <b>{code}</b>");

			return Ok(new { code });
		}

		/// <summary>
		/// Đặt lại mật khẩu User với code xác nhận gửi từ email quên mật khẩu
		/// </summary>
		/// <param name="code"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="204">Đặt lại mật khẩu User thành công (Vẫn trả về StatusCode 204 nếu email không tồn tại)</response>
		/// <response code="400">Có lỗi khi đặt lại mật khẩu</response>
		// PUT api/<UserController>/ResetPassword/{code}
		[HttpPut("ResetPassword/{code}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> ResetPasswordAsync(string code, [FromBody] ResetPasswordDto value)
		{
			var user = await _userManager.FindByEmailAsync(value.Email);
			if (user == null)
			{
				// Don't reveal that the user does not exist
				return NoContent();
			}

			code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
			var result = await _userManager.ResetPasswordAsync(user, code, value.Password);
			if (result.Succeeded)
			{
				return NoContent();
			}
			return BadRequest(result.Errors);
		}

		/// <summary>
		/// Chỉnh sửa User theo Email
		/// </summary>
		/// <param name="email"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="200">Trả về User đã chỉnh sửa</response>
		/// <response code="400">Input không hợp lệ</response>
		/// <response code="404">Không tìm thấy User</response>
		// PUT api/<UserController>/{userName}
		[HttpPut("{email}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateAsync(string email, [FromBody] EditUserDto value)
		{
			var userDto = await _userService.UpdateAsync(email, value);
			return userDto == null ? NotFound() : Ok(userDto);
		}

		/// <summary>
		/// Chỉnh sửa phân quyền User theo Email
		/// </summary>
		/// <param name="email"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <response code="200">Trả về User đã chỉnh sửa</response>
		/// <response code="400">Input không hợp lệ</response>
		/// <response code="404">Không tìm thấy User</response>
		// PUT api/<UserController>/{userName}
		[HttpPut("{email}/Role")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> UpdateUserRoleAsync(string email, [FromBody] EditUserRoleDto value)
		{
			var userDto = await _userService.UpdateUserRoleAsync(email, value);
			return userDto == null ? NotFound() : Ok(userDto);
		}

		/// <summary>
		/// Xoá User
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		/// <response code="204">Xoá User thành công</response>
		/// <response code="404">Không tìm thấy User</response>
		// DELETE api/<UserController>/{userName}
		[HttpDelete("{email}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> DeleteAsync(string email)
		{
			var userDto = await _userService.DeleteAsync(email);
			return userDto == null ? NotFound() : NoContent();
		}
	}
}
