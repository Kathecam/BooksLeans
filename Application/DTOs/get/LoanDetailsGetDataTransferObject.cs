using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLendAPI.Application.DTOs.get
{
    public class LoanDetailsGetDataTransferObject
    {
        public int BookId { get; set; }
        public string Tittle { get; set; }
        public string Author { get; set; }
        public string Gender { get; set; }
        public int PublicationYear { get; set; }
        public bool Lend { get; set; }
        public int Quantity { get; set; } 
    }
}