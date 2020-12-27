using System;
using Xunit;

namespace Credito.Framework.MongoDB.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var id = Guid.NewGuid().ToString();
            //var id = "02089b09-5ad2-4dd8-a569-cc086d76d4dc";

            var mongo = new MongoTrial("mongodb://localhost");
            mongo.Insert(
                new Aggregate(id, "Eric", 34, 100000M));

            var agg = mongo.Load(id);
        }
    }
}
