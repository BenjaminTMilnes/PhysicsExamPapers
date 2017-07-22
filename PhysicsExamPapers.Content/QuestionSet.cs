using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PhysicsExamPapers.Content.Layout;

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

        protected XMLImporter _xmlImporter { get; set; }
        protected TextResolver _textResolver { get; set; }
        protected LayoutConverter _layoutConverter { get; set; }

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

        public QuestionSet(string basePath)
        {
            Generators = new Dictionary<string, IQuestionGenerator>();

            _xmlImporter = new XMLImporter(basePath);
            _textResolver = new TextResolver();
            _layoutConverter = new LayoutConverter();
        }

        protected void AddNonRandomQuestionGenerator(string xmlTemplateReference)
        {
            Generators.Add(xmlTemplateReference, new NonRandomQuestionGenerator(xmlTemplateReference, _xmlImporter, _textResolver, _layoutConverter));
        }
    }
}
