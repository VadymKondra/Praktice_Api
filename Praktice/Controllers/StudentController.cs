
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praktice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            using (PrakticeContext prt = new PrakticeContext())
            {
                List<Student> students = await prt.Students.ToListAsync();
                List<string> studentsJSON = new List<string>();
                foreach (Student t in students)
                {
                    string newJSON = JsonConvert.SerializeObject(t);
                    studentsJSON.Add(newJSON);
                }

                return new ObjectResult(studentsJSON);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {
                Student student = await prt.Students.FirstOrDefaultAsync(x => x.Id == id);
                if (student is null)
                {
                    return NotFound();
                }
                string foundStudentJSON = JsonConvert.SerializeObject(student);
                return new ObjectResult(foundStudentJSON);
            }
        }




        [HttpPost]
        public async Task<ActionResult<Student>> Post(string newStudentJSON)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {


                Student newStudent = JsonConvert.DeserializeObject<Student>(newStudentJSON);
                if (newStudent is null)
                {
                    return BadRequest();
                }
                prt.Students.Add(newStudent);
                await prt.SaveChangesAsync();
                return Ok();

            }
        }
        [HttpPut]
        public async Task<ActionResult<Student>> Put(string studentJSON)
        {
            if (studentJSON == null)
            {
                return BadRequest();
            }
            Student student = JsonConvert.DeserializeObject<Student>(studentJSON);
            using (PrakticeContext prt = new PrakticeContext())
            {
                if (!prt.Students.Any(x => x.Id == student.Id))
                {
                    return NotFound();
                }

                prt.Update(student);
                await prt.SaveChangesAsync();
                return Ok(student);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {

                Student foundStudent = await prt.Students.FindAsync(id);
                if (foundStudent is null)
                {
                    return NotFound();
                }
                prt.Students.Remove(foundStudent);
                await prt.SaveChangesAsync();
                return Ok(foundStudent);


            }
        }
    }
}
