using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Product
{
    public class CreateProductRequest
    {
        [Required]
        public required string Name { get; set; } = null!;
        [Required]
        public required string Description { get; set; } = null!;
        [Required]
        public required float Price { get; set; }
        public string FirstImage { get; set; } = "";
        public string SecondImage { get; set; } = "";
        public string ThirdImage { get; set; } = "";
        public string LastImage { get; set; } = "";
        [Required]
        public required int CategoryId { get; set; }
    }
}
