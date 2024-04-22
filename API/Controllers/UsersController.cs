using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookLendApi.API.Auth;
using BookLendApi.Domain.Entities;
using BookLendApi.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BookLendApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRespository _userRepository;
        private readonly string _secretKey;

        public UsersController(IUsersRespository userRepository, string secretKey)
        {
            _userRepository = userRepository;
            _secretKey = secretKey;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(Users user)
        {
            // Verificar si el usuario ya existe en la base de datos
            var existingUser = _userRepository.GetByNameUser(user.NameUser);
            if (existingUser != null)
            {
                return Conflict("El nombre de usuario ya está en uso.");
            }

            // Si el usuario no existe, guardar el nuevo usuario en la base de datos
            _userRepository.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _userRepository.GetByNameUser(username);

            if (user == null || user.Password != password)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(username);

            return Ok(new { token });
        }

        private string GenerateJwtToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(1), // Token expira en 1 día
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
