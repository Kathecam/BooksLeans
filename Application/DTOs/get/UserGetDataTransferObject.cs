using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLendApi.Application.DTOs.get
{
    public class UserGetDataTransferObject
    {
         public int UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string NameUser { get; set; }
    }
}