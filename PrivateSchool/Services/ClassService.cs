using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrivateSchool.Data;
using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using PrivateSchool.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool.Services
{
    public class ClassService : IClassService
    {
        private readonly PrivateSchoolDBContext _db;

        private readonly IMapper _mapper;

        public ClassService(PrivateSchoolDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ClassReturnModel> GetClassById(int id)
        {
            var classs = _db.Classes
                .Include(c => c.Teacher).ThenInclude(c => c.User)
                .Include(c => c.Teacher).ThenInclude(c => c.Subject)
                .Where(c => c.Id == id).FirstOrDefault();

            return _mapper.Map<ClassReturnModel>(classs);
        }

        public async Task<List<ClassReturnModel>> GetAllClasses()
        {
            foreach (var item in _db.Teachers.ToList())
            {
                item.SubjectId = 2;
            }
            await _db.SaveChangesAsync();
            
            var classes = _db.Classes
                .Include(c=>c.Teacher).ThenInclude(c=>c.User)
                .Include(c=>c.Teacher).ThenInclude(c=>c.Subject)
                .ToList();


            var res = new List<ClassReturnModel>();
            foreach (var item in classes)
            {
                res.Add(_mapper.Map<ClassReturnModel>(item));
            }

            return res;
        }

        public async Task<ClassReturnModel> Add(AddClassBindingModel model)
        {
            if(_db.Classes.Include(c => c.Teacher).Any(c => c.TeacherId == model.TeacherId && c.Teacher.SubjectId == model.SubjectId))
            {
                return null;
            }
            

            await _db.Classes.AddAsync(new Class
            {
                Name = model.Name,
                TeacherId = model.TeacherId,
            });

            await _db.SaveChangesAsync();

            return new ClassReturnModel
            {
                Name = model.Name,
            };
        }

    }
}
