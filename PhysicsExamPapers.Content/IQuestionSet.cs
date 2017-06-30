using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public interface IQuestionSet
    {
        IQuestionGenerator GetGenerator(string reference);
    }
}
