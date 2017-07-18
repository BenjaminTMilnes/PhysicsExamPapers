using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PhysicsExamPapers.Content
{
    public class XMLResource
    {
        protected XmlDocument _document;

        public XMLResource(XmlDocument document)
        {
            _document = document;
        }

        public string GetQuestionContent()
        {
            return _document.SelectSingleNode("/question/content").InnerXml;
        }

        public int GetNumberOfCorrectAnswers()
        {
            return _document.SelectNodes("/question/answers/correct-answers/answer").Count;
        }

        public string GetCorrectAnswerType(int index)
        {
            return _document.SelectSingleNode($"/question/answers/correct-answers/answer[{index}]").Attributes["type"].Value;
        }

        public string GetCorrectAnswerContent(int index)
        {
            return _document.SelectSingleNode($"/question/answers/correct-answers/answer[{index}]").InnerXml;
        }

        public int GetNumberOfIncorrectAnswers()
        {
            return _document.SelectNodes("/question/answers/incorrect-answers/answer").Count;
        }

        public string GetIncorrectAnswerType(int index)
        {
            return _document.SelectSingleNode($"/question/answers/incorrect-answers/answer[{index}]").Attributes["type"].Value;
        }

        public string GetIncorrectAnswerContent(int index)
        {
            return _document.SelectSingleNode($"/question/answers/incorrect-answers/answer[{index}]").InnerXml;
        }

        public int GetNumberOfHints()
        {
            return _document.SelectNodes("/question/hints/hint").Count;
        }

        public string GetHintContent(int index)
        {
            return _document.SelectSingleNode($"/question/hints/hint[{ index}]/content").InnerXml;
        }
    }
}
