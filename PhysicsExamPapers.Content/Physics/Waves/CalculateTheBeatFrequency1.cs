using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhysicsExamPapers.Content.Layout;

namespace PhysicsExamPapers.Content.Physics.Waves
{
    [XMLTemplateReference("Physics_Waves_CalculateTheBeatFrequency1")]
    public class CalculateTheBeatFrequency1 : QuestionGenerator
    {
        public CalculateTheBeatFrequency1(XMLImporter xmlImporter, TextResolver textResolver, LayoutConverter layoutConverter) : base(xmlImporter, textResolver, layoutConverter) { }

        public override IQuestion Generate(Random random)
        {
            var f1 = GenerateRandomNumberBetweenLimits(random, 50, 2000);
            var df = GenerateRandomNumberBetweenLimits(random, -12, 12);
            var f2 = f1 + df;
            var beatFrequency = Math.Abs(2 * df);

            var model = new Model();
            model.Add("f1", f1);
            model.Add("f2", f2);
            model.Add("df", df);
            model.Add("bf", beatFrequency);

            return Generate(model);
        }

        public override IQuestion Generate(Model model)
        {
            var type = typeof(CalculateTheBeatFrequency1);
            var xmlTemplateReference = GetXMLTemplateReference(type);
            var xmlTemplate = GetXMLTemplate(xmlTemplateReference);

            return Generate(xmlTemplate, model);
        }

        protected override IList<IAnswer> CalculateCorrectAnswers(XMLResource xmlTemplate, Model model)
        {
            var correctAnswers = new List<IAnswer>();

            var correctAnswer = new Answer();
            correctAnswer.Type = AnswerType.Number;
            correctAnswer.Content = (int)model["bf"];

            correctAnswers.Add(correctAnswer);

            return correctAnswers;
        }
    }
}
