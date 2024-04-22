using AutoMapper;
using BookLendApi.Application.DTOs;
using BookLendApi.Application.DTOs.get;
using BookLendApi.Domain.Entities;
using BookLendApi.Domain.Enums;
using BookLendApi.Domain.Interfaces;
using BookLendApi.Infraestructure.Data.DataSource;
using BookLendAPI.Application.DTOs.get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookLendApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoansRepository _loansRepository;
        private readonly IMapper _mapper;

        private readonly SqldbContext _context;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IBooksRepository _booksRepository;
        public LoansController(ILoansRepository loansRepository, IMapper mapper, SqldbContext sqldbContext, IBooksRepository booksRepository)
        {
            _loansRepository = loansRepository;
            _mapper = mapper;
            _context = sqldbContext;
            _booksRepository = booksRepository;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // Otras opciones que puedas necesitar
            };
        }

    [HttpGet]
[Authorize]
public async Task<ActionResult<IEnumerable<LoanGetDataTransferObject>>> GetAll()
{
    var loans = await _context.Loan
        .Include(loan => loan.LoansDetails)
            .ThenInclude(detail => detail.Book) // Incluir los libros asociados a los detalles del préstamo
        .Include(loan => loan.User)
        .ToListAsync();

    if (loans == null)
    {
        return NotFound();
    }
    else
    {
        var loanDTOs = loans.Select(loan => new LoanGetDataTransferObject
        {
            LoanId = loan.LoanId,
            UserId = loan.UserId,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate,
            Status = loan.Status,
            LoanDetails = loan.LoansDetails.Select(loanDetail => new LoanDetailsGetDataTransferObject
            {
                BookId = loanDetail.BookId,
                Tittle = loanDetail.Book != null ? loanDetail.Book.Title : null,
                Author = loanDetail.Book != null ? loanDetail.Book.Author : null,
                Gender = loanDetail.Book != null ? loanDetail.Book.Gender : null,
                PublicationYear = loanDetail.Book != null ? loanDetail.Book.PublicationYear : 0,
                Quantity = loanDetail.Quantity,
                Lend = loanDetail.Book != null ? loanDetail.Book.Lend : false,
            }).ToList(),
            User = new UserGetDataTransferObject
            {
                UserId = loan.User.UserId,
                Name = loan.User.Name,
                Phone = loan.User.Phone,
                NameUser = loan.User.NameUser
            }
        }).ToList();

        return loanDTOs;
    }
}




        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _loansRepository.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            var loanDTO = new LoanGetDataTransferObject
            {
                LoanId = loan.LoanId,
                UserId = loan.UserId,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate,
                Status = loan.Status,
                LoanDetails = loan.LoansDetails.Select(loanDetail => new LoanDetailsGetDataTransferObject
                {
                    BookId = loanDetail.BookId,
                    Tittle = loanDetail.Book.Title,
                    Author = loanDetail.Book.Author,
                    Gender = loanDetail.Book.Gender,
                    PublicationYear = loanDetail.Book.PublicationYear,
                    Quantity = loanDetail.Quantity,
                    Lend = loanDetail.Book.Lend,
                }).ToList(),
                User = new UserGetDataTransferObject
                {
                    UserId = loan.User.UserId,
                    Name = loan.User.Name,
                    Phone = loan.User.Phone,
                    NameUser = loan.User.NameUser
                }
            };

            return Ok(loanDTO);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] LoanDataTransferObject loanDTO)
        {
            try
            {
                var loanEntity = _mapper.Map<Loans>(loanDTO);

                if (loanDTO.LoansDetails == null || loanDTO.LoansDetails.Count == 0)
                {
                    return BadRequest("La lista de detalles de préstamos está vacía.");
                }

                // Llamar al método del repositorio para agregar el préstamo con detalles
                var loanId = await _loansRepository.AddLoanWithDetails(loanEntity, loanDTO.LoansDetails);

                return Ok(new { LoanId = loanId });
            }
            catch (Exception ex)
            {
                return BadRequest("Error al crear el préstamo: " + ex.Message);
            }
        }



        [HttpPut("{id}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] LoanStatus newStatus)
        {
            try
            {
                // Obtener el préstamo por su ID
                var loan = await _loansRepository.GetByIdAsync(id);
                if (loan == null)
                {
                    return NotFound($"No se encontró un préstamo con el ID {id}");
                }

                // Actualizar el estado del préstamo
                loan.Status = newStatus;

                // Guardar los cambios en el repositorio
                await _loansRepository.UpdateAsync(loan);

                // Convertir la colección de detalles del préstamo a una lista para poder acceder mediante índices
                var loanDetailsList = loan.LoansDetails.ToList();
                foreach (var loanDetail in loanDetailsList)
                {
                    var existingBook = await _booksRepository.GetByIdAsync(loanDetail.BookId);
                    if (existingBook != null)
                    {
                        existingBook.Lend = newStatus == LoanStatus.Confirmado ? true : false;
                        await _booksRepository.UpdateAsync(existingBook);

                        if (newStatus == LoanStatus.Devuelto)
                        {
                            // Incrementar el stock del libro
                            existingBook.Stock += loanDetail.Quantity;
                            await _booksRepository.UpdateAsync(existingBook);

                            // Eliminar el detalle del préstamo
                            loan.LoansDetails.Remove(loanDetail);
                            _context.LoansDetails.Remove(loanDetail);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return Ok($"El estado del préstamo {id} se ha actualizado a {newStatus}");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar el estado del préstamo: " + ex.Message);
            }
        }


    }
}
