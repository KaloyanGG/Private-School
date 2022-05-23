using Microsoft.Extensions.Options;
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
    }
}
