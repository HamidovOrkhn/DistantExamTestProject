using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistantExamWeb.Models
{
    public class ConsoleDTO
    {
        [Required]
        public string QuestionKey { get; set; }
    }
}
