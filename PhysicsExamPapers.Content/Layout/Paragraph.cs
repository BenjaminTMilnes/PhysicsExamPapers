using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content.Layout
{
    public class Paragraph : IElement
    {
        public IEnumerable<IElement> Subelements { get; set; }
    }
}
