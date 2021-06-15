using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Linq;
using BackEnd.MW;
using BackEnd.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BackEnd.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        IHostingEnvironment _hostingEnvironment;
        private readonly FileMW _fileMW;

        public FileController(IHostingEnvironment hostingEnvironment, FileMW fileMw)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileMW = fileMw;
        }

        [HttpPost("[action]")]
        public object Upload(string data)
        {
            try
            {
                var myFile = Request.Form.Files[0];
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "Upload_Files");
                string filename = DateTime.Now.ToString("yyyyMMdd") + "_" + myFile.FileName;
                path = utils.CreateDirectory(path);
                //path = Path.Combine(path, today);
                //if (!Directory.Exists(path))
                //    Directory.CreateDirectory(path);

                using (var fileStream = System.IO.File.Create(Path.Combine(path, filename)))
                {
                    myFile.CopyTo(fileStream);
                }
                var info = new FileData()
                {
                    file_NM = filename,
                    size = myFile.Length.ToString(),
                    type = myFile.ContentType,
                    location = path
                };

                var types = typeof(FileData).GetProperties().ToList();
                string param = string.Empty;
                for (int i = 0; i < types.Count; i++)
                {
                    if (types[i].Name.Equals("file_Code") || types[i].Name.Equals("insrt_Dt") || types[i].Name.Equals("insrt_User")) continue;
                    //순서대로 값을 param에 추가
                    param = string.IsNullOrEmpty(param) ? "'" + types[i].GetValue(info) + "'" : param + ",'" + types[i].GetValue(info) + "'";
                }
                param = param + utils.lastParam(JObject.Parse(data.ToString()));
                var result = _fileMW.AddFile(param);
                return JObject.Parse(result.ToString());
            }
            catch
            {
                Response.StatusCode = 400;
                return new EmptyResult();
            }
        }

        [HttpPost("[action]")]
        public object UploadImage(IFormFile imageFile)
        {
            try
            {
                string[] imageExtensions = { ".jpg", ".jpeg", ".gif", ".png" };

                var fileName = imageFile.FileName.ToLower();
                var isValidExtenstion = imageExtensions.Any(ext => {
                    return fileName.LastIndexOf(ext) > -1;
                });

                if (isValidExtenstion)
                {
                    // Uncomment to save the file
                    //var path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    //if(!Directory.Exists(path))
                    //    Directory.CreateDirectory(path);

                    //using(var fileStream = System.IO.File.Create(Path.Combine(path, imageFile.FileName))) {
                    //    imageFile.CopyTo(fileStream);
                    //}
                }
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }

        [HttpGet("[action]/{file_code}")]
        public async Task<IActionResult> GetImage(string file_code)
        {
            object result = _fileMW.GetImage(file_code).ToString();
            var info = JsonConvert.DeserializeObject<FileData>(result.ToString());
            var image = System.IO.File.OpenRead(Path.Combine(info.location, info.file_NM));
            //return File(image, info.type);
            return File(image, "image/png");
        }
    }
}