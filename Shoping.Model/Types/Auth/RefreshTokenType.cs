using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Model.Types.Auth
{
    public class RefreshTokenRequest
    {

        public required string RefreshToken { get; set; }
    }
    public class RefreshTokenResponse
    {

        public required string NewAccessToken { get; set; }
    }
}
