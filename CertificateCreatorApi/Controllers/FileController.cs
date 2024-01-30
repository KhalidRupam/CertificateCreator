using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CertificateCreatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public FileController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> postFile(IFormFile file)
        {
            var FileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources/Pdfs", FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return Ok(FileName);
        }

        [HttpGet("{fileName}")]
        public IActionResult GetPdf(string fileName)
        {
            try
            {
                var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources/Pdfs", fileName);

                if (System.IO.File.Exists(filePath))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "application/pdf", fileName);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
