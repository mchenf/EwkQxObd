using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EwkQxObd.Core.Model.Views;


public class InstrumentLinkStatus
{

    [Column(nameof(SerialNumber), TypeName = "nvarchar(50)")]
    public string? SerialNumber { get; set; }

    [Column(nameof(ContractNumber), TypeName = "int")]
    public int? ContractNumber { get; set; }

    [Column(nameof(RecordedAt), TypeName = "datetime2(7)")]
    public DateTime? RecordedAt { get; set; }

    [Column(nameof(AccountNumber), TypeName = "int")]
    public int? AccountNumber { get; set; }

    [Column(nameof(LinkedAccount), TypeName = "uniqueidentifier")]
    public Guid? LinkedAccount { get; set; }

    public string? System { get; set; }
    public string? InstrumentName { get; set; }
    public string? InstrumentGroup { get; set; }
    public string? NetworkName { get; set; }
    public int? NetworkId { get; set; }

    [Column(nameof(StatusFlag), TypeName = "int")]
    public VinlkStatus StatusFlag { get; set; }

    public int? ContractObjId { get; set; }

    public string? Description { get; set; }
    public string? InstrumentType { get; set; }

    [Column(nameof(ValidTo), TypeName = "datetime2(7)")]
    public DateTime? ValidTo { get; set; }

    [Column(nameof(Valid), TypeName = "int")]
    public int? Valid { get; set; }

    
    public IqxOrganization ConnectedTo { get; set; }
    
    public IqxOrganization ShippedTo { get; set; }

    [NotMapped]
    public int? ExpiresIn
    {
        get
        {
            if (Valid == null)
            {
                return null;
            }

            int? di = Valid > 100 ? 99 : Valid;
            di = di < -100 ? -99 : di;

            return di;
        }
    }


    public int? ExpireStyle
    {
        get
        {
            if (ExpiresIn == null)
            {
                return null;
            }
            if (ExpiresIn == 99)
            {
                return 3;
            }
            if (ExpiresIn > 30)
            {
                return 2;
            }
            if (ExpiresIn >= 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
