namespace PhysicsExamPapers.Expressions
{
    public enum LexemeType
    {
        Number = 1,
        Identifier = 2,
        BinomialOperator = 3,
        AssignmentOperator = 4,
        OpeningBracket = 5,
        ClosingBracket = 6,
        WhiteSpace = 7
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
