using CertificateCreatorApi.Models;
using CertificateCreatorApi.Repositories.EmployeeRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertificateCreatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeDetails()
        {
            try
            {
                var res = await _employeeRepository.GetAllEmployeeDetails();
                return Ok(res);
            }
            catch (Exception ex) { return StatusCode(500, "An error occurred while inserting the user."); }
        }
    }
}
