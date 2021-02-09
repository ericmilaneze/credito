﻿using Amazon.CDK;
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
                               Globals.GetDeployEnvironment(app).PutEnvNamePrefixWithDash(nameof(InfraStack)),
                               new StackProps { Env = GetEnvironment() });

            var creditoWebApiStack = 
                new CreditoWebApiStack(app,
                                    Globals.GetDeployEnvironment(app).PutEnvNamePrefixWithDash(nameof(CreditoWebApiStack)),
                                    new StackProps { Env = GetEnvironment() });

            creditoWebApiStack.AddDependency(infraStack);
            
            app.Synth();

            Environment GetEnvironment() =>
                new Environment
                {
                    Account = Globals.GetDeployEnvironment(app).Aws.Account,
                    Region = Globals.GetDeployEnvironment(app).Aws.Region
                };
        }
    }
}
