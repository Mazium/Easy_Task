using AutoMapper;
using Easy_Task.Application.DTOs;
using Easy_Task.Domain.Entities;

namespace Easi_TaskDemo.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}
