using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public class Question : IQuestion
    {
        public string Content { get; set; }
        public IList<IAnswer> CorrectAnswers { get; set; }
        public IList<IHint> Hints { get; set; }

        public Question()
        {
            CorrectAnswers = new List<IAnswer>();
            Hints = new List<IHint>();
        }
    }
}
