using AutoMapper;
using CakeStore.BL.Interfaces;
using CakeStore.DL.Interfaces;
using CakeStore.Models.Models;
using CakeStore.Models.Requests.Identity;
using CakeStore.Models.Responses.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CakeStore.BL.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(
            IIdentityRepository identityRepository,
            IMapper mapper,
            ILogger<IdentityService> logger)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetUserByUserNameResponse> FindByUserName(string userName)
        {
            try
            {
                var user = await _identityRepository.FindByUserName(userName);
                var userResponse = _mapper.Map<GetUserByUserNameResponse>(user);

                return userResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(Add),
                    nameof(IdentityService),
                    ex.Message);

                throw;
            }
        }

        public async Task Add(AddUserRequest addUserRequest)
        {
            try
            {
                var user = _mapper.Map<User>(addUserRequest);

                await _identityRepository.Add(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(Add),
                    nameof(IdentityService),
                    ex.Message);

                throw;
            }
        }

        public string GenerateJwtToken(string userId, string userName, string secret)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                var encryptedToken = tokenHandler.WriteToken(token);

                return encryptedToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An exception occurred in the {ActionName} action of {ServiceName}: {ErrorMessage}",
                    nameof(GenerateJwtToken),
                    nameof(IdentityService),
                    ex.Message);

                throw;
            }
        }
    }
}
