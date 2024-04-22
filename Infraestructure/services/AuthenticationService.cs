using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLendApi.Infraestructure.Data.DataSource;

namespace BookLendApi.Infraestructure.services
{
    public class AuthenticationService
    {
        private readonly SqldbContext _context;

        public AuthenticationService(SqldbContext context)
        {
            _context = context;
        }

        public bool ValidateCredentials(string user, string password)
        {
            // Buscar el usuario en la base de datos por nombre de usuario
            var UserRegister = _context.Users.FirstOrDefault(u => u.NameUser == user);

            // Verificar si se encontró un usuario y si la contraseña es correcta
            if (UserRegister != null && UserRegister.Password == password)
            {
                return true; // Credenciales válidas
            }
            return false; // Credenciales inválidas
        }
    }
}