namespace Demo.Contracts
{
    using System;


    public interface SaveMemberAddress
    {
        Guid CommandId { get; }
        DateTime Timestamp { get; }
        string MemberId { get; }
        string Street { get; }
        string City { get; }
        string State { get; }
        string Zip { get; }
    }
}