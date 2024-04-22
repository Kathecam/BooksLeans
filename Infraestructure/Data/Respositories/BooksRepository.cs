using BookLendApi.Domain.Entities;
using BookLendApi.Infraestructure.Data.DataSource;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class BooksRepository : IBooksRepository
{
    private readonly SqldbContext _context;

    public BooksRepository(SqldbContext context)
    {
        _context = context;
    }

    public IEnumerable<Books> GetAll()
    {
        return _context.Book.ToList();
    }

    public Books GetById(int id)
    {
        return _context.Book.FirstOrDefault(b => b.BookId == id);
    }

    public void Add(Books book)
    {
        _context.Book.Add(book);
        _context.SaveChanges();
    }

    public IEnumerable<Books> Search(string criteria)
    {
        return _context.Book.Where(b => b.Title.Contains(criteria) || b.Author.Contains(criteria)  || b.Gender.Contains(criteria)).ToList();
    }

    public IEnumerable<Books> Filter(string genre, string author, int publicationyear)
    {
        var query = _context.Book.AsQueryable();

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(b => b.Gender == genre);
        }

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.Author == author);
        }

        if (publicationyear > 0)
        {
            query = query.Where(b => b.PublicationYear == publicationyear);
        }

        return query.ToList();
    }

    public async Task<Books> GetByIdAsync(int id)
    {
        return await _context.Book.FirstOrDefaultAsync(b => b.BookId == id);
    }

    public async Task UpdateAsync(Books book)
    {
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Books>> GetBooksByLoanIdAsync(int loanId)
    {
        return await _context.LoansDetails
            .Where(ld => ld.LoanId == loanId)
            .Select(ld => ld.Book)
            .ToListAsync();
    }
}
