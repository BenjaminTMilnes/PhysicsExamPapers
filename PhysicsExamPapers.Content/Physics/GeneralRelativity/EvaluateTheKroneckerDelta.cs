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
        public EvaluateTheKroneckerDelta(XMLImporter xmlImporter, TextResolver textResolver) : base(xmlImporter, textResolver) { }

        public override IQuestion Generate(Random random)
        {
            var alpha = GenerateRandomNumberBetweenLimits(random, 0, 4);
            var beta = GenerateRandomNumberBetweenLimits(random, 0, 4);

            var model = new Model();
            model.Add("alpha", alpha);
            model.Add("beta", beta);

            return Generate(model);
        }

        public IQuestion Generate(Model model)
        {
            var type = typeof(EvaluateTheKroneckerDelta);
            var xmlTemplateReference = GetXMLTemplateReference(type);
            var xmlTemplate = GetXMLTemplate(xmlTemplateReference);

            var unresolvedQuestionContent = xmlTemplate.GetQuestionContent();

            var question = new Question();

            question.Model = model;
            question.Content = _textResolver.Resolve(unresolvedQuestionContent, model);

            var xmlImporter = new Layout.XMLImporter();
            var htmlExporter = new Layout.HTMLExporter();
            var document = new System.Xml.XmlDocument();

            document.LoadXml(question.Content);

            question.Content = htmlExporter.ExportElement(xmlImporter.ImportElement(document.ChildNodes.Item(0)));

            question.CorrectAnswers = CalculateCorrectAnswers(model);
            question.Hints = GenerateHints(xmlTemplate, model);

            return question;
        }

        protected IList<IAnswer> CalculateCorrectAnswers(Model model)
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

        protected IList<IHint> GenerateHints(XMLResource xmlTemplate, Model model)
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
