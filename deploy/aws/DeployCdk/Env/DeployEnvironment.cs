using System;

namespace DeployCdk.Env
{
    public class DeployEnvironment
    {
        private readonly string _envName;

        public AwsEnvironment Aws { get; }
        public string VpcId { get; }

        public DeployEnvironment(string envName, AwsEnvironment aws, string vpcId)
        {
            _envName = envName;
            Aws = aws;
            VpcId = vpcId;
        }

        public static DeployEnvironment Create()
        {
            var account = Environment.GetEnvironmentVariable("CDK_DEPLOY_AWS_ACCOUNT");
            var region = Environment.GetEnvironmentVariable("CDK_DEPLOY_AWS_REGION");
            var envName = Environment.GetEnvironmentVariable("CDK_DEPLOY_ENV");
            var vpcId = Environment.GetEnvironmentVariable("CDK_DEPLOY_VPC_ID");

            var aws = new AwsEnvironment(account, region);
            return new DeployEnvironment(envName, aws, vpcId);
        }

        public string PutEnvNamePrefixInId(string id) =>
            $"{GetEnvName()}-{id}";

        public string GetEnvName() =>
            _envName.Trim().ToLowerInvariant();
    }
}