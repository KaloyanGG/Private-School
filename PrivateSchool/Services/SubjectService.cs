using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
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

        public async Task<Subject> Add(AddSubjectBindingModel model)
        {
            if (await _subjectRepository.Contains(model.Name))
            {
                return null;
            }

            await _subjectRepository.AddSubject(new Subject
            {
                Name = model.Name,
                MaxCapacity = model.MaxCapacity
            });


            return new Subject
            {
                Name = model.Name,
                MaxCapacity = model.MaxCapacity
            };
        }

        public async Task<Subject> DeleteSubjectByName(string name)
        {
            if (!await _subjectRepository.Contains(name))
            {
                return null;
            }
            var subject = await _subjectRepository.GetSubjectByName(name);

            await _subjectRepository.DeleteByName(name);

            return subject;

        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            List<Subject> result = await _subjectRepository.GetAllSubjects();

            return result;
        }

        public async Task<Subject> GetSubjectByName(string name)
        {

            return await _subjectRepository.GetSubjectByName(name);


        }

        public async Task<Subject> updateSubject(Subject subject)
        {
            await _subjectRepository.UpdateSubject(subject);

            return await _subjectRepository.GetSubjectByName(subject.Name);
        }
    }
}
