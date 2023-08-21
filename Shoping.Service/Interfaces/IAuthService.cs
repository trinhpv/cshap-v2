using Shoping.Model.Types.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Interfaces
{
    public interface IAuthService
    {
        SignUpResponse SignUp(SignUpType user);
        SignInResponse SignIn(SignInType user);
        RefreshTokenResponse RefreshToken(string refreshToken);
        Boolean ChangePassword(ChangePasswordType changePassInfo);
    }
}
