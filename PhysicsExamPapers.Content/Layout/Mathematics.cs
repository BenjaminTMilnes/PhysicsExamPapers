using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content.Layout
{
    public class Mathematics : IElement
    {
        public string Content { get; set; }

        public Mathematics(string content)
        {
            Content = content;
        }
    }
}
