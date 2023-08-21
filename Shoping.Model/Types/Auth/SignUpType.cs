using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Auth
{
    public class SignUpType
    {
        [Required]
        public string UserName { get; set; }
        [Required]

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class SignUpResponse
    {

        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }

}
