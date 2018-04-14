using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Expressions
{
    public enum LexemeType
    {
        Number = 1,
        BinomialOperator = 2,
        WhiteSpace = 3
    }

    public class Lexeme
    {
        public string Value { get; set; }
        public LexemeType Type { get; set; }

        public Lexeme(string value, LexemeType type)
        {
            Value = value;
            Type = type;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
