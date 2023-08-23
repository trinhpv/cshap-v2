using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Upload
{
    public class FileUploadAPI
    {
        [Required]
        public required IFormFile imgFile { get; set; }
    }
    public class UploadResponse
    {
        public required string FileLink { get; set; }
    }
}
