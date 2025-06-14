using APID02.DTOS;
using APID02.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;

namespace APID02.MapperConfig
{
    public class mapconfig :Profile
    {
        public mapconfig()
        {

            CreateMap<Student, StudentDataDTO>().AfterMap((src, dest) => {
                dest.Dept_Name = src.Dept?.Dept_Name;  // safe navigation
                dest.Super_Name = src.St_superNavigation?.St_Fname;  // safe navigation
            }).ReverseMap();

            CreateMap<Student, AddStudentData>().AfterMap((src, dest) => {
                dest.Dept_id = src.Dept_Id;
                dest.Super_id = src.St_super;
            }).ReverseMap();

            CreateMap<Department, DepartmentDataDTO>().AfterMap((src, dest) =>

            {
                dest.Students_count = src.Students.Count();
            }).ReverseMap();

            CreateMap<AddDepartmentDTO, Department>()
                 .ForMember(dest => dest.Dept_Id, opt => opt.Ignore());
        }
    }
}
