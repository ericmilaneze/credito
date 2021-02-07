using Amazon.CDK;
using Environment = Amazon.CDK.Environment;

namespace DeployCdk
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();

            var infraStack = 
                new InfraStack(app,
                               Globals.DeployEnvironment.PutEnvNamePrefixWithDash(nameof(InfraStack)),
                               new StackProps { Env = GetEnvironment() });

            var creditoInfraStack = 
                new CreditoInfraStack(app,
                                      Globals.DeployEnvironment.PutEnvNamePrefixWithDash(nameof(CreditoInfraStack)),
                                      new StackProps { Env = GetEnvironment() });

            var creditoAppStack = 
                new CreditoAppStack(app,
                                    Globals.DeployEnvironment.PutEnvNamePrefixWithDash(nameof(CreditoAppStack)),
                                    new StackProps { Env = GetEnvironment() });

            creditoInfraStack.AddDependency(infraStack);
            creditoAppStack.AddDependency(creditoInfraStack);
            
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
