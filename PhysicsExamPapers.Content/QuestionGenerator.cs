using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PhysicsExamPapers.Content.Layout;

namespace PhysicsExamPapers.Content
{
    public abstract class QuestionGenerator : IQuestionGenerator
    {
        protected XMLImporter _xmlImporter;
        protected TextResolver _textResolver;
        protected LayoutConverter _layoutConverter;

        public QuestionGenerator(XMLImporter xmlImporter, TextResolver textResolver, LayoutConverter layoutConverter)
        {
            _xmlImporter = xmlImporter;
            _textResolver = textResolver;
            _layoutConverter = layoutConverter;
        }

        protected string GetXMLTemplateReference(Type type)
        {
            return type.GetCustomAttribute<XMLTemplateReferenceAttribute>().Reference;
        }

        protected XMLResource GetXMLTemplate(string xmlTemplateReference)
        {
            return _xmlImporter.Import(xmlTemplateReference);
        }

        public IQuestion Generate()
        {
            return Generate(new Random());
        }

        public abstract IQuestion Generate(Random random);

        public abstract IQuestion Generate(Model model);

        protected virtual IQuestion Generate(XMLResource xmlTemplate, Model model)
        {
            var unresolvedQuestionContent = xmlTemplate.GetQuestionContent();
            var resolvedQuestionContent = _textResolver.Resolve(unresolvedQuestionContent, model);
            var layout = _layoutConverter.ConvertLayout(resolvedQuestionContent);

            var question = new Question();

            question.Model = model;
            question.Content = layout;
            question.CorrectAnswers = CalculateCorrectAnswers(xmlTemplate, model);
            question.IncorrectAnswers = CalculateIncorrectAnswers(xmlTemplate, model);
            question.Hints = GenerateHints(xmlTemplate, model);

            return question;
        }

        protected virtual IList<IAnswer> CalculateCorrectAnswers(XMLResource xmlTemplate, Model model)
        {
            var correctAnswers = new List<IAnswer>();
            var numberOfCorrectAnswers = xmlTemplate.GetNumberOfCorrectAnswers();

            for (var a = 0; a < numberOfCorrectAnswers; a++)
            {
                var unresolvedAnswerContent = xmlTemplate.GetCorrectAnswerContent(a + 1);
                var layout = _layoutConverter.ConvertLayout(unresolvedAnswerContent);

                var correctAnswer = new Answer();

                correctAnswer.Type = AnswerType.Expression;
                correctAnswer.Content = layout;

                correctAnswers.Add(correctAnswer);
            }

            return correctAnswers;
        }

        protected virtual IList<IAnswer> CalculateIncorrectAnswers(XMLResource xmlTemplate, Model model)
        {
            var incorrectAnswers = new List<IAnswer>();
            var numberOfIncorrectAnswers = xmlTemplate.GetNumberOfIncorrectAnswers();

            for (var a = 0; a < numberOfIncorrectAnswers; a++)
            {
                var unresolvedAnswerContent = xmlTemplate.GetIncorrectAnswerContent(a + 1);
                var layout = _layoutConverter.ConvertLayout(unresolvedAnswerContent);

                var incorrectAnswer = new Answer();

                incorrectAnswer.Type = AnswerType.Expression;
                incorrectAnswer.Content = layout;

                incorrectAnswers.Add(incorrectAnswer);
            }

            return incorrectAnswers;
        }

        protected virtual IList<IHint> GenerateHints(XMLResource xmlTemplate, Model model)
        {
            var hints = new List<IHint>();
            var numberOfHints = xmlTemplate.GetNumberOfHints();

            for (var a = 0; a < numberOfHints; a++)
            {
                var unresolvedQuestionContent = xmlTemplate.GetHintContent(a + 1);
                var layout = _layoutConverter.ConvertLayout(unresolvedQuestionContent);

                var hint = new Hint();

                hint.Content = layout;

                hints.Add(hint);
            }

            return hints;
        }

        protected int GenerateRandomNumberBetweenLimits(Random random, int lowerLimit, int upperLimit)
        {
            return (int)Math.Round(random.NextDouble() * (upperLimit - lowerLimit) + lowerLimit);
        }
    }
}
