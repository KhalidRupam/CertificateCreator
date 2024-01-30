using CertificateCreatorApi.Models;
using CertificateCreatorApi.Repositories.EmployeesCertificateRepository;
using Microsoft.AspNetCore.Mvc;

namespace CertificateCreatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesCertificateController : ControllerBase
    {
        private readonly IEmployeesCertificateRepository _employeesCertificate;

        public EmployeesCertificateController(IEmployeesCertificateRepository employeesCertificate)
        {
            _employeesCertificate = employeesCertificate;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeesCertificate(EmployeesCertificate entity)
        {
            try
            {
                if (entity == null) { return BadRequest("Entity can not be null"); }

                var res = await _employeesCertificate.CreateEmployeesCertificate(entity);

                if (res>0)
                {
                    return Ok("Certificate Created");
                }

                return BadRequest("Something went wrong");
            }
            catch (Exception ex) { return StatusCode(500, "An error occurred while creating the Employees Certificate."); }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployeesCertificate(EmployeesCertificate entity)
        {
            try
            {
                if (entity == null) { return BadRequest("Entity can not be null"); }

                var res = await _employeesCertificate.UpdateEmployeesCertificate(entity);

                if (res > 0)
                {
                    return Ok("Certificate Updated");
                }

                return BadRequest("Something went wrong");
            }
            catch (Exception ex) { return StatusCode(500, "An error occurred while creating the Employees Certificate."); }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesCertificates()
        {
            try
            {
                var res = await _employeesCertificate.GetAllEmployeesCertificates();
                return Ok(res);
            }
            catch (Exception ex) { return StatusCode(500, "An error occurred while creating the Employees Certificate."); }
        }

        [HttpGet("GetAllEmployeesCertificatesByEmployeeId")]
        public async Task<IActionResult> GetAllEmployeesCertificatesByEmployeeId(string creatorId)
        {
            try
            {
                var res = await _employeesCertificate.GetAllEmployeesCertificatesByEmployeeId(creatorId);
                return Ok(res);
            }
            catch (Exception ex) { return StatusCode(500, "An error occurred while creating the Employees Certificate."); }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeesCertificates(int id)
        {
            try
            {
                var res = await _employeesCertificate.DeleteEmployeesCertificates(id);
                if (res>0)
                {
                    return Ok("Deleted Successfully");
                }
                return Ok("Something went wrong");
            }
            catch (Exception ex) { return StatusCode(500, "An error occurred while creating the Employees Certificate."); }
        }
    }
}
