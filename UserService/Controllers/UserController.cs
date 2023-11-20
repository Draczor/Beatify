using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.DbContexts;
using UserService.Models;
using UserService.Producer;
using UserService.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;
        //private readonly IRabbitMQProducer _rabbitMQProducer;
        //public readonly IPublishEndpoint publishEndpoint;
        private readonly ISendEndpointProvider sendEndpointProvider;

        public UserController(UserContext context, ISendEndpointProvider sendEndpointProvider)
        {
            _context = context;
            this.sendEndpointProvider = sendEndpointProvider;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:users"));

            await endpoint.Send<User>(new
            {
                Id = 35,
                Name = "Message test",
                Email = "Message@rabbitmq.nl",
                Password = "pwdformessage"
            });

            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //_rabbitMQProducer.SendMessage(user);

            return Ok(new { id = user.Id });
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, User user)
        {
            if (user == null)
            {
                return NotFound();
            }

            var userToUpdate = await _context.Users.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();

            if (userToUpdate == null)
            {
                return NotFound();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            try
            {
                if (user != null)
                {
                    _context.Remove(_context.Users.Single(a => a.Id == id));
                    _context.SaveChanges();
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Something went worng, can't find user");
                }
            }
            catch
            {
                return BadRequest("something went wrong with deleting the user!");
            }
        }
    }
}
