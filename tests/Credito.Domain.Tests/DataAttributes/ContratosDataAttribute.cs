using System.Collections.Generic;
using Newtonsoft.Json;

namespace Credito.Domain.Tests.DataAttributes
{
    public class ContratosDataAttribute : JsonFileDataAttribute
    {
        public ContratosDataAttribute(string filePath) : base(filePath)
        {
            
        }

        protected override IEnumerable<object[]> GetData(string fileData)
        {
            var contratos = JsonConvert.DeserializeObject<List<ContratoData>>(fileData);
            foreach (var contrato in contratos)
                yield return new object[] { contrato };
        }
    }
}