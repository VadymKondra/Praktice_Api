using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praktice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {

        private readonly ILogger<APIController> _logger;

        public APIController(ILogger<APIController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            using (PrakticeContext prt = new PrakticeContext())
            {
                List<User> users = await prt.Users.ToListAsync();
                List<string> usersJSON = new List<string>();
                foreach (User u in users)
                {
                    string newJSON = JsonConvert.SerializeObject(u);
                    usersJSON.Add(newJSON);
                }

                return new ObjectResult(usersJSON);
            }
        }

        [HttpGet("{parameter}")]
        public async Task<ActionResult<User>> Get(string parameter)
        {
            string[] individualParams = parameter.Split(";");
            using (PrakticeContext prt = new PrakticeContext())
            {
                User foundUser = await prt.Users.FirstOrDefaultAsync(x => x.Email.Equals(individualParams[0]) && x.Password.Equals(individualParams[1]));
                if(foundUser is null)
                {
                    return NotFound();
                }
                string foundUserJSON = JsonConvert.SerializeObject(foundUser);
                return new ObjectResult(foundUserJSON);
            }
        }




        [HttpPost]
        public async Task<ActionResult<User>> Post(string newUserJSON)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {


                User newUser = JsonConvert.DeserializeObject<User>(newUserJSON);
                if (newUser is null)
                {
                    return BadRequest();
                }
                prt.Users.Add(newUser);
                await prt.SaveChangesAsync();
                return Ok();

            }
        }
        [HttpPut]
        public async Task<ActionResult<User>> Put(string userJSON)
        {
            if (userJSON == null)
            {
                return BadRequest();
            }
            User user = JsonConvert.DeserializeObject<User>(userJSON);
            using(PrakticeContext prt = new PrakticeContext())
            {
                if (!prt.Users.Any(x => x.Id == user.Id))
                {
                    return NotFound();
                }

                prt.Update(user);
                await prt.SaveChangesAsync();
                return Ok(user);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {

                User foundUser = prt.Users.Find(id);
                if(foundUser is null)
                {
                    return NotFound();
                }
                prt.Users.Remove(foundUser);
                await prt.SaveChangesAsync();
                return Ok(foundUser);


            }
        }

    }
}
