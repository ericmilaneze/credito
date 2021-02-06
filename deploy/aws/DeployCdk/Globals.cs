using DeployCdk.Env;

namespace DeployCdk
{
    public class Globals
    {
        public static DeployEnvironment DeployEnvironment =>
            DeployEnvironment.Create();
    }
}