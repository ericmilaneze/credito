using System.IO;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.Logs;

namespace DeployCdk
{
    public class CreditoAppStack : Stack
    {
        internal CreditoAppStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var vpc = Vpc.FromLookup(this, "VPC",
                new VpcLookupOptions
                {
                    VpcId = Globals.DeployEnvironment.VpcId
                });

            var creditoWebApiTaskDefinition = new FargateTaskDefinition(this, "CreditoWebApiTaskDefinition",
                new FargateTaskDefinitionProps
                {
                    MemoryLimitMiB = 512,
                    Cpu = 256
                });

            var creditoWebApiLogGroup = LogGroup.FromLogGroupArn(this,
                "CreditoWebApiContainerLogGroup",
                Fn.ImportValue(Globals.DeployEnvironment.PutEnvNamePrefixWithDash("CreditoWebApiLogGroupArn")));

            var creditoWebApiLogging = new AwsLogDriver(
                new AwsLogDriverProps
                {
                    StreamPrefix = "ecs",
                    LogGroup = creditoWebApiLogGroup
                });

            var creditoWebApiContainer = creditoWebApiTaskDefinition.AddContainer("CreditoWebApiContainer",
                new ContainerDefinitionOptions
                {
                    Image = ContainerImage.FromAsset(
                        Directory.GetCurrentDirectory(),
                        new AssetImageProps
                        {
                            File = "src/Credito.WebApi/Dockerfile"
                        }),
                    Logging = creditoWebApiLogging
                });

            creditoWebApiContainer.AddPortMappings(
                new PortMapping
                {
                    ContainerPort = 80,
                    HostPort = 80,
                    Protocol = Amazon.CDK.AWS.ECS.Protocol.TCP
                });

            var cluster = Cluster.FromClusterAttributes(this, "Cluster",
                new ClusterAttributes
                {
                    ClusterName = Fn.ImportValue(Globals.DeployEnvironment.PutEnvNamePrefixWithDash("ClusterName")),
                    Vpc = vpc,
                    SecurityGroups = new SecurityGroup[] { }
                });

            var webApiServiceSecurityGroup = SecurityGroup.FromSecurityGroupId(this,
                "WebApiServiceSecurityGroup",
                Fn.ImportValue(Globals.DeployEnvironment.PutEnvNamePrefixWithDash("WebApiServiceSecurityGroupId")));

            var creditoWebApiService = new FargateService(this, "CreditoWebApiService",
                new FargateServiceProps
                {
                    Cluster = cluster,
                    TaskDefinition = creditoWebApiTaskDefinition,
                    DesiredCount = 1,
                    CircuitBreaker = new DeploymentCircuitBreaker { Rollback = true },
                    AssignPublicIp = true,
                    HealthCheckGracePeriod = Duration.Seconds(60),
                    SecurityGroups = new ISecurityGroup[] { webApiServiceSecurityGroup }
                });

            var creditoWebApiTargetGroup = ApplicationTargetGroup.FromTargetGroupAttributes(this, "CreditoWebApiTargetGroup",
                new TargetGroupAttributes
                {
                    TargetGroupArn = Fn.ImportValue(Globals.DeployEnvironment.PutEnvNamePrefixWithDash("CreditoWebApiTargetGroupArn"))
                });

            creditoWebApiService.AttachToApplicationTargetGroup(creditoWebApiTargetGroup);
        }
    }
}
