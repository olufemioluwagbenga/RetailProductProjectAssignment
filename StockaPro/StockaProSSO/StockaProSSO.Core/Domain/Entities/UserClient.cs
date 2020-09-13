using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockaProSSO.Core.Domain.Entities
{
    [Table("UserScope")]
    public partial class UserClient : BaseEntity<int>
    {
        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
