using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mvCommerce.Libraries.Files;

namespace mvCommerce.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    public class ImageController : Controller
    {
        [HttpPost]
        public IActionResult Storage(IFormFile file)
        {
           var path = FileManager.RegisterProductImage(file);
            if(path.Length > 0)
            {
                return Ok(new { path = path });
            }
            else
            {
                return new StatusCodeResult(500);
            }
        }
        public IActionResult Delete(string path)
        {
            if(FileManager.DeleteProductImage(path))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}