using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLendApi.Domain.Entities;

namespace BookLendApi.Application.DTOs
{
    public class LoanDataTransferObject
{
    public int UserId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
   public List<LoanDetailsDataTransferObject> LoansDetails { get; set; }
}
}