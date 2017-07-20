using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PhysicsExamPapers.Content
{
    public sealed class NoSuchQuestionTemplateException : Exception
    {
        private string _templateReference;

        public NoSuchQuestionTemplateException(string templateReference)
        {
            _templateReference = templateReference;
        }

        public override string ToString()
        {
            return $"No question template could be found with the reference '{_templateReference}'.";
        }
    }

    public abstract class QuestionSet : IQuestionSet
    {
        protected IDictionary<string, IQuestionGenerator> Generators { get; set; }

        protected string GetGeneratorXMLTemplateReference(Type type)
        {
            return type.GetCustomAttribute<XMLTemplateReferenceAttribute>().Reference;
        }

        public IQuestionGenerator GetGenerator(string reference)
        {
            if (!Generators.ContainsKey(reference))
            {
                throw new NoSuchQuestionTemplateException(reference);
            }

            return Generators[reference];
        }

        public QuestionSet()
        {
            Generators = new Dictionary<string, IQuestionGenerator>();
        }
    }
}
