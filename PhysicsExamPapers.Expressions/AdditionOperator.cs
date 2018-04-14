using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Expressions
{
    public class AdditionOperator : Expression
    {
        public Expression Operand1 { get; set; }
        public Expression Operand2 { get; set; }

        public override string ToString()
        {
            return Operand1.ToString() + " + " + Operand2.ToString();
        }
    }
}
