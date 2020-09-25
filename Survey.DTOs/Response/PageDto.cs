using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Survey.DTOs.Response
{
    public class PageDto
    {
        [MaxLength(250), Required]
        public string Title { get; set; }
        [MaxLength(1000), Required]
        public string Description { get; set; }
        public int Order { get; set; }
        public long SurveyId { get; set; }
        public virtual SurveyDto Survey { get; set; }
    }
}
