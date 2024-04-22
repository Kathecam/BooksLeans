using AutoMapper;
using BookLendApi.Application.DTOs;
using BookLendApi.Application.DTOs.get;
using BookLendApi.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoanDataTransferObject, Loans>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.LoansDetails, opt => opt.MapFrom(src => src.LoansDetails));

        CreateMap<LoanDetailsDataTransferObject, LoansDetails>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<int, Books>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src));

        CreateMap<Users, UserDataTransferObjects>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        CreateMap<Books, BooksDataTranferObjects>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId));

        CreateMap<Loans, LoanGetDataTransferObject>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.LoanDetails, opt => opt.MapFrom(src => src.LoansDetails.Select(ld => new BookGetDataTransferObject
            {
                BookId = ld.BookId,
                Title = ld.Book.Title,
                Author = ld.Book.Author,
                Gender = ld.Book.Gender,
                PublicationYear = ld.Book.PublicationYear,
                Lend = ld.Book.Lend
            })))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<Books, BookGetDataTransferObject>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear))
            .ForMember(dest => dest.Lend, opt => opt.MapFrom(src => src.Lend));

        CreateMap<Users, UserGetDataTransferObject>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.NameUser, opt => opt.MapFrom(src => src.NameUser));
    }
}
