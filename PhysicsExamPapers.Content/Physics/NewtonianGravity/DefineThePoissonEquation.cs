using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysicsExamPapers.Content.Layout;

namespace PhysicsExamPapers.Content.Physics.NewtonianGravity
{
    [XMLTemplateReference("Physics_NewtonianGravity_DefineThePoissonEquation")]
    public class DefineThePoissonEquation : QuestionGenerator
    {
        public DefineThePoissonEquation(XMLImporter xmlImporter, TextResolver textResolver, LayoutConverter layoutConverter) : base(xmlImporter, textResolver, layoutConverter) { }

        public override IQuestion Generate(Random random)
        {
            var model = new Model();

            return Generate(model);
        }

        public override IQuestion Generate(Model model)
        {
            var type = typeof(DefineThePoissonEquation);
            var xmlTemplateReference = GetXMLTemplateReference(type);
            var xmlTemplate = GetXMLTemplate(xmlTemplateReference);

            return Generate(xmlTemplate, model);
        }
    }
}
