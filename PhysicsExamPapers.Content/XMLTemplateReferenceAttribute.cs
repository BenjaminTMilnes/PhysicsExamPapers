using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    [AttributeUsage(AttributeTargets.Class)]
    public class XMLTemplateReferenceAttribute : Attribute
    {
        public string Reference { get; private set; }

        public XMLTemplateReferenceAttribute(string reference)
        {
            Reference = reference;
        }
    }
}
