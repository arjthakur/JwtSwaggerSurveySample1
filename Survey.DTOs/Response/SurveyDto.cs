using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Survey.DTOs.Response
{
    public class SurveyDto
    {
        public long Id { get; set; }
        [MaxLength(250), Required]
        public string Title { get; set; }
        [MaxLength(1000), Required]
        public string Description { get; set; }
        public virtual ICollection<PageDto> Pages { get; set; }
    }
}
