namespace Demo.MemberService
{
    using Core;
    using MassTransit;
    using Topshelf;


    class MemberService :
        ServiceControl
    {
        IServiceBus _bus;

        public bool Start(HostControl hostControl)
        {
            _bus = ServiceBusFactory.New(x =>
                {
                    x.UseRabbitMq();
                    x.ReceiveFrom("rabbitmq://localhost/demo/member-service");

                    x.Subscribe(s =>
                        {
                            s.Consumer(() => new SaveMemberAddressConsumer());
                            s.Consumer(() => new AddressValidationConsumer());
                        });
                });

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            if (_bus != null)
            {
                _bus.Dispose();
                _bus = null;
            }

            return true;
        }
    }
}