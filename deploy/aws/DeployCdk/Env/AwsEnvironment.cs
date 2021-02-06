namespace DeployCdk.Env
{
    public class AwsEnvironment
    {
        public string Account { get; }
        public string Region { get; }

        public AwsEnvironment(string account, string region)
        {
            Account = account;
            Region = region;
        }
    }
}