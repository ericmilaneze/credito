using Amazon.CDK;

namespace DeployCdk.Env
{
    public class AwsEnvironment
    {
        private readonly Construct _scope;

        public string Account =>
            _scope.Node.TryGetContext("CDK_DEPLOY_AWS_ACCOUNT") as string
                ?? System.Environment.GetEnvironmentVariable("CDK_DEPLOY_AWS_ACCOUNT");

        public string Region =>
            _scope.Node.TryGetContext("CDK_DEPLOY_AWS_REGION") as string
                ?? System.Environment.GetEnvironmentVariable("CDK_DEPLOY_AWS_REGION");

        public AwsEnvironment(Construct scope) =>
            _scope = scope;
    }
}