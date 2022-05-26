using AutoMapper;
using PrivateSchool.Entities;
using PrivateSchool.Models;
using PrivateSchool.Models.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateSchool
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<RegisterBindingModel, User>();
            CreateMap<Class, ClassReturnModel>()
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src=>src.Teacher.User.UserName))
                .ForMember(dest =>dest.SubjectName, opt=>opt.MapFrom(src=>src.Teacher.Subject.Name));
        }

    }
}
