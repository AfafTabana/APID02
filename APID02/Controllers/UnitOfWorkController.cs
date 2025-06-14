using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APID02.UnitOfWorks;
using APID02.Models;
using AutoMapper;
using APID02.DTOS;

namespace APID02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfWorkController : ControllerBase
    {

        UnitOfWork unit;
        IMapper mapper;



        public UnitOfWorkController(UnitOfWork _unit , IMapper _mapper) { 
        

            this.unit = _unit;
           this.mapper = _mapper;
        }

        [HttpGet]
        public ActionResult GetAll() { 
            List<Student> s = unit.StudReps.getall();
            List<Department> d =unit.DeptReps.getall();
            List<StudentDataDTO> studentDataDTOs = mapper.Map<List<StudentDataDTO>>(s);

            return Ok(studentDataDTOs);
        
        }

        [HttpGet("{id}")]
        public ActionResult GetbyId(int id) { 
           Student s = unit.StudReps.getbyid(id);
            StudentDataDTO student =mapper.Map<StudentDataDTO>(s);
            return Ok(student);
       
        }
        [HttpGet("department/{id}")]
       
        public ActionResult GetDeptbyId(int id)
        {
            Department d = unit.DeptReps.getbyid(id);
            DepartmentDataDTO department = mapper.Map<DepartmentDataDTO>(d);
            return Ok(department);

        }

        [HttpPut("student")]
        public IActionResult Edit(int id , [FromBody] Student student) {

            if (student == null) return BadRequest();
            if (id != student.St_Id) return BadRequest();
            unit.StudReps.edit(id ,student);

            unit.save();

            return Ok();
        
        }

        [HttpPut("department")]
        public IActionResult EditDept( int id , [FromBody] Department department)
        {



            if (department == null) return BadRequest();
            if (id != department.Dept_Id) return BadRequest();

            unit.DeptReps.edit(id , department);

            unit.save();

            return Ok();

        }

        [HttpPost("student")]
        public IActionResult add([FromBody] AddStudentData student)
        {
            Student sts = new Student()
            {
                St_Fname = student.St_Fname,
                St_Age = student.St_Age,
                St_Address = student.St_Address,
                St_Lname = student.St_Lname,
                St_super = student.Super_id,
                Dept_Id = student.Dept_id


            };
            unit.StudReps.add(sts);
          
            unit.save();    
            return Ok();
        }

        [HttpPost("Department")]
        public IActionResult addDepartment([FromBody] AddDepartmentDTO department)
        {
            if (department == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Department depts = new Department()
            {
                Dept_Id = department.Dept_Id,
                Dept_Name = department.Dept_Name,
                Dept_Location = department.Dept_Location,
                Dept_Desc = department.Dept_Desc,
                Dept_Manager = department.Dept_Manager,
                Manager_hiredate = department.Manager_hiredate


            };
           
            unit.DeptReps.add(depts);
            unit.save();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id) { 

            unit.StudReps.delete(id);

            unit.save();
            return Ok();
        
        
        }


        [HttpDelete("department/{id}")]
        public IActionResult deleteDept(int id)
        {

           
            unit.DeptReps.delete(id);
            unit.save();
            return Ok();


        }
    }
}
