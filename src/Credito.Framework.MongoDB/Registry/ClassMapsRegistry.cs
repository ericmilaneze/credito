using Credito.Domain.ContratoDeEmprestimo;
using Credito.Framework.MongoDB.Example;
using MongoDB.Bson.Serialization;

namespace Credito.Framework.MongoDB.Registry
{
    public class ClassMapsRegistry
    {
        internal static void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<ExampleAggregate>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<ContratoDeEmprestimoAggregate>(cm =>
            {
                cm.AutoMap();
                cm.MapField("_parcelas").SetElementName("parcelas");
                cm.MapProperty(c => c.TaxaAoDia).SetElementName("taxaAoDia");
                cm.MapProperty(c => c.ValorCarencia).SetElementName("valorCarencia");
                cm.MapProperty(c => c.ValorFinanciado).SetElementName("valorFinanciado");
                cm.MapProperty(c => c.ValorDaParcela).SetElementName("valorDaParcela");
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Parcela>(cm =>
            {
                cm.AutoMap();
                cm.MapField(p => p.SaldoDevedorFinal).SetElementName("saldoDevedorFinal");
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}