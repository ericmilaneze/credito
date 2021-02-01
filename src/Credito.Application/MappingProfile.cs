using AutoMapper;
using Credito.Domain.Common.ValueObjects;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.Application.v1_0.Models;

namespace Credito.Application
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