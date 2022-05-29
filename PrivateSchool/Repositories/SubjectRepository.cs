using Microsoft.EntityFrameworkCore;
using PrivateSchool.Data;
using PrivateSchool.Entities;
using PrivateSchool.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly PrivateSchoolDBContext _db;

        public SubjectRepository(PrivateSchoolDBContext db)
        {
            _db = db;
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            return await _db.Subjects.ToListAsync();
        }
    }
}
