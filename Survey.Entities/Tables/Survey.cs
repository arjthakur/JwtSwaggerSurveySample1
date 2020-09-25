using Survey.Entities.Standards;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Survey.Entities.Tables
{
    public class Survey : BaseProperties, IBaseProperties
    {
        [MaxLength(250), Required]
        public string Title { get; set; }
        [MaxLength(1000), Required]
        public string Description { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
    }
}
