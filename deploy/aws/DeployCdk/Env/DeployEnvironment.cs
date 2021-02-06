using System;

namespace DeployCdk.Env
{
    public class DeployEnvironment
    {
        private readonly string _envName;

        public AwsEnvironment Aws { get; }

        public DeployEnvironment(string envName, AwsEnvironment aws)
        {
            _envName = envName;
            Aws = aws;
        }

        public static DeployEnvironment Create()
        {
            var account = Environment.GetEnvironmentVariable("CDK_DEPLOY_AWS_ACCOUNT");
            var region = Environment.GetEnvironmentVariable("CDK_DEPLOY_AWS_REGION");
            var envName = Environment.GetEnvironmentVariable("CDK_DEPLOY_ENV");

            var aws = new AwsEnvironment(account, region);
            return new DeployEnvironment(envName, aws);
        }

        public string GetEnvName() =>
            _envName.Trim().ToLowerInvariant();
    }
}