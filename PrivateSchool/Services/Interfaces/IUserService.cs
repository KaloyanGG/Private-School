using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserReturnModel> Login(string username, string password);
        Task<UserReturnModel> Register(RegisterBindingModel model);
        Task Logout();
        Task<Student> GetStudentById(int id);
        Task<FullInfoUserReturnModel> UpdateUser(User user);
        Task<User> GetUserByUsername(string username);
        Task<FullInfoUserReturnModel> DeleteUserById(string id);
    }
}
