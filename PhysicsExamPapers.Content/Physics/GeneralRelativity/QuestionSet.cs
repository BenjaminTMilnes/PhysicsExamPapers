using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysicsExamPapers.Content.Layout;
using PhysicsExamPapers.Content.Physics.NewtonianGravity;

namespace PhysicsExamPapers.Content.Physics.GeneralRelativity
{
    public class QuestionSet : PhysicsExamPapers.Content.QuestionSet
    {
        public QuestionSet(string basePath) : base(basePath)
        {
            Generators.Add(GetGeneratorXMLTemplateReference(typeof(EvaluateTheKroneckerDelta)), new EvaluateTheKroneckerDelta(_xmlImporter, _textResolver, _layoutConverter));
            Generators.Add(GetGeneratorXMLTemplateReference(typeof(SimplifyKroneckerDeltas)), new SimplifyKroneckerDeltas(_xmlImporter, _textResolver, _layoutConverter));
            Generators.Add(GetGeneratorXMLTemplateReference(typeof(SimplifyKroneckerDeltas2)), new SimplifyKroneckerDeltas2(_xmlImporter, _textResolver, _layoutConverter));

            AddNonRandomQuestionGenerator("Physics_NewtonianGravity_DefineThePoissonEquation");
            AddNonRandomQuestionGenerator("Mathematics_Hyperbolae_DefineAHyperbola1");
        }
    }
}