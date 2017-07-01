using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public class Answer : IAnswer
    {
        public AnswerType Type { get; set; }
        public object Content { get; set; }
    }
}
