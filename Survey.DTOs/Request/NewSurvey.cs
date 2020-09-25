using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Survey.DTOs.Request
{
    public class NewSurvey
    {
        [MaxLength(250), Required]
        public string Title { get; set; }
        [MaxLength(1000), Required]
        public string Description { get; set; }
    }
}
