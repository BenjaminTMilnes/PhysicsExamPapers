using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysicsExamPapers.Content.Layout;

namespace PhysicsExamPapers.Content
{
    public class NonRandomQuestionGenerator : QuestionGenerator
    {
        public string XMLTemplateReference { get; protected set; }

        public NonRandomQuestionGenerator(string xmlTemplateReference, XMLImporter xmlImporter, TextResolver textResolver, LayoutConverter layoutConverter) : base(xmlImporter, textResolver, layoutConverter)
        {
            XMLTemplateReference = xmlTemplateReference;
        }

        public override IQuestion Generate(Random random)
        {
            var model = new Model();

            return Generate(model);
        }

        public override IQuestion Generate(Model model)
        {
            var xmlTemplate = GetXMLTemplate(XMLTemplateReference);

            return Generate(xmlTemplate, model);
        }
    }
}
