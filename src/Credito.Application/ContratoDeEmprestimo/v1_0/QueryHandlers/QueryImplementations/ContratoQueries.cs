using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Credito.Application.v1_0.ContratoDeEmprestimo.Queries;
using Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces;
using Credito.Application.v1_0.Models;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.Framework.MongoDB;
using MongoDB.Driver;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers.QueryImplementations
{
    public class ContratoQueries : IObterContratos, IObterContratoPorId
    {
        private readonly IMongoDbContext _context;
        private readonly IMapper _mapper;

        public ContratoQueries(IMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContratoDeEmprestimoModel>> ObterContratosAsync(ObterContratosQuery request,
                                                                                      CancellationToken cancellationToken = default) =>
            _mapper.Map<IEnumerable<ContratoDeEmprestimoModel>>(
                await _context.GetCollection<ContratoDeEmprestimoAggregate>()
                              .Aggregate()
                              .Skip(request.Skip)
                              .Limit(request.Take)
                              .ToListAsync(cancellationToken));

        public ContratoDeEmprestimoModel ObterContratoPorId(ObterContratoPorIdQuery request) =>
            _mapper.Map<ContratoDeEmprestimoModel>(
                _context.GetCollection<ContratoDeEmprestimoAggregate>()
                        .AsQueryable()
                        .Where(x => x.Id == request.Id)
                        .FirstOrDefault());
    }
}