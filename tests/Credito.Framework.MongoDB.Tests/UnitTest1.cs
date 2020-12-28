using System;
using Xunit;

namespace Credito.Framework.MongoDB.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var id = Guid.NewGuid();
            //var id = "02089b09-5ad2-4dd8-a569-cc086d76d4dc";

            var mongo = new MongoDbRepository("mongodb://localhost", "test");
            mongo.Insert(new Aggregate(id, "Eric", 34, 100000M));

            var agg = mongo.Load<Aggregate>(id);

            mongo.Update(new Aggregate(id, "Eric", 35, 100000M));

            var aggUpdated = mongo.Load<Aggregate>(id);

            mongo.Remove<Aggregate>(id);

            mongo.Insert(new Aggregate(id, "Eric", 28, 100000M));

            var aggs0 = mongo.Find<Aggregate>(x => x.Id == id);
            var aggs2 = mongo.Find<Aggregate>(x => x.Idade == 28, 0, 1);
            var aggs3 = mongo.Find<Aggregate>(x => x.Idade == 28, 1, 1);
            
            var aggs4 = mongo.Get<Aggregate>(0, 1);
            var aggs5 = mongo.Get<Aggregate>(0, 2);
            var aggs6 = mongo.Get<Aggregate>(1, 1);
        }
    }
}
