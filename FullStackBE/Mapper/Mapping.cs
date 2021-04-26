using AutoMapper;
using FullStackBE.Models;
using FullStackBE.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackBE.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Item, ItemCreateDto>().ReverseMap();
            CreateMap<Item, ItemUpdateDto>().ReverseMap();
        }
    }
}
