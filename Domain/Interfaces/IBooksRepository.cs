using BookLendApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBooksRepository
{
    IEnumerable<Books> GetAll();
    Books GetById(int id);
    void Add(Books book);
    IEnumerable<Books> Search(string criteria);
    IEnumerable<Books> Filter(string genre, string author, int publicationyear);
    Task UpdateAsync(Books book);
    Task<List<Books>> GetBooksByLoanIdAsync(int loanId); 
    Task<Books> GetByIdAsync(int id);

}
