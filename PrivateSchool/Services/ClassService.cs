using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task<ClassReturnModel> GetClassReturnModelById(int id)
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
            if(_db.Classes.Include(c => c.Teacher).Any(c=>c.Name == model.Name))
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

        public async Task<ClassReturnModel> DeleteClassByName(string name)
        {
            var contains =  (await _db.Classes.Select(s => s.Name).ToListAsync()).Contains(name);

            if (!contains)
            {
                return null;
            }
            var classs = await _db.Classes.Where(s => s.Name == name).FirstOrDefaultAsync();

            foreach (var c in _db.Classes.ToList())
            {
                if (c.Name == name)
                {
                    _db.Classes.Remove(c);
                    break;
                }
            }
            await _db.SaveChangesAsync();
            var result = _mapper.Map<ClassReturnModel>(classs);
            
            return result;

        }

        public async Task<Class> GetClassById(int id)
        {
            return await _db.Classes.Where(c=>c.Id==id).FirstOrDefaultAsync();
        }


        public async Task<object> AddStudentToAClass(Student student, Class classs)
        {
            var stud = _db.Students.Include(s=>s.Classes).Where(s => s.Id == student.Id).FirstOrDefault();
            // CLASSES = NULL!
            stud.Classes.Add(classs);
            await _db.SaveChangesAsync();
            return new
            {
                AllClassIdsForTheStudent = (await _db.Students.Where(s => s.Id == student.Id).FirstOrDefaultAsync()).Classes.Select(c=>new
                {
                    c.Id, c.Name
                }),
            };
        }

        public async Task<List<StudentReturnModel>> GetAllStudentsByClassId(int id)
        {

           // return (await _db.Classes.Where(c => c.Id == id).FirstOrDefaultAsync()).Students.ToList();

            var classs = await _db.Classes.Include(c=>c.Students).ThenInclude(s=>s.User).Where(c => c.Id == id).FirstOrDefaultAsync();

            return classs.Students.Select(s => new StudentReturnModel()
            {
                Id = s.Id,
                Username = s.User.UserName
            }).ToList();

        }

        public async Task<Class> GetClassByName(string name)
        {
            return await _db.Classes.Where(c => c.Name == name).FirstOrDefaultAsync();

        }

        public async Task<Class> updateClass(Class classs)
        {
            Class classs1 = _db.Classes.Where(s => s.Id == classs.Id).FirstOrDefault();
            classs1 = classs;
            await _db.SaveChangesAsync();
            return await GetClassByName(classs.Name);
        }
    }
}
