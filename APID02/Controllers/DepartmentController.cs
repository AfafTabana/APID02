using APID02.DTOS;
using APID02.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APID02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        ITIContext db;
        IMapper mapper;
        public DepartmentController(ITIContext _db, IMapper _mapper)
        {

            db = _db;
            mapper = _mapper;

        }

        [HttpGet]
        [Authorize]
        public ActionResult GetAll()
        {
            List<Department> depts = db.Departments.ToList();

            List<DepartmentDataDTO> deptDto = mapper.Map<List<DepartmentDataDTO>>(depts);

            return Ok(deptDto);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public ActionResult GetByid(int id)
        {


            Department dept = db.Departments.FirstOrDefault(e => e.Dept_Id == id);
            if (dept == null) return NotFound();

            DepartmentDataDTO depts = mapper.Map<DepartmentDataDTO>(dept);

            return Ok(depts);

        }

        [HttpGet("{name:alpha}")]
        [Authorize]
        public ActionResult GetByName(string name)
        {

            Department dept = db.Departments.FirstOrDefault(e => e.Dept_Name == name);
            if (dept == null) return NotFound();

            DepartmentDataDTO depts = mapper.Map<DepartmentDataDTO>(dept);
            return Ok(depts);

        }
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Authorize]
        public ActionResult Add(AddDepartmentDTO department)
        {

            if (department == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Department depts = new Department()
            {
                Dept_Name = department.Dept_Name,
                Dept_Location = department.Dept_Location,
                Dept_Desc = department.Dept_Desc,
                Dept_Manager = department.Dept_Manager,
                Manager_hiredate = department.Manager_hiredate


            };

            db.Departments.Add(depts);
            db.SaveChanges();
            return CreatedAtAction("Add", new { id = depts.Dept_Id }, department);


        }

        [HttpPut]
        [Authorize]
        public ActionResult Edit(int id, Department dept)
        {

            if (dept == null) return BadRequest();
            if (id != dept.Dept_Id) return BadRequest();

            if (ModelState.IsValid)
            {
                db.Entry(dept).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

                return NoContent();
            }
            else return BadRequest(ModelState);

        }

        [HttpDelete]
        [Authorize]
        public ActionResult Delete(int id)
        {

            Department dept = db.Departments.FirstOrDefault(e => e.Dept_Id == id);
            if (dept == null) return NotFound();
            db.Departments.Remove(dept);
            db.SaveChanges();
            return Ok(dept);
        }

        [HttpGet("SearchByName/{name}")]
        [Authorize]

        public ActionResult SearchByName(string name)
        {
            List<Department> departments = db.Departments.Where(e => e.Dept_Name == name).ToList();
            List<DepartmentDataDTO> depts = mapper.Map<List<DepartmentDataDTO>>(departments);
            return Ok(depts
                );
        }

        [HttpGet("paginated")]
        [Authorize]
        public IActionResult GetPaginatedStudents(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest("Page number and page size must be greater than 0.");

            var totalRecords = db.Departments.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var depts = db.Departments
                .OrderBy(s => s.Dept_Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            List<DepartmentDataDTO> departments = mapper.Map<List<DepartmentDataDTO>>(depts);

            var result = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Data = departments
            };

            return Ok(result);
        }
    }
}

