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
        public HttpResponseMessage Get(string template_reference)
        {
            var path = Path.Combine(HttpRuntime.AppDomainAppPath, "static_content");

            QuestionSet questionSet;

             questionSet = new PhysicsExamPapers.Content.Physics.GeneralRelativity.QuestionSet(path);
            var questionGenerator = questionSet.GetGenerator(template_reference);
            var question = questionGenerator.Generate();

            var json = JsonConvert.SerializeObject(question, Formatting.Indented);

            var response = Request.CreateResponse(HttpStatusCode.OK, "value");
            response.Content = new StringContent(json, System.Text.Encoding.UTF8);

            return response;
        }
    }
}
