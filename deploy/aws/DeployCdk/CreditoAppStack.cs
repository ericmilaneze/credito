using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;

namespace DeployCdk
{
    public class CreditoAppStack : Stack
    {
        internal CreditoAppStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var vpc = Vpc.FromLookup(this, "VPC",
                new VpcLookupOptions
                {
                    VpcId = Globals.DeployEnvironment.VpcId
                });

            var cluster = new Cluster(this, "Cluster",
                new ClusterProps
                {
                    Vpc = vpc
                });
        }
    }
}
