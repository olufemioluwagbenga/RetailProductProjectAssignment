using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockaProSSO.Core.Domain.Entities
{
    [Table("AdminAuthorize")]
    public class AdminAuthorize : BaseEntity<int>
    {
        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        [Required]

        public int ClientKey { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public bool Enabled { get; set; }
    }
}
