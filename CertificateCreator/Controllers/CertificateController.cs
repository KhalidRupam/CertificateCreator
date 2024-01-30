using CertificateCreator.Models;
using Microsoft.AspNetCore.Mvc;

namespace CertificateCreator.Controllers
{
    public class CertificateController : Controller
    {
        public IActionResult Index(EmployeesCertificatesWithDetails employees)
        {
            return View(employees);
        }
    }
}
