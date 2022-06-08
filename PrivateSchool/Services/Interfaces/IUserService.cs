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
        Task<FullInfoUserReturnModel> Update(UpdateUserBindingModel user, string id, string role);
        Task<User> GetUserByUsername(string username);
        Task<FullInfoUserReturnModel> DeleteUserById(string id);
        Task<TeacherReturnModel> AddSubjectByIdToTeacher(string name, int subjectId);
        Task<FullInfoTeacherModel> UpdateTeacher(UpdateTeacherBindingModel model, string name);
        Task<FullInfoStudentModel> UpdateStudent(UpdateStudentBindingModel model, string name);
    }
}
