using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PhysicsExamPapers.Content
{
    public abstract class QuestionGenerator : IQuestionGenerator
    {
        protected XMLImporter _xmlImporter;
        protected TextResolver _textResolver;

        public QuestionGenerator(XMLImporter xmlImporter, TextResolver textResolver)
        {
            _xmlImporter = xmlImporter;
            _textResolver = textResolver;
        }

        protected string GetXMLTemplateReference(Type type)
        {
            return type.GetCustomAttribute<XMLTemplateReferenceAttribute>().Reference;
        }

        protected XMLResource GetXMLTemplate(string xmlTemplateReference)
        {
            return _xmlImporter.Import(xmlTemplateReference);
        }

        public abstract IQuestion Generate(Random random);

        public IQuestion Generate()
        {
            return Generate(new Random());
        }

        protected int GenerateRandomNumberBetweenLimits(Random random, int lowerLimit, int upperLimit)
        {
            return (int)Math.Round(random.NextDouble() * (upperLimit - lowerLimit) + lowerLimit);
        }
    }
}
