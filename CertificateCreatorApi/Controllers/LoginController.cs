using CertificateCreatorApi.Models;
using CertificateCreatorApi.Repositories.LoginRepository;
using CertificateCreatorApi.Services.EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CertificateCreatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IEmailServices _emailServices;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginRepository loginRepository, IEmailServices emailServices, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _emailServices = emailServices;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(LoginEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest();
                }
                var res = await _loginRepository.CreateUser(entity);
                if (string.IsNullOrEmpty(res))
                {
                    return StatusCode(500, "Something went wrong");
                }

                Random r = new Random();
                var otp = r.Next(111111, 999999);

                await _loginRepository.CreateOtp(new UserOTP() { OTP = otp.ToString(), UserId = res, ExpiryDate = DateTime.Now.AddDays(1) });

                string body = $"<p>Your OTP for verification is: <strong>{otp}</strong></p>";

                await _emailServices.SendMail(entity.Email, "OTP verifications", body);
                return Ok();
            }
            catch (Exception ex) { return StatusCode(500, "An error occurred while inserting the user."); }
        }

        [HttpPatch("UpdateOTP")]
        public async Task<IActionResult> UpdateOTP(OTPEntity oTPEntity)
        {
            try
            {
                if (oTPEntity == null)
                {
                    return BadRequest("Invalid request data");
                }

                string emailId = oTPEntity.Email;
                string OTP = oTPEntity.OTP;

                if (string.IsNullOrEmpty(emailId) || string.IsNullOrEmpty(OTP))
                {
                    return BadRequest();
                }
                var res = await _loginRepository.GetLoginByEmailId(emailId);
                if (res == null)
                {
                    return StatusCode(500, "Something went wrong");
                }
                if (res.userOTP.OTP == OTP)
                {
                    res.loginEntity.EmailVerified = true;
                    var response = await _loginRepository.UpdateUser(res.loginEntity);

                    if (response > 0)
                    {
                        string body = $"Dear " + res.loginEntity.UserName + ",<br/>\r\n\r\nWe are pleased to inform you that your One-Time Password (OTP) has been successfully verified.\r\n\r\nThank you for completing the verification process. Your account is now verified, and you can enjoy the full benefits of our services.\r\n\r\nIf you have any questions or concerns, please feel free to contact our support team.\r\n\r\n<br/>Best regards,<br/>\r\n\r\nKhalid";

                        await _emailServices.SendMail(res.loginEntity.Email, "Successfully Verified", body);
                    }
                    return Ok();
                }
                return NotFound();

            }
            catch (Exception ex) { return StatusCode(500, "An error occurred while inserting the user."); }
        }

        [HttpPost("CheckLogin")]
        public async Task<IActionResult> CheckLogin(LoginEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest();
                }
                var res = await _loginRepository.GetLoginByEmailId(entity.Email);
                if (res.loginEntity == null)
                {
                    return StatusCode(500, "User Not found");
                }
                if (res.loginEntity.Password==entity.Password)
                {
                    res.token = GenerateJwtToken(res.loginEntity);

                    return Ok(res.token);
                }
                return StatusCode(500, "Password mismatch");
            }
            catch (Exception ex) { return StatusCode(500, "An error occurred while inserting the user."); }
        }
        private string GenerateJwtToken(LoginEntity login)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.UserName),
                    new Claim("UserName", login.UserName.ToString()),
                     new Claim("UserId", login.Id.ToString()),
                     new Claim("EmailVerified", login.EmailVerified.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                };
            if (login.UserTypeId==2)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else
            {
                authClaims.Add(new Claim(ClaimTypes.Role, "User"));
            }
          

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var tokend = new JwtSecurityTokenHandler().WriteToken(token);
            return tokend;
        }
    }
}
