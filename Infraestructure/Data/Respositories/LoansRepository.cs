using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLendApi.Application.DTOs;
using BookLendApi.Domain.Entities;
using BookLendApi.Domain.Interfaces;
using BookLendApi.Infraestructure.Data.DataSource;
using BookLendApi.Infraestructure.services;
using Microsoft.EntityFrameworkCore;

namespace BookLendApi.Infraestructure.data.Respositories
{
    public class LoansRepository : ILoansRepository
    {
        private readonly SqldbContext _context;


        public LoansRepository(SqldbContext context)
        {
            _context = context;

        }

        public async Task<int> AddLoanWithDetails(Loans loan, List<LoanDetailsDataTransferObject> details)
{
    using (var transaction = _context.Database.BeginTransaction())
    {
        try
        {
            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();

            // Agregar los detalles del préstamo (libros prestados) a la base de datos
            foreach (var detailDto in details)
            {
                // Verificar disponibilidad de stock para cada libro
                var book = await _context.Book.FindAsync(detailDto.BookId);
                if (book == null || book.Stock < detailDto.Quantity)
                {
                    throw new Exception($"No hay suficiente stock disponible para el libro con ID {detailDto.BookId}.");
                }

                var loanDetail = new LoansDetails { LoanId = loan.LoanId, BookId = detailDto.BookId, Quantity = detailDto.Quantity };
                _context.LoansDetails.Add(loanDetail);

                // Disminuir el stock del libro
                book.Stock -= detailDto.Quantity;
                _context.Entry(book).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            // Commit de la transacción y devolución del ID del préstamo creado
            transaction.Commit();
            return loan.LoanId;
        }
        catch (Exception ex)
        {
            // Rollback en caso de error y lanzar excepción
            transaction.Rollback();
            throw new Exception("Error al guardar el préstamo con sus detalles: " + ex.Message);
        }
    }
}



        public async Task RemoveBookFromLoan(int loanId, int bookId, int quantity)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Buscar el préstamo
                    var loan = await _context.Loan.FindAsync(loanId);
                    if (loan == null)
                    {
                        throw new Exception($"No se encontró un préstamo con el ID {loanId}");
                    }

                    // Eliminar la relación entre el libro y el préstamo
                    var loanDetail = loan.LoansDetails.FirstOrDefault(ld => ld.BookId == bookId);
                    if (loanDetail != null)
                    {
                        _context.LoansDetails.Remove(loanDetail);

                        // Aumentar el stock del libro
                        var book = await _context.Book.FindAsync(bookId);
                        book.Stock += quantity;
                        _context.Entry(book).State = EntityState.Modified;

                        await _context.SaveChangesAsync();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error al eliminar el libro del préstamo: " + ex.Message);
                }
            }
        }


        public IEnumerable<Loans> GetAll()
{
    return _context.Loan
        .Include(loan => loan.LoansDetails) // Incluir los detalles del préstamo
            .ThenInclude(detail => detail.Book) // Incluir los libros asociados a los detalles
        .ToList();
}
        public async Task<Loans> GetByIdAsync(int id)
        {
            return await _context.Loan
                .Include(loan => loan.User)
                .Include(loan => loan.LoansDetails) // Incluir detalles del préstamo
                    .ThenInclude(ld => ld.Book) // Incluir los libros relacionados con los detalles del préstamo
                .FirstOrDefaultAsync(loan => loan.LoanId == id);
        }


        public async Task UpdateAsync(Loans loan)
        {
            _context.Entry(loan).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}