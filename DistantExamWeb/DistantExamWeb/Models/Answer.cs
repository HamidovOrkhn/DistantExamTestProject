using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistantExamWeb.Models
{
    public class Answer
    {
        public long Id { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public string Name { get; set; }
    }
}
