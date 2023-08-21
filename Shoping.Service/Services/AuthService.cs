using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shoping.Model.Types.Auth;
using Shopping.Data.Entities;
using Shopping.Data.Repositories.Interfaces;
using Shopping.Service.Interfaces;
using Shopping.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuratio)
        {
            _userRepository = userRepository;
            _configuration = configuratio;

        }


        public SignUpResponse SignUp(SignUpType user)
        {
            var curruser = _userRepository.FindUserName(user.UserName);
            if (curruser != null)
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "username is current in use" };
                throw new HttpResponseException(err);
            }
            var userData = new UserEntity
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Role = RoleType.User
            };
            _userRepository.Insert(userData);
            var newUser =  _userRepository.FindUserName(user.UserName);
            var accessToken = GenerateToken(newUser, TokenType.AccessToken);
            var refreshToken = GenerateToken(newUser, TokenType.RefreshToken);
            return new SignUpResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
        public Boolean ChangePassword(ChangePasswordType changePasswordInfo)
        {
            var currentUser = _userRepository.FindUserName(changePasswordInfo.UserName);
            if (currentUser == null)
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "This user is not available" };
                throw new HttpResponseException(err);
            }
            var isTruePass = BCrypt.Net.BCrypt.Verify(changePasswordInfo.Password, currentUser.Password);
            if (!isTruePass)
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "Wrong old passwor" };
                throw new HttpResponseException(err);
            }
            currentUser.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordInfo.NewPassword);
            _userRepository.Update(currentUser);
            return true;
        }
        public SignInResponse SignIn(SignInType user)
        {
            var currentUser = _userRepository.FindUserName(user.UserName);
            if (currentUser == null)
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "wrong username" };
                throw new HttpResponseException(err);
            }
            var isTruePass = BCrypt.Net.BCrypt.Verify(user.Password, currentUser.Password);

            if (!isTruePass)
            {
                var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "wrong password" };
                throw new HttpResponseException(err);
            }
            var accessToken = GenerateToken(currentUser, TokenType.AccessToken);
            var refreshToken = GenerateToken(currentUser, TokenType.RefreshToken);
            return new SignInResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
        public RefreshTokenResponse RefreshToken(string refreshToken)
        {
            var validatedUser = ValidateRefreshToken(refreshToken);
            if (validatedUser != null)
            {
                return new RefreshTokenResponse
                {
                    NewAccessToken = GenerateToken(validatedUser, TokenType.AccessToken),
                };
            }
            var err = new ExType { Status = HttpStatusCode.BadRequest, Message = "wrong token" };
            throw new HttpResponseException(err);
        }
        private string GenerateToken(UserEntity user, TokenType type)
        {
            int expMinutes;
            if (type == TokenType.RefreshToken)
            {
                expMinutes = _configuration.GetValue<int>("Jwt:RefreshTokenExpTime");
            }
            else
            {
                expMinutes = _configuration.GetValue<int>("Jwt:AccessTokenExpTime");
            }

            var issuer = _configuration.GetValue<string>("Jwt:Issuer");
            var audience = _configuration.GetValue<string>("Jwt:Audience");
            var key = Encoding.ASCII.GetBytes
            (_configuration.GetValue<string>("Jwt:Key"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("Type", type.ToString()),
                        new Claim("UserName", user.UserName),
                        new Claim("UserEmail", user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,
                                Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                        }),
                Expires = DateTime.UtcNow.AddMinutes(expMinutes),
                Issuer = issuer,
                Audience = audience,
                TokenType = type.ToString(),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
        private UserEntity? ValidateRefreshToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var typeToken = (TokenType)Enum.Parse(typeof(TokenType), jwtToken.Claims.First(x => x.Type == "Type").Value.ToString());

                if (typeToken == TokenType.RefreshToken)
                {
                    var result = new UserEntity
                    {
                        Id = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value),
                        UserName = jwtToken.Claims.First(x => x.Type == "UserName").Value.ToString(),
                        Email = jwtToken.Claims.First(x => x.Type == "UserEmail").Value.ToString(),
                        Role = (RoleType)Enum.Parse(typeof(RoleType), jwtToken.Claims.First(x => x.Type == "role").Value.ToString())
                    };
                    return result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
