using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PhysicsExamPapers.Content.Layout
{
    public class XMLImporter
    {
        public IElement ImportElement(XmlNode xmlNode)
        {
            if (xmlNode.NodeType == XmlNodeType.Text)
            {
                return ImportText(xmlNode as XmlText);
            }
            if (xmlNode.NodeType == XmlNodeType.Element && (xmlNode.Name == "paragraph" || xmlNode.Name == "p"))
            {
                return ImportParagraph(xmlNode as XmlElement);
            }
            if (xmlNode.NodeType == XmlNodeType.Element && (xmlNode.Name == "mathematics" || xmlNode.Name == "maths" || xmlNode.Name == "m"))
            {
                return ImportMathematics(xmlNode as XmlElement);
            }

            throw new UnknownLayoutElementException(xmlNode);
        }

        public IEnumerable<IElement> ImportElements(XmlNodeList xmlNodeList)
        {
            foreach (var xmlNode in xmlNodeList)
            {
                yield return ImportElement(xmlNode as XmlNode);
            }
        }

        protected IElement ImportParagraph(XmlElement xmlElement)
        {
            var subelements = ImportElements(xmlElement.ChildNodes);
            var paragraph = new Paragraph();
            paragraph.Subelements = subelements;

            return paragraph;
        }

        protected IElement ImportMathematics(XmlElement xmlElement)
        {
            return new Mathematics(xmlElement.InnerText);
        }

        protected IElement ImportText(XmlText xmlText)
        {
            return new Text(xmlText.InnerText);
        }
    }
}
