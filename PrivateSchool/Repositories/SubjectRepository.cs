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

        public async Task AddSubject(Subject subject)
        {
            await _db.Subjects.AddAsync(subject);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Contains(string name)
        {
            return (await _db.Subjects.Select(s => s.Name).ToListAsync()).Contains(name);
        }

        public async Task DeleteByName(string name)
        {/*
            List<Subject> subjects =  _db.Subjects.ToList();

            _db.Subjects.Add(new Subject
            {
                Name = "new",
                MaxCapacity = 10
            });
            */
            //var sbj = await GetSubjectByName(name);
            // Subject sbj = await GetSubjectByName(name);
            foreach (Subject sbj in _db.Subjects.ToList())
            {
                if (sbj.Name == name)
                {
                    _db.Subjects.Remove(sbj);
                    break;
                }
            }
           // _db.Subjects.Remove(_db.Subjects.Where(s => s.Name == name).FirstOrDefault());
            //subjects.Remove(sbj);
            //bool hasChanges = _db.ChangeTracker.HasChanges();
            //int updates = await _db.SaveChangesAsync();
            //_db.Entry(_db.Subjects).State = EntityState.Modified;
            
            await _db.SaveChangesAsync();
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            return await _db.Subjects.ToListAsync();
        }

        public async Task<Subject> GetSubjectByName(string name)
        {
            return await _db.Subjects.Where(s => s.Name == name).FirstOrDefaultAsync();
        }
    }
}
