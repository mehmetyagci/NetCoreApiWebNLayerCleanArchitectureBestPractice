using App.Domain.Options;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication;

namespace App.Bus;

public static class BusExtension
{
    public static void AddBusExt(this IServiceCollection services, IConfiguration configuration)
    {
        //var serviceBusOption = configuration.GetSection(nameof(ServiceBusOption)).Get<ServiceBusOption>();

        //services.AddMassTransit(x =>
        //{
        //    x.UsingRabbitMq((context, cfg) =>
        //    {
        //        cfg.Host(new Uri(serviceBusOption!.Url),
        //            h => {
        //                h.Username("cgewpizx"); // örnek kullanıcı adı
        //                h.Password("sDnYjPe7gMpZZBo7nP-uv_4cAYOn89xv"); // CloudAMQP üzerinden alınmalı

        //            });
        //    });
        //});

        var uri = new Uri(configuration["ServiceBusOption:Url"]);

        var userInfo = uri.UserInfo.Split(':');
        var username = userInfo[0];
        var password = userInfo[1];
        var host = uri.Host;
        var virtualHost = uri.AbsolutePath.TrimStart('/'); // "cgewpizx"

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, virtualHost, h =>
                {
                    h.Username(username);
                    h.Password(password);
                    h.UseSsl(s =>
                    {
                        s.Protocol = SslProtocols.Tls12; // TLS güvenli bağlantı
                    });
                });
            });
        });
    }
}