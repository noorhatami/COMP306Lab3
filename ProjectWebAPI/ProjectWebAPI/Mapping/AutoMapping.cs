using AutoMapper;
using ProjectWebAPI.Models;
using ProjectWebAPI.DTOs;

namespace ProjectWebAPI.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Books, BookCreateDTO>();
            CreateMap<Books, BookUpdateDTO>();
            CreateMap<Books, BookInfoDTO>();
            CreateMap<BookCreateDTO, Books>();
            CreateMap<BookUpdateDTO, Books>();
            CreateMap<BookInfoDTO, Books>();

            CreateMap<Reviews, ReviewsCreateDTO>();
            CreateMap<Reviews, ReviewsUpdateDTO>();
            CreateMap<Reviews, ReviewsInfoDTO>();
            CreateMap<ReviewsCreateDTO, Reviews>();
            CreateMap<ReviewsUpdateDTO, Reviews>();
            CreateMap<ReviewsInfoDTO, Reviews>();
        }
    }

}