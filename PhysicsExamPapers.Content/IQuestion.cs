using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public interface IQuestion
    {
        Model Model { get; set; }
        string Content { get; set; }
        IList<IAnswer> CorrectAnswers { get; set; }
        IList<IAnswer> IncorrectAnswers { get; set; }
        IList<IHint> Hints { get; set; }
    }
}
