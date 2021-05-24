using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praktice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public async Task<ActionResult<IEnumerable<Message>>> Get()
        {
            using (PrakticeContext prt = new PrakticeContext())
            {
                List<Message> messages = await prt.Messages.ToListAsync();
                List<string> messagesJSON = new List<string>();
                foreach (Message m in messages)
                {
                    string newJSON = JsonConvert.SerializeObject(m);
                    messagesJSON.Add(newJSON);
                }

                return new ObjectResult(messagesJSON);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> Get(int id)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {
                List<Message> message = await prt.Messages.Where(x => x.Author == id).ToListAsync();
                List<string> messagesJSON = new List<string>();
                if (message is null)
                {
                    return NotFound();
                }
                foreach (Message m in message)
                {
                    string foundMessageJSON = JsonConvert.SerializeObject(message);
                    messagesJSON.Add(foundMessageJSON);
                }
                
                return new ObjectResult(messagesJSON);
            }
        }




        [HttpPost]
        public async Task<ActionResult<Message>> Post(string newMessageJSON)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {


                Message newMessage = JsonConvert.DeserializeObject<Message>(newMessageJSON);
                if (newMessage is null)
                {
                    return BadRequest();
                }
                prt.Messages.Add(newMessage);
                await prt.SaveChangesAsync();
                return Ok();

            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Message>> Delete(int id)
        {
            using (PrakticeContext prt = new PrakticeContext())
            {

                Message foundMessage = await prt.Messages.FindAsync(id);
                if (foundMessage is null)
                {
                    return NotFound();
                }
                prt.Messages.Remove(foundMessage);
                await prt.SaveChangesAsync();
                return Ok(foundMessage);


            }
        }
    }
}
