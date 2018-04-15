namespace PhysicsExamPapers.Expressions
{
    public class RoundFunction : Expression
    {
        public Expression Operand { get; set; }
        public Number<int> NumberOfDecimalPlaces { get; set; }

        public override string ToString()
        {
            return "round(" + Operand.ToString() + ", " + NumberOfDecimalPlaces.ToString() + ")";
        }
    }
}
