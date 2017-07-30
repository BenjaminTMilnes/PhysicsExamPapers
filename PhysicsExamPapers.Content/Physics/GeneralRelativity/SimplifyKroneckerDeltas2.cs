using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PhysicsExamPapers.Content.Layout;

namespace PhysicsExamPapers.Content.Physics.GeneralRelativity
{
    [XMLTemplateReference("Physics_GeneralRelativity_SimplifyKroneckerDeltas2")]
    public class SimplifyKroneckerDeltas2 : QuestionGenerator
    {
        public SimplifyKroneckerDeltas2(XMLImporter xmlImporter, TextResolver textResolver, LayoutConverter layoutConverter) : base(xmlImporter, textResolver, layoutConverter) { }

        public override IQuestion Generate(Random random)
        {
            var englishLetters = new string[] { "A", "B", "H", "I", "J", "K", "M", "N", "P", "R", "S", "U", "V", "W", "X" };
            var reorderedEnglishLetters = ReorderRandomly(random, englishLetters);

            var greekLetters = new string[] { "\\gamma", "\\epsilon", "\\zeta", "\\eta", "\\iota", "\\kappa", "\\mu", "\\nu", "\\xi", "\\rho", "\\sigma" };
            var reorderedGreekLetters = ReorderRandomly(random, greekLetters);

            var model = new Model();
            model.Add("gl1", reorderedGreekLetters[0]);
            model.Add("gl2", reorderedGreekLetters[1]);
            model.Add("gl3", reorderedGreekLetters[2]);
            model.Add("gl4", reorderedGreekLetters[3]);
            model.Add("el1", reorderedEnglishLetters[0]);

            return Generate(model);
        }

        public override IQuestion Generate(Model model)
        {
            var type = typeof(SimplifyKroneckerDeltas2);
            var xmlTemplateReference = GetXMLTemplateReference(type);
            var xmlTemplate = GetXMLTemplate(xmlTemplateReference);

            return Generate(xmlTemplate, model);
        }
    }
}
