using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookLendApi.Application.DTOs
{
    public class LoanDetailsDataTransferObject
{ 
    public int LoanDetailsId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; } 
}
}