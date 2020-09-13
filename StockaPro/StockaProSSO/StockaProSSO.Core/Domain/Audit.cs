

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockaProSSO.Core.Domain
{
    [Table("Audit")]
    public partial class Audit : BaseEntity<long>
    {


        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Ip { get; set; }

        public DateTime TimeStamp { get; set; }

        public int SectionId { get; set; }
    }
}
