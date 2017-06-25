﻿using System;
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
