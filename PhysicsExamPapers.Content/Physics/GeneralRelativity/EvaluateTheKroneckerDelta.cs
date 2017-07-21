using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PhysicsExamPapers.Content.Layout;

namespace PhysicsExamPapers.Content.Physics.GeneralRelativity
{
    [XMLTemplateReference("Physics_GeneralRelativity_EvaluateTheKroneckerDelta")]
    public class EvaluateTheKroneckerDelta : QuestionGenerator
    {
        public EvaluateTheKroneckerDelta(XMLImporter xmlImporter, TextResolver textResolver, LayoutConverter layoutConverter) : base(xmlImporter, textResolver, layoutConverter) { }

        public override IQuestion Generate(Random random)
        {
            var alpha = GenerateRandomNumberBetweenLimits(random, 0, 4);
            var beta = GenerateRandomNumberBetweenLimits(random, 0, 4);

            var model = new Model();
            model.Add("alpha", alpha);
            model.Add("beta", beta);

            return Generate(model);
        }

        public override IQuestion Generate(Model model)
        {
            var type = typeof(EvaluateTheKroneckerDelta);
            var xmlTemplateReference = GetXMLTemplateReference(type);
            var xmlTemplate = GetXMLTemplate(xmlTemplateReference);

            return Generate(xmlTemplate, model);
        }

        protected override IList<IAnswer> CalculateCorrectAnswers(XMLResource xmlTemplate, Model model)
        {
            var alpha = (int)model["alpha"];
            var beta = (int)model["beta"];

            var correctAnswers = new List<IAnswer>();

            var correctAnswer = new Answer();
            correctAnswer.Type = AnswerType.Number;

            if (alpha == beta)
            {
                correctAnswer.Content = 1;
            }
            else
            {
                correctAnswer.Content = 0;
            }

            correctAnswers.Add(correctAnswer);

            return correctAnswers;
        }
    }
}
