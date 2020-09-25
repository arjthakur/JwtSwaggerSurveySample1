using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Survey.DTOs.Request
{
    public class Login
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>MySurveyAdmin</example>
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <example>Admin@Survey10001</example>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
