using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Repositories;
using PrivateSchool.Repositories.Interfaces;
using PrivateSchool.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        public async Task<List<Subject>> GetAllSubjects()
        {
            List<Subject> result = await _subjectRepository.GetAllSubjects();
            
            return result;
        }
    }
}
