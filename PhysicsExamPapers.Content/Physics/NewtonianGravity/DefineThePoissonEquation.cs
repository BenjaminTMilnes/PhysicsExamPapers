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

        public IQuestion Generate(Model model)
        {
            var type = typeof(DefineThePoissonEquation);
            var xmlTemplateReference = GetXMLTemplateReference(type);
            var xmlTemplate = GetXMLTemplate(xmlTemplateReference);

            var unresolvedQuestionContent = xmlTemplate.GetQuestionContent();

            var question = new Question();

            question.Model = model;

            question.Content = _textResolver.Resolve(unresolvedQuestionContent, model);
            question.Content = _layoutConverter.ConvertLayout(question.Content);

            question.CorrectAnswers = CalculateCorrectAnswers(xmlTemplate, model);
            question.IncorrectAnswers = CalculateIncorrectAnswers(xmlTemplate, model);
            question.Hints = GenerateHints(xmlTemplate, model);

            return question;
        }

        protected IList<IAnswer> CalculateCorrectAnswers(XMLResource xmlTemplate, Model model)
        {
            var correctAnswers = new List<IAnswer>();
            var numberOfCorrectAnswers = xmlTemplate.GetNumberOfCorrectAnswers();

            for (var a = 0; a < numberOfCorrectAnswers; a++)
            {
                var correctAnswer = new Answer();
                correctAnswer.Type = AnswerType.Expression;
                var unresolvedAnswerContent = xmlTemplate.GetCorrectAnswerContent(a + 1);
                correctAnswer.Content = _layoutConverter.ConvertLayout(unresolvedAnswerContent);

                correctAnswers.Add(correctAnswer);
            }

            return correctAnswers;
        }

        protected IList<IAnswer> CalculateIncorrectAnswers(XMLResource xmlTemplate, Model model)
        {
            var incorrectAnswers = new List<IAnswer>();
            var numberOfIncorrectAnswers = xmlTemplate.GetNumberOfIncorrectAnswers();

            for (var a = 0; a < numberOfIncorrectAnswers; a++)
            {
                var incorrectAnswer = new Answer();
                incorrectAnswer.Type = AnswerType.Expression;
                var unresolvedAnswerContent = xmlTemplate.GetIncorrectAnswerContent(a + 1);
                incorrectAnswer.Content = _layoutConverter.ConvertLayout(unresolvedAnswerContent);

                incorrectAnswers.Add(incorrectAnswer);
            }

            return incorrectAnswers;
        }

        protected IList<IHint> GenerateHints(XMLResource xmlTemplate, Model model)
        {
            var hints = new List<IHint>();
            var numberOfHints = xmlTemplate.GetNumberOfHints();

            for (var a = 0; a < numberOfHints; a++)
            {
                var hint = new Hint();
                hint.Content = xmlTemplate.GetHintContent(a + 1);
                hint.Content = _layoutConverter.ConvertLayout(hint.Content);

                hints.Add(hint);
            }

            return hints;
        }
    }
}
