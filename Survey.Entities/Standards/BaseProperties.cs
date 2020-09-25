using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Survey.Entities.Standards
{
    public class BaseProperties : IBaseProperties
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedById { get; set; }
        public long ModifiedById { get; set; }
        [ForeignKey("CreatedById")]
        public virtual ApplicationUser CreatedBy { get; set; }
        [ForeignKey("ModifiedById")]
        public virtual ApplicationUser ModifiedBy { get; set; }
    }
}
