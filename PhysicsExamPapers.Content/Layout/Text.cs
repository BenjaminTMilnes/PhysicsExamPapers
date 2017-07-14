using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content.Layout
{
    public class Text : IElement
    {
        public string Content { get; set; }

        public Text(string content)
        {
            Content = content;
        }
    }
}
