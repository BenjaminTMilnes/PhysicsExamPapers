using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public class TextResolver
    {
        public string Resolve(string text, IDictionary<string, object> values)
        {
            foreach (var value in values)
            {
                text = text.Replace($"[[{value.Key}]]", value.Value.ToString());
            }

            return text;
        }
    }
}
