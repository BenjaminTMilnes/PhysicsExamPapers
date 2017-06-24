using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Content
{
    public class TextResolver
    {
        public string Resolve(string text, params object[] values)
        {
            var numberOfValues = values.Length;

            for (var a = 0; a < numberOfValues; a++)
            {
                text = text.Replace($"[[{a}]]", values[a].ToString());
            }

            return text;
        }
    }
}
