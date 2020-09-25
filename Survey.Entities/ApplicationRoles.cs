using Microsoft.AspNetCore.Identity;
using Survey.Entities.Standards;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Survey.Entities
{
    public class ApplicationRoles : IdentityRole<long>, IBaseProperties
    {
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
