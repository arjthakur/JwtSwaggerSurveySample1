using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Survey.DTOs.Intern;
using Survey.DTOs.Request;
using Survey.DTOs.Response;
using Survey.Entities;
using Survey.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsers();
        Task<UserDto> Login(Login login);
        List<PageDto> GetPages();
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly TokenManagement tokenManagement;
        private readonly IMapper mapper;

        public UserService(IUserRepository UserRepository, TokenManagement tokenManagement, IMapper mapper)
        {
            userRepository = UserRepository;
            this.tokenManagement = tokenManagement;
            this.mapper = mapper;
        }
        public async Task<List<UserDto>> GetUsers()
        {
            var users = await userRepository.GetUsers();
            return mapper.Map<List<UserDto>>(users);
        }
        public List<PageDto> GetPages()
        {
            return mapper.Map<List<PageDto>>(userRepository.GetPages());
        }

        public async Task<UserDto> Login(Login login)
        {
            ApplicationUser user = await userRepository.GetLogin(login.Username, login.Password);
            if (user != null)
            {
                var userRoles = await userRepository.GetRoles(user).ConfigureAwait(false);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenManagement.Secret));
                var Expiration = DateTime.Now.AddHours(3);
                var jwtToken = new JwtSecurityToken(
                    issuer: tokenManagement.Issuer,
                    audience: tokenManagement.Audience,
                    expires: Expiration,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Roles = userRoles.ToList(),
                    Expiration = Expiration,
                    Token = token
                };
            }
            return null;
        }
    }
}
