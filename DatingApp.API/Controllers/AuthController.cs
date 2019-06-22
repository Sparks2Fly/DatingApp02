using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register( UsersForRegisterDto usersForRegisterDto )
        {
            usersForRegisterDto.Username = usersForRegisterDto.Username.ToLower();

            if ( await _repo.UserExists(usersForRegisterDto.Username))
                return BadRequest("Username already exisits");
            
            var userToCreate = new User
            {
                UserName = usersForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, usersForRegisterDto.Password);

            return StatusCode(201);

        }

    }
}