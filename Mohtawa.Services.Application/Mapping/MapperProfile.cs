using AutoMapper;
using Mohtawa.Services.Application.Models.DTOs;
using Mohtawa.Services.Application.Models.Requests.Book;
using Mohtawa.Services.Domain.Models.Entities;

namespace Mohtawa.Services.Application.Mapping
{
    public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<AddBookRequest, Book>();
        }

    }
}
