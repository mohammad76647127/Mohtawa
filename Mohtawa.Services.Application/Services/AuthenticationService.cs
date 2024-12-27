using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Mohtawa.Services.Application.Interfaces;
using Mohtawa.Services.Application.Models.DTOs;
using Mohtawa.Services.Application.Models.Requests.Authentication;
using Mohtawa.Services.Application.Models.Responses.Authentication;
using Mohtawa.Services.Domain.Contracts;
using Mohtawa.Services.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mohtawa.Services.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const int SaltSize = 16; // 128-bit
        private const int KeySize = 32; // 256-bit
        private const int Iterations = 10000; // Number of iterations
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IConfiguration configuration, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        #region Public Methods
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            //check in db the user 
            var user = await _userRepository.GetUserByUserName(loginRequest.UserName);
            if (user == null)
            {
                return new()
                {
                    ErrorMessage = "Invalid Credentials",
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }
            if (VerifyPassword(loginRequest.Password, user.PasswordHashed))
            {
                return new()
                {
                    Token = GenerateToken(user),
                };
            }
            return new()
            {
                ErrorMessage = "Invalid Credentials",
                HttpStatusCode = System.Net.HttpStatusCode.BadRequest
            };
        }
        public async Task<RegisterResponse> Register(RegisterRequest registerRequest)
        {
            var userRepository = _unitOfWork.Repository<User>();
            var isExists = await userRepository.IsExists(x => x.UserName == registerRequest.UserName || x.Email == registerRequest.Email);
            if (isExists)
            {
                return new()
                {
                    Result = false,
                    ErrorMessage = "User Already Exists",
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                };
            }
            await userRepository.AddAsync(new()
            {
                Email = registerRequest.Email,
                CreatedBy = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                PasswordHashed = HashPassword(registerRequest.Password),
                UserId = Guid.NewGuid(),
                UserName = registerRequest.UserName,
            });
            var result = await _unitOfWork.Save();
            if (result <= 0)
            {
                return new()
                {
                    Result=false,
                    ErrorMessage = "error",
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                };
            }
            return new()
            {
                Result = true
            };
        }
        #endregion

        #region Private Methods
        private TokenDTO GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                //we can add the needed claims
            };
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            return new TokenDTO()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                ExpiresOn = DateTime.Now.AddHours(1),
            };

        }
        private string HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            var hash = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hash, 0, SaltSize);
            Array.Copy(key, 0, hash, SaltSize, KeySize);

            return Convert.ToBase64String(hash);
        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword);

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            for (var i = 0; i < KeySize; i++)
            {
                if (hashBytes[SaltSize + i] != key[i])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
