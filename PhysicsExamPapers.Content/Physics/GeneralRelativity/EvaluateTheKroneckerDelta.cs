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

        public EvaluateTheKroneckerDelta(XMLImporter xmlImporter)
        {

            _xmlImporter = xmlImporter;

            StaticContentName = "physics__general_relativity__evaluate_the_kronecker_delta";
        }

        public override IQuestion Generate()
        {

            var xmlResource = _xmlImporter.Import(StaticContentName);





            throw new NotImplementedException();
        }
    }
}
