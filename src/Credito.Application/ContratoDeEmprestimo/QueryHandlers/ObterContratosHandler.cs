using System.Threading;
using System.Threading.Tasks;
using Credito.Application.Models;
using MediatR;
using Credito.Application.ContratoDeEmprestimo.Queries;
using System.Collections.Generic;
using Credito.Framework.MongoDB;
using Credito.Domain.ContratoDeEmprestimo;
using MongoDB.Driver;
using System.Linq;
using AutoMapper;

namespace Credito.Application.ContratoDeEmprestimo.QueryHandlers
{
    public class ObterContratosHandler : IRequestHandler<ObterContratosQuery, IEnumerable<ContratoDeEmprestimoModel>>
    {
        private readonly IMongoDbContext _context;
        private readonly IMapper _mapper;

        public ObterContratosHandler(IMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContratoDeEmprestimoModel>> Handle(ObterContratosQuery request, CancellationToken cancellationToken)
        {
            var resource = await _context.GetCollection<ContratoDeEmprestimoAggregate>()
                                         .Aggregate()
                                         .Skip(request.Skip)
                                         .Limit(request.Take)
                                         .ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ContratoDeEmprestimoModel>>(resource);
        }
    }
}