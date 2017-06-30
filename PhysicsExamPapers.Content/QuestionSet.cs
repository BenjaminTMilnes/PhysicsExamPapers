using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PhysicsExamPapers.Content
{
    public abstract class QuestionSet : IQuestionSet
    {
        protected IDictionary<string, IQuestionGenerator> Generators { get; set; }

        protected string GetGeneratorXMLTemplateReference(Type type)
        {
            return type.GetCustomAttribute<XMLTemplateReferenceAttribute>().Reference;
        }

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
