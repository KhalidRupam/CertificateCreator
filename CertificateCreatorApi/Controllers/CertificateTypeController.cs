using CertificateCreatorApi.Repositories.CertificateTypeRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertificateCreatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateTypeController : ControllerBase
    {
        private readonly ICertificateTypeRepository _certificateTypeRepository;

        public CertificateTypeController(ICertificateTypeRepository certificateTypeRepository)
        {
            _certificateTypeRepository = certificateTypeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCertificateTypes()
        {
            try
            {
                var res = await _certificateTypeRepository.GetAllCertificateTypes();
                return Ok(res);
            }
            catch (Exception ex) { return StatusCode(500, "An error occurred while inserting the user."); }
        }
    }
}
