using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public abstract class QuestionGenerator : IQuestionGenerator
    {
        protected string StaticContentName { get; set; }

        public abstract IQuestion Generate();
    }
}
