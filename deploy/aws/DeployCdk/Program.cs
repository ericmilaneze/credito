using Amazon.CDK;
using Environment = Amazon.CDK.Environment;

namespace DeployCdk
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new CreditoAppStack(app, "CreditoAppStack", new StackProps { Env = GetEnvironment() });
            app.Synth();
        }

        private static Environment GetEnvironment() =>
            new Environment
            {
                Account = Globals.DeployEnvironment.Aws.Account,
                Region = Globals.DeployEnvironment.Aws.Region
            };
    }
}
