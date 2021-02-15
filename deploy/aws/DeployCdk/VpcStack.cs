using Amazon.CDK;
using Amazon.CDK.AWS.EC2;

namespace DeployCdk
{
    public class VpcStack : Stack
    {
        public Vpc Vpc { get; set; }

        internal VpcStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var vpc = new Vpc(this, "Vpc",
                new VpcProps
                {
                    MaxAzs = 2
                });

            Vpc = vpc;

            new CfnOutput(this, "VpcId",
                new CfnOutputProps
                {
                    ExportName = Globals.GetDeployEnvironment(this).PutEnvNamePrefixWithDash("VpcId"),
                    Value = vpc.VpcId
                });
        }
    }
}
