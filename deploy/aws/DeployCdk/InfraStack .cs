using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;

namespace DeployCdk
{
    public class InfraStack : Stack
    {
        internal InfraStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var vpc = Vpc.FromLookup(this, "VPC",
                new VpcLookupOptions
                {
                    VpcId = Globals.GetDeployEnvironment(this).VpcId
                });

            var cluster = new Cluster(this, "Cluster",
                new ClusterProps
                {
                    Vpc = vpc,
                    ClusterName = Globals.GetDeployEnvironment(this).PutEnvNamePrefixWithDash("Cluster")
                });

            var albSecurityGroup = new SecurityGroup(this, "AlbSecurityGroup",
                new SecurityGroupProps
                {
                    Vpc = vpc,
                    AllowAllOutbound = true
                });

            albSecurityGroup.AddIngressRule(Peer.AnyIpv4(), Port.Tcp(80));

            var alb = new ApplicationLoadBalancer(this, "ALB",
                new ApplicationLoadBalancerProps
                {
                    Vpc = vpc,
                    InternetFacing = true,
                    Http2Enabled = true,
                    IdleTimeout = Duration.Seconds(60),
                    IpAddressType = IpAddressType.IPV4,
                    SecurityGroup = albSecurityGroup
                });

            var webApiServiceSecurityGroup = new SecurityGroup(this, "WebApiServiceSecurityGroup",
                new SecurityGroupProps
                {
                    Vpc = vpc,
                    AllowAllOutbound = true
                });

            webApiServiceSecurityGroup.AddIngressRule(albSecurityGroup, Port.Tcp(80));

            var appListener = alb.AddListener("AppListener",
                new BaseApplicationListenerProps
                {
                    Port = 80,
                    Protocol = ApplicationProtocol.HTTP,
                    DefaultAction = ListenerAction.FixedResponse(404,
                        new FixedResponseOptions
                        {
                            ContentType = "text/plain",
                            MessageBody = "This is not here..."
                        })
                });

            new CfnOutput(this, "ClusterName",
                new CfnOutputProps
                {
                    ExportName = Globals.GetDeployEnvironment(this).PutEnvNamePrefixWithDash("ClusterName"),
                    Value = cluster.ClusterName
                });

            new CfnOutput(this, "WebApiServiceSecurityGroupId",
                new CfnOutputProps
                {
                    ExportName = Globals.GetDeployEnvironment(this).PutEnvNamePrefixWithDash("WebApiServiceSecurityGroupId"),
                    Value = albSecurityGroup.SecurityGroupId
                });

            new CfnOutput(this, "AppListenerArn",
                new CfnOutputProps
                {
                    ExportName = Globals.GetDeployEnvironment(this).PutEnvNamePrefixWithDash("AppListenerArn"),
                    Value = appListener.ListenerArn
                });
        }
    }
}
