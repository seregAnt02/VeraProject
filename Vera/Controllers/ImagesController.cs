using Microsoft.AspNetCore.Mvc;

namespace Vera.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        public ImagesController(IWebHostEnvironment appEnvironment){

            _appEnvironment = appEnvironment;
        }
        
        [HttpGet]
        public IActionResult Get(int imageId)
        {            
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "wwwroot/images/help.jpg");
            return PhysicalFile(file_path, "image/jpeg"); //Vera\wwwroot\images\help.jpg
        }

        [HttpGet]
        public IActionResult GetFile()
        {
            string? code = new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString();
            
            var image = System.IO.File.OpenRead("wwwroot/images/help.jpg"); 
            return File(image, "image/jpeg");
        }                
        
    }
}