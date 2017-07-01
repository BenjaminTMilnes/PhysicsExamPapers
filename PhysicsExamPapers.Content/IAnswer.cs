using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public interface IAnswer
    {
        AnswerType Type { get; set; }
        object Content { get; set; }
    }
}
