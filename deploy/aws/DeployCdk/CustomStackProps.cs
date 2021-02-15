using Amazon.CDK;
using Amazon.CDK.AWS.EC2;

namespace DeployCdk
{
    public class CustomStackProps : StackProps
    {
        public Vpc Vpc { get; set; }
    }
}