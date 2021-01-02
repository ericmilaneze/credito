using System.Threading;
using System.Threading.Tasks;
using Credito.Application.Models;
using MediatR;
using Credito.Application.ContratoDeEmprestimo.Queries;
using Credito.Framework.MongoDB;
using Credito.Domain.ContratoDeEmprestimo;
using MongoDB.Driver;
using AutoMapper;
using System.Linq;

namespace Credito.Application.ContratoDeEmprestimo.QueryHandlers
{
    public class ObterContratoPorIdHandler : IRequestHandler<ObterContratoPorIdQuery, ContratoDeEmprestimoModel>
    {
        private readonly IMongoDbContext _context;
        private readonly IMapper _mapper;

        public ObterContratoPorIdHandler(IMongoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ContratoDeEmprestimoModel> Handle(ObterContratoPorIdQuery request,
                                                            CancellationToken cancellationToken)
        {
            var resource = _context.GetCollection<ContratoDeEmprestimoAggregate>()
                                   .AsQueryable()
                                   .Where(x => x.Id == request.Id)
                                   .FirstOrDefault();
            return await Task.FromResult(_mapper.Map<ContratoDeEmprestimoModel>(resource));
        }
    }
}