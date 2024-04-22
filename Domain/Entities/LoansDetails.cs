using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookLendApi.Domain.Entities
{
    public class LoansDetails
    {
        [Key]
        public int LoanDetailsId { get; set; }

        public int Quantity { get; set; }
        public int BookId { get; set; }
        public int LoanId { get; set; }
        public Books Book { get; set; }
        public Loans Loan { get; set; } // Propiedad de navegación para la relación
    }
}