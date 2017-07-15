using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PhysicsExamPapers.Content.Layout
{
    public class LayoutConverter
    {
        protected XMLImporter _xmlImporter;
        protected HTMLExporter _htmlExporter;

        public LayoutConverter()
        {
            _xmlImporter = new XMLImporter();
            _htmlExporter = new HTMLExporter();
        }

        public string ConvertLayout(string layoutXML)
        {
            var xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(layoutXML);

            var layout = _xmlImporter.ImportElement(xmlDocument.FirstChild);
            var html = _htmlExporter.ExportElement(layout);

            return html;
        }
    }
}
