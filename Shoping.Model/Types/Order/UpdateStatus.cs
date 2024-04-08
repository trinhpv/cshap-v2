using Shopping.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Order
{
    public class UpdateStatusRequest
    {
        [Required]
        public PaymentStatus Status { get; set; }
    }
}
