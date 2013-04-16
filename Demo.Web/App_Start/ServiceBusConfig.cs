namespace Demo.Web
{
    using MassTransit;


    public static class ServiceBusConfig
    {
        public static void RegisterServiceBus()
        {
            Bus.Initialize(x =>
                {
                    x.UseRabbitMq();
                    x.ReceiveFrom("rabbitmq://localhost/demo/website");
                });
        }

        public static void StopServiceBus()
        {
            Bus.Shutdown();
        }
    }
}