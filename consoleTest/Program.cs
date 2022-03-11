using Microsoft.Extensions.DependencyInjection;
using consoleTest;
using EasyNetworker.DependencyInjection;
using NetFwTypeLib;
using System.Net.Sockets;

EnableAppInFirewall();
var serviceProvider = new ServiceCollection()
            .AddSingleton<ConsoleApp>()
            .AddEasyNetworker()
            .RegisterPacketHandler<TestHandler, TestPacket>()
            .BuildServiceProvider();
serviceProvider.GetService<ConsoleApp>()?.Run();

static void EnableAppInFirewall()
{
#pragma warning disable CA1416 // Validate platform compatibility
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8629 // Nullable value type may be null.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");



    INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);

    var currentProfiles = fwPolicy2?.CurrentProfileTypes;

    // Let's create a new rule

    INetFwRule2 inboundTcpRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
    INetFwRule2 inboundUdpRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));


    inboundTcpRule.Enabled = true;
    inboundTcpRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
    
    inboundTcpRule.Protocol = (int)ProtocolType.Tcp;
    inboundTcpRule.Profiles = currentProfiles.Value;
    inboundTcpRule.Name = "EasyNetworker";
    //inboundTcpRule.LocalPorts = "1234";

    inboundUdpRule.Enabled = true;
    inboundUdpRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
    inboundUdpRule.Protocol = (int)ProtocolType.Udp;
    inboundUdpRule.Profiles = currentProfiles.Value;
    inboundUdpRule.Name = "EasyNetworker";
    //inboundUdpRule.LocalPorts = "1234";

    // Now add the rule

    INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
    firewallPolicy.Rules.Add(inboundTcpRule);
    firewallPolicy.Rules.Add(inboundUdpRule);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8629 // Nullable value type may be null.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CA1416 // Validate platform compatibility
}