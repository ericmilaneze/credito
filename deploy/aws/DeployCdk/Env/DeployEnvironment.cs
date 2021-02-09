using System;
using Amazon.CDK;

namespace DeployCdk.Env
{
    public class DeployEnvironment
    {
        private readonly Construct _scope;

        public string EnvName =>
            (_scope.Node.TryGetContext("CDK_DEPLOY_ENV") as string
                ?? System.Environment.GetEnvironmentVariable("CDK_DEPLOY_ENV")).Trim().ToLowerInvariant();
        
        public string VpcId =>
            _scope.Node.TryGetContext("CDK_DEPLOY_VPC_ID") as string
                ?? System.Environment.GetEnvironmentVariable("CDK_DEPLOY_VPC_ID");
        
        public AwsEnvironment Aws =>
            new AwsEnvironment(_scope);

        public DeployEnvironment(Construct scope) =>
            _scope = scope;

        public string PutEnvNamePrefixWithDash(string id) =>
            $"{EnvName}-{id}";
    }
}