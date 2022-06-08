using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Services.Interfaces
{
    public interface IClassService
    {
        Task<ClassReturnModel> Add(AddClassBindingModel model);
        Task<List<ClassReturnModel>> GetAllClasses();
        Task<ClassReturnModel> GetClassReturnModelById(int id);
        Task<ClassReturnModel> DeleteClassByName(string name);
        Task<Class> GetClassById(int v);
        Task<object> AddStudentToAClass(string userId, Class classs);
        Task<List<StudentReturnModel>> GetAllStudentsByClassId(int id);
        Task<Class> GetClassByName(string name);
        Task<Class> updateClass(Class classs);
    }
}
