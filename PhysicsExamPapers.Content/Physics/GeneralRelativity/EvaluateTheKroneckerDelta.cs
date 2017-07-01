using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PhysicsExamPapers.Content.Physics.GeneralRelativity
{
    [XMLTemplateReference("Physics_GeneralRelativity_EvaluateTheKroneckerDelta")]
    public class EvaluateTheKroneckerDelta : QuestionGenerator
    {
        public EvaluateTheKroneckerDelta(XMLImporter xmlImporter, TextResolver textResolver) : base(xmlImporter, textResolver) { }

        public override IQuestion Generate(Random random)
        {
            var alpha = GenerateRandomNumberBetweenLimits(random, 0, 4);
            var beta = GenerateRandomNumberBetweenLimits(random, 0, 4);

            return Generate(alpha, beta);
        }

        public IQuestion Generate(int alpha, int beta)
        {
            var type = typeof(EvaluateTheKroneckerDelta);
            var xmlTemplateReference = GetXMLTemplateReference(type);
            var xmlTemplate = GetXMLTemplate(xmlTemplateReference);

            var unresolvedQuestionContent = xmlTemplate.GetQuestionContent();

            var question = new Question();

            var model = new Model();
            model.Add("alpha", alpha);
            model.Add("beta", beta);

            question.Content = _textResolver.Resolve(unresolvedQuestionContent, model);
            question.CorrectAnswers = CalculateCorrectAnswers(alpha, beta);
            question.Hints = GenerateHints(xmlTemplate);

            return question;
        }

        protected IList<IAnswer> CalculateCorrectAnswers(int alpha, int beta)
        {
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

        protected IList<IHint> GenerateHints(XMLResource xmlTemplate)
        {
            var hints = new List<IHint>();
            var numberOfHints = xmlTemplate.GetNumberOfHints();

            for (var a = 0; a < numberOfHints; a++)
            {
                var hint = new Hint();
                hint.Content = xmlTemplate.GetHintContent(a + 1);

                hints.Add(hint);
            }

            return hints;
        }
    }
}
