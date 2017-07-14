using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content.Layout
{
    public class HTMLExporter
    {
        public string ExportElement(IElement element)
        {
            if (element is Paragraph)
            {
                return ExportParagraph(element as Paragraph);
            }
            if (element is Mathematics)
            {
                return ExportMathematics(element as Mathematics);
            }
            if (element is Text)
            {
                return ExportText(element as Text);
            }

            throw new NotImplementedException();
        }

        public string ExportElements(IEnumerable<IElement> elements)
        {
            return string.Join("", elements.Select((e) => ExportElement(e)));
        }

        protected string ExportParagraph(Paragraph paragraph)
        {
            var subelements = ExportElements(paragraph.Subelements);

            return $"<p>{subelements}</p>";
        }

        protected string ExportMathematics(Mathematics mathematics)
        {
            return $"<latex>{mathematics.Content}</latex>";
        }

        protected string ExportText(Text text)
        {
            return text.Content;
        }
    }
}
