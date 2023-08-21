using Shoping.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Comment
{
    public class CreateCommentRequest
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
