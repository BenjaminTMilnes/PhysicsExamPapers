using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace PhysicsExamPapers.Content.Physics.GeneralRelativity
{
    [XMLTemplateReference("physics__general_relativity__evaluate_the_kronecker_delta")]
    public class EvaluateTheKroneckerDelta : QuestionGenerator
    {
        protected XMLImporter _xmlImporter;
        protected TextResolver _textResolver;

        public EvaluateTheKroneckerDelta(XMLImporter xmlImporter, TextResolver textResolver)
        {
            _xmlImporter = xmlImporter;
            _textResolver = textResolver;
        }

        public override IQuestion Generate(Random random)
        {
            var alpha = GenerateRandomNumberBetweenLimits(random, 0, 4);
            var beta = GenerateRandomNumberBetweenLimits(random, 0, 4);

            return Generate(alpha, beta);
        }

        public IQuestion Generate(int alpha, int beta)
        {
            var type = typeof(EvaluateTheKroneckerDelta);
            var xmlTemplateReference = type.GetCustomAttribute<XMLTemplateReferenceAttribute>().Reference;

            var xmlResource = _xmlImporter.Import(xmlTemplateReference);
            var unresolvedQuestionContent = xmlResource.GetQuestionContent();

            var question = new Question();

            question.Content = _textResolver.Resolve(unresolvedQuestionContent, alpha, beta);

            var correctAnswer = new Answer();
            correctAnswer.Type = AnswerType.Number;

            if (alpha == beta)
            {
                correctAnswer.Content = "1";
            }
            else
            {
                correctAnswer.Content = "0";
            }

            question.CorrectAnswers.Add(correctAnswer);

            var numberOfHints = xmlResource.GetNumberOfHints();

            for (var a = 0; a < numberOfHints; a++)
            {
                var hint = new Hint();
                hint.Content = xmlResource.GetHintContent(a + 1);

                question.Hints.Add(hint);
            }

            return question;
        }
    }
}
