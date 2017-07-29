using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PhysicsExamPapers.Content.Layout;

namespace PhysicsExamPapers.Content.Physics.GeneralRelativity
{
    [XMLTemplateReference("Physics_GeneralRelativity_SimplifyKroneckerDeltas")]
    public class SimplifyKroneckerDeltas : QuestionGenerator
    {
        public SimplifyKroneckerDeltas(XMLImporter xmlImporter, TextResolver textResolver, LayoutConverter layoutConverter) : base(xmlImporter, textResolver, layoutConverter) { }

        public override IQuestion Generate(Random random)
        {
            var greekLetters = new string[] { "\\gamma", "\\epsilon", "\\zeta", "\\eta", "\\theta", "\\iota", "\\kappa", "\\mu", "\\nu", "\\xi", "\\rho", "\\sigma" };
            var reorderedGreekLetters = ReorderRandomly<string>(random, greekLetters);

            var model = new Model();
            model.Add("gl1", reorderedGreekLetters[0]);
            model.Add("gl2", reorderedGreekLetters[1]);
            model.Add("gl3", reorderedGreekLetters[2]);
            model.Add("gl4", reorderedGreekLetters[3]);
            model.Add("gl5", reorderedGreekLetters[4]);
            model.Add("gl6", reorderedGreekLetters[5]);
            model.Add("gl7", reorderedGreekLetters[6]);
            model.Add("gl8", reorderedGreekLetters[7]);
            model.Add("gl9", reorderedGreekLetters[8]);
            model.Add("gl10", reorderedGreekLetters[9]);
            model.Add("gl11", reorderedGreekLetters[10]);
            model.Add("gl12", reorderedGreekLetters[11]);

            return Generate(model);
        }

        public override IQuestion Generate(Model model)
        {
            var type = typeof(SimplifyKroneckerDeltas);
            var xmlTemplateReference = GetXMLTemplateReference(type);
            var xmlTemplate = GetXMLTemplate(xmlTemplateReference);

            return Generate(xmlTemplate, model);
        }
    }
}
