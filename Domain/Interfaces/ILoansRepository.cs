using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLendApi.Application.DTOs;
using BookLendApi.Domain.Entities;

namespace BookLendApi.Domain.Interfaces
{
    public interface ILoansRepository
    {
        IEnumerable<Loans> GetAll();
       Task<int> AddLoanWithDetails(Loans loan, List<LoanDetailsDataTransferObject> details);
        
        Task<Loans> GetByIdAsync(int id);
        Task UpdateAsync(Loans loan);
        Task RemoveBookFromLoan(int loanId, int bookId, int quantity);
    }
}