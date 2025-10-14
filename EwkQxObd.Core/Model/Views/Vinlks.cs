using System;
using System.Collections.Generic;

namespace EwkQxObd.Core.Model.Views;

public class Vinlks
{
    public string? SerialNumber { get; set; }

    public long? ContractNumber { get; set; }

    public DateTime? RecordedAt { get; set; }

    public string? Country { get; set; }

    public string? Region { get; set; }

    public long? PartnerId { get; set; }

    public string? OrgName { get; set; }

    public string? AccountNumber { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public Guid? LinkedAccount { get; set; }

    public string? System { get; set; }

    public string? InstrumentName { get; set; }

    public string? InstrumentGroup { get; set; }

    public string? NetworkName { get; set; }

    public long? NetworkId { get; set; }

    public DateTime? QueryTimeStamp { get; set; }

    public bool IsMissingContract { get; set; }

    public bool IsMissingDblink { get; set; }

    public bool IsMissingSystem { get; set; }

    public bool IsMissingNetwork { get; set; }

    public long? ContractObjId { get; set; }

    public string? Description { get; set; }
    public string? InstrumentType { get; set; }
}
