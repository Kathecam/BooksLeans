using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLendApi.Domain.Entities;

namespace BookLendApi.Domain.Interfaces
{
    public interface IUsersRespository
    {
        Users GetById(int id);
        Users GetByNameUser(string nameUser);
        void Add(Users user);
    }
}