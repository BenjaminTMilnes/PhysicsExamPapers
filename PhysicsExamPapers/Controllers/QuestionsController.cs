using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using PhysicsExamPapers.Content;

namespace PhysicsExamPapers.Controllers
{
    public class QuestionsController : ApiController
    {
        public string Get(int id)
        {
            var path = Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "static_content");
            var xmlImporter = new XMLImporter(path);
            var questionGenerator = new PhysicsExamPapers.Content.Physics.GeneralRelativity.EvaluateTheKroneckerDelta(xmlImporter, new TextResolver());
            var question = questionGenerator.Generate();

            return question.Content;
        }
    }
}
