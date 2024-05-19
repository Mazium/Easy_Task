using AutoMapper;
using Easy_Task.Application.DTOs;
using Easy_Task.Domain.Entities;

namespace Easi_TaskDemo.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>()
             .ForMember(dest => dest.AppUserId, opt => opt.Ignore()); 
            CreateMap<Employee, EmployeeDto>();
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();

        }
    }
}
