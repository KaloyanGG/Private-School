using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            string userType = StaticData.Roles[model.Type];

            User user = _mapper.Map<User>(model);
            IdentityResult identityResult = await _userManager.CreateAsync(user, model.Password);

            await _signInManager.UserManager.AddToRoleAsync(user, userType);

            switch (userType)
            {
                case "Student":
                    await _db.Students.AddAsync(new Student { UserId = user.Id, Classes = new List<Class>() });
                    break;
                case "Teacher":
                    await _db.Teachers.AddAsync(new Teacher { UserId = user.Id });
                    break;
            }

            await _db.SaveChangesAsync();

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

        public async Task<Student> GetStudentById(int id)
        {
            return await _db.Students.Where(s => s.Id == id).FirstOrDefaultAsync();
        }


        public async Task<User> GetUserByUsername(string username)
        {
            return await _db.Users.Where(u => u.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<FullInfoUserReturnModel> DeleteUserById(string id)
        {
            var user = await _db.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            if (_db.Students.Include(s => s.User).Select(s => s.UserId).ToList().Contains(user.Id))
            {
                var s = _db.Students.Where(s => s.UserId == user.Id).FirstOrDefault();
                _db.Students.Remove(s);
            }
            else
            {
                var s = _db.Teachers.Where(s => s.UserId == user.Id).FirstOrDefault();
                _db.Teachers.Remove(s);
            }
            await _db.SaveChangesAsync();
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return _mapper.Map<FullInfoUserReturnModel>(user);

        }

        public async Task<TeacherReturnModel> AddSubjectByIdToTeacher(string userId, int subjectId)
        {

            var teacher = _db.Teachers
                .Where(t => t.UserId == userId).FirstOrDefault();
            teacher.SubjectId = subjectId;
            await _db.SaveChangesAsync();

            return _mapper.Map<TeacherReturnModel>(_db.Teachers.Where(t => t.UserId == userId)
                                 .Include(t => t.User)
                                 .Include(t => t.Subject)
                                 .FirstOrDefault());

        }

        public async Task<FullInfoUserReturnModel> Update(UpdateUserBindingModel user, string id, string role)
        {
            User existingUser = _db.Users.Find(id);
            existingUser.EGN = user.EGN;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.DateOfBirth = DateTime.Parse(user.DateOfBirth);

            _db.SaveChanges();
            return _mapper.Map<User, FullInfoUserReturnModel>(existingUser, opt =>
                 opt.AfterMap((src, dest) => dest.Role = role));

        }

        public async Task<FullInfoTeacherModel> UpdateTeacher(UpdateTeacherBindingModel user, string id)
        {
            Teacher existingTeacher = _db.Teachers.Include(t => t.User).Where(t => t.UserId == id).FirstOrDefault();
            User existingUser = _db.Users.Find(id);
            existingUser.EGN = user.EGN;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.DateOfBirth = DateTime.Parse(user.DateOfBirth);
            existingTeacher.Level = user.Level;

            _db.SaveChanges();
            return _mapper.Map<Teacher, FullInfoTeacherModel>(existingTeacher, opt =>
           opt.AfterMap((src, dest) => dest.Role = "Teacher"));

        }

        public async Task<FullInfoStudentModel> UpdateStudent(UpdateStudentBindingModel user, string id)
        {
            Student existingStudent = _db.Students.Include(t => t.User).Where(t => t.UserId == id).FirstOrDefault();
            User existingUser = _db.Users.Find(id);
            existingUser.EGN = user.EGN;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.DateOfBirth = DateTime.Parse(user.DateOfBirth);
            existingStudent.AverageGrade = user.AverageGrade;

            _db.SaveChanges();
            return _mapper.Map<Student, FullInfoStudentModel>(existingStudent, opt =>
                    opt.AfterMap((src, dest) => dest.Role = "Student"));
        }
    }
}
