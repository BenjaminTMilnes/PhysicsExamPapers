using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public abstract class QuestionSet : IQuestionSet
    {
        protected IDictionary<string, IQuestionGenerator> Generators { get; set; }

        public IQuestionGenerator GetGenerator(string reference)
        {
            return Generators[reference];
        }

        public QuestionSet()
        {
            Generators = new Dictionary<string, IQuestionGenerator>();
        }
    }
}
