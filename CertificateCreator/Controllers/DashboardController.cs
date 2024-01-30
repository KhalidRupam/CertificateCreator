using CertificateCreator.Models;
using CertificateCreator.Services.CertificateTypeService;
using CertificateCreator.Services.EmployeesCertificateService;
using CertificateCreator.Services.EmployeeService;
using CertificateCreator.Services.FileService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.IdentityModel.Tokens.Jwt;

namespace CertificateCreator.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IToastNotification _toastNotification;
        private readonly IEmployeeService _employeeService;
        private readonly ICertificateTypeService _certificateTypeService;
        private readonly IFileService _fileService;
        private readonly IEmployeesCertificateService _employeesCertificate;

        public DashboardController(IToastNotification toastNotification, IEmployeeService employeeService, ICertificateTypeService certificateTypeService,
            IFileService fileService, IEmployeesCertificateService employeesCertificate)
        {
            _toastNotification = toastNotification;
            _employeeService = employeeService;
            _certificateTypeService = certificateTypeService;
            _fileService = fileService;
            _employeesCertificate = employeesCertificate;
        }
        public async Task<IActionResult> Index()
        {

            var res = await LoadData();
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int employeeId, int certificateId, IFormFile file)
        {

            var ress = await _fileService.SaveFileToApi(file);

            var TokenResponse = HttpContext.Session.GetString("token");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(TokenResponse);
            var tokenS = jsonToken as JwtSecurityToken;
            var UserId = tokenS.Claims.First(claim => claim.Type == "UserId").Value;

            var r = await _employeesCertificate.insertEmployeesCertificate(new EmployeesCertificate() { CertificateId = certificateId, EmployeeId = employeeId, PDFUrl = ress, CreatedBy = UserId, CreationDate = DateTime.Now, Id = 0, ModificationDate = DateTime.Now });

            var res = await LoadData();
            return View(res);
        }

        public async Task<IActionResult> Details(string employeeName, string departmentName, int certificateTypeID)
        {
            EmployeesCertificatesWithDetails employees = new();
            employees.EmployeeName = employeeName;
            employees.DepartMentName = departmentName;
            employees.CertificateId = certificateTypeID;
            var res = await LoadData();
            return RedirectToAction("Index", "Certificate", employees);
        }

        public async Task<IActionResult> Delete(int certificateId)
        {
            var response = await _employeesCertificate.DeleteEmployeesCertificates(certificateId);
            _toastNotification.AddSuccessToastMessage(response);
            var res = await LoadData();
            return RedirectToAction("Index");
        }

        public async Task<CertificateCreationViewModel> LoadData()
        {
            var res = await _employeeService.GetAllEmployeeDetails();
            var cer = await _certificateTypeService.GetAllCertificateTypes();
            var employeeWithDetails = await _employeesCertificate.GetAllEmployeesCertificates();

            CertificateCreationViewModel certificateCreationViewModel = new();
            certificateCreationViewModel.employees = res;
            certificateCreationViewModel.certificateTypes = cer;
            certificateCreationViewModel.EmployeesCertificatesWithDetails = employeeWithDetails;
            return certificateCreationViewModel;
        }
    }
}
