namespace Demo.Contracts
{
    using System;


    public interface MemberAddressApproved
    {
        Guid EventId { get; }
        DateTime Timestamp { get; }
        string MemberId { get; }
        string Approver { get; }
    }
}