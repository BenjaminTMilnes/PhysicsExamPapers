using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;
using PhysicsExamPapers.Content;

namespace PhysicsExamPapers.Controllers
{
    public class QuestionsController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "static_content");
            var xmlImporter = new XMLImporter(path);
            var textResolver = new TextResolver();

            var questionGenerator = new PhysicsExamPapers.Content.Physics.GeneralRelativity.EvaluateTheKroneckerDelta(xmlImporter, textResolver);
            var question = questionGenerator.Generate();

            var json = JsonConvert.SerializeObject(question, Formatting.Indented);

            var response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent(json, System.Text.Encoding.UTF8);

            return response;
        }
    }
}
