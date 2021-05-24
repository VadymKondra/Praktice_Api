using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praktice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        public async Task<ActionResult<IEnumerable<Teacher>>> Get()
        {
            using (PrakticeContext prt = new PrakticeContext())
            {
                List<Teacher> teachers = await prt.Teachers.ToListAsync();
                List<string> teachersJSON = new List<string>();
                foreach (Teacher t in teachers)
                {
                    string newJSON = JsonConvert.SerializeObject(t);
                    teachersJSON.Add(newJSON);
                }

                return new ObjectResult(teachersJSON);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {
                Teacher teacher = await prt.Teachers.FirstOrDefaultAsync(x => x.Id == id);
                if (teacher is null)
                {
                    return NotFound();
                }
                string foundTeacherJSON = JsonConvert.SerializeObject(teacher);
                return new ObjectResult(foundTeacherJSON);
            }
        }




        [HttpPost]
        public async Task<ActionResult<Teacher>> Post(string newTeacherJSON)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {


                Teacher newTeacher = JsonConvert.DeserializeObject<Teacher>(newTeacherJSON);
                if (newTeacher is null)
                {
                    return BadRequest();
                }
                prt.Teachers.Add(newTeacher);
                await prt.SaveChangesAsync();
                return Ok();

            }
        }
        [HttpPut]
        public async Task<ActionResult<Teacher>> Put(string teacherJSON)
        {
            if (teacherJSON == null)
            {
                return BadRequest();
            }
            Teacher teacher = JsonConvert.DeserializeObject<Teacher>(teacherJSON);
            using (PrakticeContext prt = new PrakticeContext())
            {
                if (!prt.Teachers.Any(x => x.Id == teacher.Id))
                {
                    return NotFound();
                }

                prt.Update(teacher);
                await prt.SaveChangesAsync();
                return Ok(teacher);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> Delete(int id)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {

                Teacher foundTeacher = await prt.Teachers.FindAsync(id);
                if (foundTeacher is null)
                {
                    return NotFound();
                }
                prt.Teachers.Remove(foundTeacher);
                await prt.SaveChangesAsync();
                return Ok(foundTeacher);


            }
        }
    }
}
