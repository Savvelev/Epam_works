using AutoMapper;
using DataAccess.DbEntities;
using Domain.Entities;

namespace DataAccess
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Homework, HomeworkDb>()
                .ReverseMap();

            CreateMap<Lector, LectorDb>()
                .ReverseMap();


            CreateMap<Lecture, LectureDb>()
                .ReverseMap();

            CreateMap<Student, StudentDb>()
                .ReverseMap();

            CreateMap<Attendance, AttendanceDb>()
                .ReverseMap();
        }
    }
}
