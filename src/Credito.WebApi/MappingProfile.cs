using AutoMapper;
using Credito.Domain.Common.ValueObjects;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.WebApi.Models;

namespace Credito.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ValueFromDecimal, decimal>()
                .ConvertUsing(value => ValueFromDecimal.ToDecimal(value));

            CreateMap<ValueFromInt, int>()
                .ConvertUsing(value => ValueFromInt.ToInt(value));

            CreateMap<ContratoDeEmprestimoAggregate, ContratoDeEmprestimoModel>();
            CreateMap<Parcela, ParcelaModel>();
        }
    }
}