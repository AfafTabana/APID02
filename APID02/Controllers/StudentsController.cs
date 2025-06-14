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
    public class StudentsController : ControllerBase
    {
        //ITIContext db; 
        IMapper mapper;
        ITIContext db;
        public StudentsController(ITIContext _db , IMapper _mapper) { 
        
           db = _db;
           mapper = _mapper;

        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Student> students = db.Students.ToList();

            List<StudentDataDTO> studentDataDTOs = mapper.Map<List<StudentDataDTO>>(students);

            return Ok(studentDataDTOs);
        }

        [HttpGet("{id:int}")]

        public ActionResult GetByid(int id) {
          

            Student student = db.Students.FirstOrDefault(e=>e.St_Id == id);
            if (student == null) return NotFound(); 

            StudentDataDTO sts = mapper.Map<StudentDataDTO>(student);   

            return Ok(sts);
        
        }

        [HttpGet("{name:alpha}")]

        public ActionResult GetByName(string name) {

            Student student = db.Students.FirstOrDefault(e => e.St_Fname == name); 
            if (student == null) return NotFound();

            StudentDataDTO sts = mapper.Map<StudentDataDTO>(student);
            return Ok(sts);
        
        }
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [Authorize]
        public ActionResult Add(AddStudentData student) { 

            if (student == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Student sts = new Student() {
             St_Fname = student.St_Fname,
             St_Age = student.St_Age,
             St_Address = student.St_Address,
             St_Lname = student.St_Lname , 
             St_super = student.Super_id  ,
             Dept_Id = student.Dept_id
            
            
            };

            db.Students.Add(sts);
            db.SaveChanges();
            return CreatedAtAction("Add", new { id = sts.St_Id }, student);


        }

        [HttpPut]
        [Authorize]
        public ActionResult Edit(int id, Student student) {

            if (student == null) return BadRequest();
            if (id != student.St_Id) return BadRequest();

            if (ModelState.IsValid)
            {
                db.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();

                return NoContent();
            }
            else return BadRequest(ModelState);

        }

        [HttpDelete]
        [Authorize]
        public ActionResult Delete(int id) { 
        
            Student  sts = db.Students.FirstOrDefault(e=>e.St_Id == id);
            if (sts == null) return NotFound();
            db.Students.Remove(sts);
            db.SaveChanges();
            return Ok(sts);
        }

        [HttpGet("SearchByName/{name}")]

        public ActionResult SearchByName(string name)
        {
            List<Student> students = db.Students.Where(e=>e.St_Fname==name).ToList();

            return Ok(students);
        }

        [HttpGet("paginated")]
        public IActionResult GetPaginatedStudents(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest("Page number and page size must be greater than 0.");

            var totalRecords = db.Students.Count();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var students = db.Students
                .OrderBy(s => s.St_Id) 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var result = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Data = students
            };

            return Ok(result);
        }

    }
}
