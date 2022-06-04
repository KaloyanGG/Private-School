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
            CreateMap<Student, StudentReturnModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<User, FullInfoUserReturnModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest=> dest.Role, opt=>opt.Ignore());
            CreateMap<Teacher, TeacherReturnModel>()
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest=>dest.Username, opt => opt.MapFrom(src=>src.User.UserName));
        }

    }
}
