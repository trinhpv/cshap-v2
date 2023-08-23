using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Upload
{
    public class DeleteUploadedRequest
    {
        [Required] public string FileLink { get; set; }
    }
}
