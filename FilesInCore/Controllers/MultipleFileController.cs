using FilesInCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilesInCore.Controllers
{
    public class MultipleFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MultipleFile()
        {
            MultipleFilesModel model = new MultipleFilesModel();
            return View(model);
        }

        public IActionResult MultiUpload(MultipleFilesModel model)
        {

            if(ModelState.IsValid)
            {
                model.IsResponse = true;
                if (model.Files.Count > 0)
                {
                    foreach (var file in model.Files)
                    {
                        string path  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        string fileNameWithPath = Path.Combine(path, file.FileName);
                        using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    model.IsSuccess = true;
                    model.Message = "Files upload successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Please select files";
                }
            }
            return View("MultipleFile", model);
        }
    }
}
