using AutoMapper;
using AutoMapper.QueryableExtensions;
using Demokrata.Contracts;
using Demokrata.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demokrata.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersContext _usersContext;
        private readonly IMapper _mapper;

        public UserController(UsersContext usersContext, IMapper mapper)
        {  
            _usersContext = usersContext; 
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult>CreateUser(UserDTO user)
        {
            if (user == null || user.Id != null)
            {
                return BadRequest();
            }

            User userDb = _mapper.Map<User>(user);
            userDb.Prepare();
            await _usersContext.Users.AddAsync(userDb);
            await _usersContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<IEnumerable<UserDTO>>>Getusers()
        {
            var users = await _mapper.ProjectTo<UserDTO>(_usersContext.Users).ToListAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            UserDTO user = await _mapper.ProjectTo<UserDTO>(_usersContext.Users.AsQueryable().Where(u => u.Id == id)).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("GetByNameLastName")]
        public async Task<IActionResult> GetUserByString(string NameLastName)
        {
            UserDTO user = await _mapper.ProjectTo<UserDTO>(_usersContext.Users.AsQueryable().Where(u => u.FirstName == NameLastName || u.LastName == NameLastName)).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Deleteuser(int id)
        {
            User user = await _usersContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _usersContext.Users.Remove(user);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Updateuser(UserDTO user)
        {
            if (user == null || user.Id == null)
            {
                return BadRequest();
            }

            var existUser = await _usersContext.Users.FindAsync(user.Id);
            _mapper.Map(user, existUser);
            existUser.Prepare();          
            await _usersContext.SaveChangesAsync();

            return Ok();
        }
    }
}
