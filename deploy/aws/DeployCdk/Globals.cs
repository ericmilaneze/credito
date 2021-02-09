using Amazon.CDK;
using DeployCdk.Env;

namespace DeployCdk
{
    public class Globals
    {
        public static DeployEnvironment GetDeployEnvironment(Construct scope) =>
            new DeployEnvironment(scope);
    }
}