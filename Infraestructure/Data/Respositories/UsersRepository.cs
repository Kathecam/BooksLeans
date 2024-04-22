using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLendApi.Domain.Entities;
using BookLendApi.Domain.Interfaces;
using BookLendApi.Infraestructure.Data.DataSource;

namespace BookLendApi.Infraestructure.data.Respositories
{
    public class UsersRepository: IUsersRespository
    {
        private readonly SqldbContext _context;

        public UsersRepository(SqldbContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Users.ToList();
        }

        public Users GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }

        public Users GetByNameUser(string nameUser)
        {
            return _context.Users.FirstOrDefault(u => u.NameUser == nameUser);
        }

        public void Add(Users user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}