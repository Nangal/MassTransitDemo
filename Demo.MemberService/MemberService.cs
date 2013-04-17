namespace Demo.MemberService
{
    using Automatonymous;
    using Core;
    using MassTransit;
    using MassTransit.Saga;
    using Topshelf;


    class MemberService :
        ServiceControl
    {
        readonly NewAddressStateMachine _stateMachine;
        readonly ISagaRepository<NewAddressState> _stateMachineRepository;
        IServiceBus _bus;

        public MemberService()
        {
            _stateMachine = new NewAddressStateMachine();
            _stateMachineRepository = new InMemorySagaRepository<NewAddressState>();
        }

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

                            s.StateMachineSaga(_stateMachine, _stateMachineRepository, c =>
                                {
                                    c.Correlate(_stateMachine.AddressAdded,
                                        (state, message) => state.OriginatingCommandId == message.CommandId)
                                     .SelectCorrelationId(message => message.EventId);

                                    c.Correlate(_stateMachine.AddressApproved,
                                        (state, message) => state.MemberId == message.MemberId);
                                });
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