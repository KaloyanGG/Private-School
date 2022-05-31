using PrivateSchool.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetAllSubjects();
        Task<Subject> GetSubjectByName(string name);
        Task<bool> Contains(string name);
        Task AddSubject(Subject subject);
        Task DeleteByName(string name);
    }
}
