using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.Logs;

namespace DeployCdk
{
    public class CreditoInfraStack : Stack
    {
        internal CreditoInfraStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var vpc = Vpc.FromLookup(this, "VPC",
                new VpcLookupOptions
                {
                    VpcId = Globals.DeployEnvironment.VpcId
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
                Fn.ImportValue(Globals.DeployEnvironment.PutEnvNamePrefixWithDash("WebApiServiceSecurityGroupId")));

            var appListener = ApplicationListener.FromApplicationListenerAttributes(this, "AppListener",
                new ApplicationListenerAttributes
                {
                    ListenerArn = Fn.ImportValue(Globals.DeployEnvironment.PutEnvNamePrefixWithDash("AppListenerArn")),
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
                    LogGroupName = $"/ecs/{Globals.DeployEnvironment.GetEnvName()}/credito/web-api",
                    Retention = RetentionDays.FIVE_DAYS,
                    RemovalPolicy = RemovalPolicy.SNAPSHOT
                });

            new CfnOutput(this, "CreditoWebApiTargetGroupArn",
                new CfnOutputProps
                {
                    ExportName = Globals.DeployEnvironment.PutEnvNamePrefixWithDash("CreditoWebApiTargetGroupArn"),
                    Value = creditoWebApiTargetGroup.TargetGroupArn
                });

            new CfnOutput(this, "CreditoWebApiLogGroupArn",
                new CfnOutputProps
                {
                    ExportName = Globals.DeployEnvironment.PutEnvNamePrefixWithDash("CreditoWebApiLogGroupArn"),
                    Value = creditoWebApiLogGroup.LogGroupArn
                });
        }
    }
}
