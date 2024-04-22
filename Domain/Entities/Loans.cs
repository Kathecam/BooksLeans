using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BookLendApi.Domain.Enums;

namespace BookLendApi.Domain.Entities
{
    public class Loans
{
    [Key]
    public int LoanId { get; set; }
    public int UserId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public LoanStatus Status { get; set; }
     public ICollection<LoansDetails> LoansDetails { get; set; }
    public Users User { get; set; }

    
}
}