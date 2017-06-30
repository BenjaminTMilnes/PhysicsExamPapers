using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content.Physics.GeneralRelativity
{
    public class QuestionSet : PhysicsExamPapers.Content.QuestionSet
    {
        protected XMLImporter _xmlImporter { get; set; }
        protected TextResolver _textResolver { get; set; }

        public QuestionSet(string basePath) : base()
        {
            _xmlImporter = new XMLImporter(basePath);
            _textResolver = new TextResolver();

            Generators.Add(GetGeneratorXMLTemplateReference(typeof(EvaluateTheKroneckerDelta)), new EvaluateTheKroneckerDelta(_xmlImporter, _textResolver));
        }
    }
}
