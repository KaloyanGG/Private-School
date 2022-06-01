using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjects();
        Task<Subject> GetSubjectByName(string name);
        Task<Subject> Add(AddSubjectBindingModel model);
        Task<Subject> DeleteSubjectByName(string name);
        Task<Subject> updateSubject(Subject subject);
    }
}
