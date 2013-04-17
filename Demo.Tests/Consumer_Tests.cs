namespace Demo.Tests
{
    using System;
    using Contracts;
    using Core;
    using MassTransit;
    using MassTransit.Testing;
    using NUnit.Framework;


    [TestFixture]
    public class When_saving_a_member_address
    {
        [Test]
        public void Should_have_received_the_message()
        {
            Assert.IsTrue(_test.Received.Any<SaveMemberAddress>());
        }

        [Test]
        public void Should_publish_the_member_address_added_event()
        {
            Assert.IsTrue(_test.Published.Any<MemberAddressAdded>());
        }

        ConsumerTest<BusTestScenario, SaveMemberAddressConsumer> _test;

        [TestFixtureSetUp]
        public void Setup()
        {
            _test = TestFactory
                .ForConsumer<SaveMemberAddressConsumer>()
                .InSingleBusScenario()
                .New(x =>
                    {
                        x.ConstructUsing(() => new SaveMemberAddressConsumer());
                        x.Send(new SaveMemberAddressMessage
                            {
                                CommandId = NewId.NextGuid(),
                                Timestamp = DateTime.UtcNow,
                                MemberId = "Joe",
                                Street = "123 American Way",
                                City = "Oakland",
                                State = "CA",
                                Zip = "94602"
                            });
                    });

            _test.Execute();
        }


        public class SaveMemberAddressMessage :
            SaveMemberAddress
        {
            public Guid CommandId { get; set; }
            public DateTime Timestamp { get; set; }
            public string MemberId { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
        }
    }
}