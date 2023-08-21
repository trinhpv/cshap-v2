using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Auth
{
    public class ChangePasswordType : SignInType
    {
        [Required]
        public required string NewPassword { get; set; }
        
    }
    public class RequestChangePasswordType 
    {
        [Required]
        public required string NewPassword { get; set; }
        [Required]
        public required string OldPassword { get; set; }
    }
}
