namespace Demo.MemberService
{
    using System;
    using Contracts;
    using MassTransit;
    using MassTransit.Scheduling;


    class AddressValidationConsumer :
        Consumes<MemberAddressAdded>.Context,
        Consumes<ValidateAddress>.Context
    {
        public void Consume(IConsumeContext<MemberAddressAdded> context)
        {
            Console.WriteLine("Do some address validation here for: {0}", context.Message.MemberId);

            var command = new ValidateAddressCommand(NewId.NextGuid(), DateTime.UtcNow, context.Message.MemberId);

            context.Bus.ScheduleMessage(DateTime.UtcNow + TimeSpan.FromSeconds(5), command);
        }


        public void Consume(IConsumeContext<ValidateAddress> message)
        {
            Console.WriteLine("Validate Address: {0}", message.Message.MemberId);
        }


        class ValidateAddressCommand :
            ValidateAddress
        {
            public ValidateAddressCommand(Guid commandId, DateTime timestamp, string memberId)
            {
                CommandId = commandId;
                Timestamp = timestamp;
                MemberId = memberId;
            }

            public Guid CommandId { get; private set; }
            public DateTime Timestamp { get; private set; }
            public string MemberId { get; private set; }
        }
    }


    public interface ValidateAddress
    {
        Guid CommandId { get; }
        DateTime Timestamp { get; }
        string MemberId { get; }
    }
}