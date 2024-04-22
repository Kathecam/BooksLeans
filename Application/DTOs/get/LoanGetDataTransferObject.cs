using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLendApi.Application.DTOs.get;
using BookLendApi.Domain.Enums;
using BookLendAPI.Application.DTOs.get;

namespace BookLendApi.Application.DTOs.get
{
    public class LoanGetDataTransferObject
    {
        public int LoanId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public LoanStatus Status { get; set; }
        public List<LoanDetailsGetDataTransferObject> LoanDetails { get; set; }
        public UserGetDataTransferObject User { get; set; } // DTO para representar la información del usuario
    }
}