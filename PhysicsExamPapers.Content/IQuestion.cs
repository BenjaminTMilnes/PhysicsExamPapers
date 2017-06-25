using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public interface IQuestion
    {
        string Content { get; set; }
        IList<IAnswer> CorrectAnswers { get; set; }
    }
}
