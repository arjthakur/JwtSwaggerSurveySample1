using Survey.Entities.Standards;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.Entities.Tables
{
    public class Page : BaseProperties, IBaseProperties
    {
        [MaxLength(250), Required]
        public string Title { get; set; }
        [MaxLength(1000), Required]
        public string Description { get; set; }
        public int Order { get; set; }
        public long SurveyId { get; set; }
        [ForeignKey("SurveyId")]
        public virtual Survey Survey { get; set; }
        //public virtual ICollection<Question> Qusestions { get; set; }
    }
}
