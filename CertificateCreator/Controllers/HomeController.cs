using CertificateCreator.Models;
using CertificateCreator.Services.LoginService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;

namespace CertificateCreator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IToastNotification _toastNotification;

        public HomeController(ILoginService loginService, IToastNotification toastNotification)
        {
            _loginService = loginService;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginEntity loginDto)
        {
            loginDto.UserName = loginDto.Email;
            if (loginDto.Email != null && loginDto.Password != null)
            {
                var res = await _loginService.checkLogin(loginDto);
                if (string.IsNullOrEmpty(res))
                {
                    ViewBag.UserNotFound = true;
                    _toastNotification.AddErrorToastMessage("User Not Found");
                    return View();
                }
                var handler = new JwtSecurityTokenHandler(); 
                var jsonToken = handler.ReadToken(res);
                var tokenS = jsonToken as JwtSecurityToken;
                var emailVerified = tokenS.Claims.First(claim => claim.Type == "EmailVerified").Value;
                HttpContext.Session.SetString("token", res);
                if (emailVerified.Equals("false",StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("OTPPage", res);
                }
                else
                {
                    _toastNotification.AddSuccessToastMessage("Login successfully");
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            return View();
        }

        public IActionResult UserRegistration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserRegistration(LoginEntity loginDto)
        {
            if (ModelState.IsValid)
            {
                var res = await _loginService.insertUser(loginDto);
                return RedirectToAction("OTPPage", res);
            }
            return View();
        }


        public async Task<IActionResult> OTPPage(LoginEntity loginEntity)
        {
            return View(loginEntity);
        }

        [HttpPost]
        public async Task<IActionResult> OTPPage(string otp, string email)
        {
            var res = await _loginService.verifyOTP(email, otp);
            if (res == "OK")
            {
                return RedirectToAction("OTPVerifiedPage", new LoginEntity() { Email = email });
            }
            else
            {
                return View(new LoginEntity() { Email = email });
            }
        }

        public async Task<IActionResult> OTPVerifiedPage(LoginEntity loginEntity)
        {
            return View();

        }

        public async Task<IActionResult> NewOtp(string email)
        {
            return View(new LoginEntity() { Email = email });
        }
    }
}