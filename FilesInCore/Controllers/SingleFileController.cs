using FilesInCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilesInCore.Controllers
{
    public class SingleFileController : Controller
    {
        public IActionResult Index()
        {
            SingleFileModel model = new SingleFileModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Upload(SingleFileModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string fileName = model.FileName + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
                model.IsSuccess = true;
                model.Message = "File upload successfully";
            }
            return View("Index", model);
        }

        public IActionResult Download() 
        {
            var filePath = "C:\\Users\\PAS\\source\\repos\\FilesInCore\\FilesInCore\\wwwroot\\Files\\bihar.jpg";
            var contentType = "image/jpg";
            
            var fileStream = new FileStream(filePath, FileMode.Open);

            return new FileStreamResult(fileStream, contentType)
            {
                FileDownloadName = "tyautyau.jpg"
            };
        }
    }
}
