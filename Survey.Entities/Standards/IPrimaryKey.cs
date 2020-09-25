using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Survey.Entities.Standards
{
    interface IPrimaryKey
    {
        [Key]
        public long Id { get; set; }
    }
}
