namespace Demo.Contracts
{
    using System;


    public interface MemberAddressAdded
    {
        Guid EventId { get; }
        DateTime Timestamp { get; }
        Guid CommandId { get; }
        string MemberId { get; }
        string Street { get; }
        string City { get; }
        string State { get; }
        string Zip { get; }         
    }
}