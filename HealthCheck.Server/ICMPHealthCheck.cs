using System.Net.NetworkInformation;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheck.Server;

public class ICMPHealthCheck : IHealthCheck
{
   private readonly string _host;
   private readonly int _healthyRoundtripTime;

   public ICMPHealthCheck(string host, int healthyRoundtripTime)
   {
       _healthyRoundtripTime = healthyRoundtripTime;
       _host = host;
   }


   public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            using var ping = new Ping();
            var reply = await ping.SendPingAsync(_host);

            switch (reply.Status)
            {
                case IPStatus.Success:
                    var msg = $"ICMP to {_host} took {reply.RoundtripTime} ms.";
                    return (reply.RoundtripTime > _healthyRoundtripTime)
                        ? HealthCheckResult.Degraded(msg)
                        : HealthCheckResult.Healthy(msg);
                default:
                    var err = $"ICMP to {_host} failed {reply.Status}.";
                    return HealthCheckResult.Unhealthy(err);
            }
        }
        catch(Exception ex)
        {
            var err = $"ICMP failed: {ex.Message}.";
            return HealthCheckResult.Unhealthy(err);
        }
    }
}