namespace Demo.Core
{
    using System;
    using Automatonymous;
    using Contracts;


    public class NewAddressStateMachine :
        AutomatonymousStateMachine<NewAddressState>
    {
        public NewAddressStateMachine()
        {
            State(() => WaitingForApproval);

            Event(() => AddressAdded);
            Event(() => AddressApproved);

            InstanceState(x => x.CurrentState);

            Initially(
                When(AddressAdded)
                    .Then((state, message) =>
                        {
                            state.MemberId = message.MemberId;
                            state.OriginatingCommandId = message.CommandId;

                            Console.WriteLine("New instance on AddressAdded: {0}", state.MemberId);
                        })
                    .TransitionTo(WaitingForApproval)
                );

            During(WaitingForApproval,
                When(AddressApproved)
                    .Then((state, message) =>
                        {
                            state.Approver = message.Approver;
                            Console.WriteLine("Approved Address: {0} by {1}", state.MemberId, state.Approver);
                        })
                    .Finalize());
        }

        public State WaitingForApproval { get; set; }

        public Event<MemberAddressAdded> AddressAdded { get; set; }
        public Event<MemberAddressApproved> AddressApproved { get; set; }
    }
}