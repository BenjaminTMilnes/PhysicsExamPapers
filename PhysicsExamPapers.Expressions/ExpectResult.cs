using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Expressions
{
    public class ExpectResult<T>
    {
        public bool Success { get; set; }
        public int Position { get; set; }
        public int Length { get; set; }
        public string Text { get; set; }
        public T ResultObject { get; set; }
    }
}
