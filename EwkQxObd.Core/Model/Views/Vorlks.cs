using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model.Views
{
    public class Vorlks
    {
        [Column(nameof(AccountNumber), TypeName = "int")]
        public int AccountNumber { get; set; }

        [Column(nameof(AccountName), TypeName = "nvarchar(64)")]
        public string AccountName { get; set; } = string.Empty;

        [Column(nameof(Region), TypeName = "nvarchar(16)")]
        public string Region { get; set; } = string.Empty;

        [Column(nameof(Country), TypeName = "nvarchar(16)")]
        public string Country { get; set; } = string.Empty;

        [Column(nameof(City), TypeName = "nvarchar(64)")]
        public string City { get; set; } = string.Empty;

        [Column(nameof(Street), TypeName = "nvarchar(64)")]
        public string Street { get; set; } = string.Empty;

        [Column(nameof(GEIS_Guid), TypeName = "nvarchar(80)")]
        public Guid GEIS_Guid { get; set; }



        [Column(nameof(Country), TypeName = "nvarchar(16)")]
        public string System { get; set; } = string.Empty;

        [Column(nameof(NetworkId), TypeName = "int")]
        public int NetworkId { get; set; }

        [Column(nameof(NetworkName), TypeName = "nvarchar(64)")]
        public string NetworkName { get; set; } = string.Empty;

        [Column(nameof(Instruments), TypeName = "int")]
        public int Instruments { get; set; }

        [Column(nameof(Instruments_Linked), TypeName = "int")]
        public int Instruments_Linked { get; set; }
    }
}
