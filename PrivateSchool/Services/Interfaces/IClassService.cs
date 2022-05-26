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
        Task<ClassReturnModel> GetClassById(int id);


    }
}
