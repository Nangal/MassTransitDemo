namespace Demo.Core
{
    using System;
    using Automatonymous;
    using MassTransit;


    public class NewAddressState :
        SagaStateMachineInstance
    {
        public NewAddressState(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public NewAddressState()
        {
        }

        public string MemberId { get; set; }
        public Guid OriginatingCommandId { get; set; }
        public State CurrentState { get; set; }
        public string Approver { get; set; }
        public Guid CorrelationId { get; set; }
        public IServiceBus Bus { get; set; }
    }
}