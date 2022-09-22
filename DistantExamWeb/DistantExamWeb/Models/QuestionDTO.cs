using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistantExamWeb.Models
{
    public class QuestionDTO
    {
        [Required]
        public string QuestionName { get; set; }
        [Required]
        public string AnswerName { get; set; }
    }
}
