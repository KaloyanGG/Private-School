using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PrivateSchool.Data;
using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using PrivateSchool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PrivateSchool.Services
{
    public class UserService : IUserService
    {
        private readonly JWTSettings _jwtSettings;

        private readonly SignInManager<User> _signInManager;

        private readonly UserManager<User> _userManager;

        private readonly PrivateSchoolDBContext _db;

        private readonly IMapper _mapper;

        public UserService(IOptions<JWTSettings> jwtSettings, SignInManager<User> signInManager, UserManager<User> userManager, PrivateSchoolDBContext db, IMapper mapper)
        {
            _jwtSettings = jwtSettings.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
            _mapper = mapper;
        }

        public async Task<UserReturnModel> Login(string username, string password)
        {
            SignInResult loginResult = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (!loginResult.Succeeded)
            {
                return null;
            }
            var user = await _userManager.FindByNameAsync(username);

            return await SetUpUserModel(user);
        }

        public async Task<UserReturnModel> Register(RegisterBindingModel model)
        {
            User user = _mapper.Map<User>(model);

            IdentityResult identityResult = await _userManager.CreateAsync(user,model.Password);

            await _signInManager.UserManager.AddToRoleAsync(user, StaticData.Roles[model.Type]);

            if (!identityResult.Succeeded)
            {
                return null;
            }
            await _signInManager.SignInAsync(user, false);
            return await SetUpUserModel(user);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<UserReturnModel> SetUpUserModel(User user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id),
            };

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Authentication successful so generate jwt token
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserReturnModel
            {
                Username = user.UserName,
                Token = tokenHandler.WriteToken(token),
                Role = roles.Count() == 1 ? roles[0] : ""
            };
        }
    }
}
