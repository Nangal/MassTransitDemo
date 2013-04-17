namespace Demo.Core
{
    using System;


    public interface ValidateAddress
    {
        Guid CommandId { get; }
        DateTime Timestamp { get; }
        string MemberId { get; }
    }
}