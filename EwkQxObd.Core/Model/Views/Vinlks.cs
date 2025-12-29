using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EwkQxObd.Core.Model.Views;

public class Vinlks
{
    public string? SerialNumber { get; set; }

    public int? ContractNumber { get; set; }
    public int? SiteContract { get; set; }

    public DateTime? RecordedAt { get; set; }

    public string? Country { get; set; }

    public string? Region { get; set; }

    public string? AccountName { get; set; }

    public int? AccountNumber { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public Guid? LinkedAccount { get; set; }

    public string? System { get; set; }

    public string? InstrumentName { get; set; }

    public string? InstrumentGroup { get; set; }

    public string? NetworkName { get; set; }

    public int? NetworkId { get; set; }

    public DateTime? QueryTimeStamp { get; set; }

    public bool IsMissingContract { get; set; }

    public bool IsMissingDblink { get; set; }

    public bool IsMissingSystem { get; set; }

    public bool IsMissingNetwork { get; set; }

    public bool IsMissingOrgSync { get; set; }

    public int? ContractObjId { get; set; }

    public string? Description { get; set; }
    public string? InstrumentType { get; set; }

    [Column(nameof(ShipToId), TypeName = "int")]
    public int? ShipToId { get; set; } = int.MinValue;

    [Column(nameof(ShipToName), TypeName = "nvarchar(64)")]
    public string? ShipToName { get; set; } = string.Empty;
}
