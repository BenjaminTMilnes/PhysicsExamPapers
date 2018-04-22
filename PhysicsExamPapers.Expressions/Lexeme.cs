namespace PhysicsExamPapers.Expressions
{
    public enum LexemeType
    {
        Number = 1,
        Identifier = 2,
        FunctionName = 3,
        BinomialOperator = 4,
        AssignmentOperator = 5,
        OpeningBracket = 6,
        ClosingBracket = 7,
        Comma = 8,
        WhiteSpace = 9
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
