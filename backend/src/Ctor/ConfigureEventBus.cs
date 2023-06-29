using Ctor.Application.BusEventHandlers.BusTestReceive;
using Ctor.Application.BusEventHandlers.ReportCreated;
using Ctor.Application.Common.Interfaces.Bus;
using Ctor.Infrastructure.Services;

namespace Ctor;

public static class ConfigureEventBus
{
    public static async void ConfigureBus(this IApplicationBuilder app)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            await eventBus.Subscribe<ReportCreatedEvent, ReportCreatedEventHandler>();
            await eventBus.Subscribe<BusTestReceiveEvent, BusTestReceiveEventHandler>();
        }
    }
}