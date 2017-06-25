using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content.Physics.GeneralRelativity
{
    public class EvaluateTheKroneckerDelta : QuestionGenerator
    {
        protected XMLImporter _xmlImporter;
        protected TextResolver _textResolver;

        public EvaluateTheKroneckerDelta(XMLImporter xmlImporter, TextResolver textResolver)
        {
            _xmlImporter = xmlImporter;
            _textResolver = textResolver;

            StaticContentName = "physics__general_relativity__evaluate_the_kronecker_delta";
        }

        public override IQuestion Generate(Random random)
        {
            var xmlResource = _xmlImporter.Import(StaticContentName);
            var unresolvedQuestionContent = xmlResource.GetQuestionContent();

            var question = new Question();

            var alpha = GenerateRandomNumberBetweenLimits(random, 0, 4);
            var beta = GenerateRandomNumberBetweenLimits(random, 0, 4);

            question.Content = _textResolver.Resolve(unresolvedQuestionContent, alpha, beta);

            return question;
        }
    }
}
