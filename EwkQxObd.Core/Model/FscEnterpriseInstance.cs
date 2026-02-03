using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.Core.Model
{
    [Table("EnterpriseInstance", Schema = "fsc")]
    public class FscEnterpriseInstance
    {
        [Key]
        public int Id { get; set; }


        public required Uri ServerAddress { get; set; }
        public required string InstanceName { get; set; }

        public required string Version { get; set; }

        public int PortTcp { get; set; }
        public int PortHttp { get; set; }
        public int PortHttps { get; set; }

        public Guid SystemGuid { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        [Column("Customer")]
        public int CustomerId { get; set; }
        public required IqxOrganization Customer { get; set; }


        public IEnumerable<EqoContract>? EnterpriseContracts { get; set; }

    }
}
