using AutoMapper;
using CadastroProduto.Domain.Entities;
using CadastroProduto.Domain.Commands;

namespace CadastroProduto.Application.AutoMapper.Profiles
{
    public class EntityToCommandProfile : Profile
    {
        public EntityToCommandProfile()
        {
            CreateMap<Produto, CreateProdutoCommand>().ReverseMap();
            CreateMap<Produto, UpdateProdutoCommand>().ReverseMap();
        }
    }
}