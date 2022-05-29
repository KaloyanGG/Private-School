using PrivateSchool.Entities;
using PrivateSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjects();
    }
}
