using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLendApi.Application.DTOs.get
{
    public class BookGetDataTransferObject
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Gender { get; set; }
        public int PublicationYear { get; set; }
        public bool Lend { get; set; }
    }
}