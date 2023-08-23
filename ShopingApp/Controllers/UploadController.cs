using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoping.Model.Models;
using Shoping.Model.Types.Upload;
using Shoping.Model.Types.Vote;
using Shoping.Model.Wrapper;
using Shopping.Utility;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace Shopping.Presentation.Controllers
{
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        public UploadController(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }
        [Route("/[controller]/SingleImage")]
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public Response<UploadResponse> UploadImage([FromForm] FileUploadAPI objFile)
        {

            var uploadedFileName = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + "-" + objFile.imgFile.FileName;
            UploadResponse obj = new UploadResponse
            {
                FileLink = _configuration.GetValue<string>("Url:BaseUrl") + "/Upload/" + uploadedFileName
            };
            if (objFile.imgFile.ContentType.Contains("image/"))
            {
                if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                }
                using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + uploadedFileName))
                {
                    objFile.imgFile.CopyTo(filestream);
                    filestream.Flush();
                }
            }
            else
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "Wrong file type" };
                throw new HttpResponseException(err);
            }
            return new Response<UploadResponse>(obj);

        }
        [Route("/[controller]/Remove")]
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public Response<bool> RemoveUploadedImage([FromBody] DeleteUploadedRequest removeRequest)
        {
            var baseUrl = _configuration.GetValue<string>("Url:BaseUrl");

            if (!removeRequest.FileLink.Contains(baseUrl + "/Upload/"))
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "Wrong file link" };
                throw new HttpResponseException(err);
            }

            var fileName = removeRequest.FileLink.Replace(baseUrl + "/Upload/", "");

            if (System.IO.File.Exists(_environment.WebRootPath + "\\Upload\\" + fileName))
            {
                System.IO.File.Delete(_environment.WebRootPath + "\\Upload\\" + fileName);

            }
            else
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "File not exist" };
                throw new HttpResponseException(err);
            }

            return new Response<bool>(true);

        }
    }
}