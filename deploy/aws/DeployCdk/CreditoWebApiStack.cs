using System.IO;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.Logs;

namespace DeployCdk
{
    public class CreditoWebApiStack : Stack
    {
        internal CreditoWebApiStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var vpc = Vpc.FromLookup(this, "VPC",
                new VpcLookupOptions
                {
                    VpcId = Globals.GetDeployEnvironment(this).VpcId
                });

            var creditoWebApiTargetGroup = new ApplicationTargetGroup(this, "CreditoWebApiTargetGroup",
                new ApplicationTargetGroupProps
                {
                    Protocol = ApplicationProtocol.HTTP,
                    Port = 80,
                    Vpc = vpc,
                    TargetType = TargetType.IP,
                    DeregistrationDelay = Duration.Seconds(60),
                    HealthCheck =
                        new Amazon.CDK.AWS.ElasticLoadBalancingV2.HealthCheck
                        {
                            Enabled = true,
                            Path = "/api/credito*",
                            Protocol = Amazon.CDK.AWS.ElasticLoadBalancingV2.Protocol.HTTP,
                            Port = "traffic-port",
                            UnhealthyThresholdCount = 2,
                            Interval = Duration.Seconds(60),
                            HealthyThresholdCount = 5,
                            Timeout = Duration.Seconds(5),
                            HealthyHttpCodes = "200"
                        }
                });

            var webApiServiceSecurityGroup = SecurityGroup.FromSecurityGroupId(this,
                "WebApiServiceSecurityGroup",
                Fn.ImportValue(Globals.GetDeployEnvironment(this).PutEnvNamePrefixWithDash("WebApiServiceSecurityGroupId")));

            var appListener = ApplicationListener.FromApplicationListenerAttributes(this, "AppListener",
                new ApplicationListenerAttributes
                {
                    ListenerArn = Fn.ImportValue(Globals.GetDeployEnvironment(this).PutEnvNamePrefixWithDash("AppListenerArn")),
                    SecurityGroup = webApiServiceSecurityGroup
                });

            appListener.AddTargetGroups("CreditoWebApiTargetGroup",
                new AddApplicationTargetGroupsProps
                {
                    Conditions =
                        new ListenerCondition[]
                        {
                            ListenerCondition.PathPatterns(new string[] { "/api/credito/_monitor/shallow" })
                        },
                    Priority = 100,
                    TargetGroups = new ApplicationTargetGroup[] { creditoWebApiTargetGroup }
                });

            var creditoWebApiLogGroup = new LogGroup(this, "CreditoWebApiContainerLogGroup",
                new LogGroupProps
                {
                    LogGroupName = $"/ecs/{Globals.GetDeployEnvironment(this).EnvName}/credito/web-api",
                    Retention = RetentionDays.FIVE_DAYS,
                    RemovalPolicy = RemovalPolicy.SNAPSHOT
                });

            var creditoWebApiTaskDefinition = new FargateTaskDefinition(this, "CreditoWebApiTaskDefinition",
                new FargateTaskDefinitionProps
                {
                    MemoryLimitMiB = 512,
                    Cpu = 256
                });

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
                    ClusterName = Fn.ImportValue(Globals.GetDeployEnvironment(this).PutEnvNamePrefixWithDash("ClusterName")),
                    Vpc = vpc,
                    SecurityGroups = new SecurityGroup[] { }
                });

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

            creditoWebApiService.AttachToApplicationTargetGroup(creditoWebApiTargetGroup);
        }
    }
}
