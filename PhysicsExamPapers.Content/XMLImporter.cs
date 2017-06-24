using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PhysicsExamPapers.Content
{
    public sealed class XMLImporter
    {
        private string _basePath;

        public XMLImporter(string basePath)
        {
            _basePath = basePath;
        }

        public XMLResource Import(string resourceName)
        {
            var path = $"{_basePath}{resourceName}.xml";
            var document = new XmlDocument();

            document.Load(path);

            return new XMLResource(document);
        }
    }
}