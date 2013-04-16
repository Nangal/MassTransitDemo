namespace Demo.MemberService
{
    using System;
    using Contracts;
    using MassTransit;


    public class SaveMemberAddressConsumer :
        Consumes<SaveMemberAddress>.Context
    {
        public void Consume(IConsumeContext<SaveMemberAddress> context)
        {
            // do something with address, like, you know, save it

            Console.WriteLine("Processing: {0}", context.Message.MemberId);

            context.Bus.Publish(new MemberAddressAddedEvent(context.Message.CommandId, context.Message.MemberId,
                context.Message.Street, context.Message.City, context.Message.State, context.Message.Zip));
        }


        public class MemberAddressAddedEvent :
            MemberAddressAdded
        {
            public MemberAddressAddedEvent(Guid commandId, string memberId, string street, string city, string state, string zip)
            {
                EventId = NewId.NextGuid();
                Timestamp = DateTime.UtcNow;

                CommandId = commandId;
                MemberId = memberId;
                Street = street;
                City = city;
                State = state;
                Zip = zip;
            }

            public Guid EventId { get; private set; }
            public DateTime Timestamp { get; private set; }
            public Guid CommandId { get; private set; }
            public string MemberId { get; private set; }
            public string Street { get; private set; }
            public string City { get; private set; }
            public string State { get; private set; }
            public string Zip { get; private set; }
        }
    }
}