using Microsoft.AspNetCore.Identity;
using Survey.Entities.Standards;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Entities
{
    public class ApplicationUser : IdentityUser<long>, IBaseProperties
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
