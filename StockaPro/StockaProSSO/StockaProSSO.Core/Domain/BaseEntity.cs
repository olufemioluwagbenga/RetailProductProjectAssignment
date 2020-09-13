using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StockaProSSO.Core.Domain
{
    public class BaseEntity<Tid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Tid Id { get; set; }
    }
}
