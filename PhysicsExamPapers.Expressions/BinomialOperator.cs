namespace PhysicsExamPapers.Expressions
{
    public abstract class BinomialOperator : Expression
    {
        public Expression Operand1 { get; set; }
        public Expression Operand2 { get; set; }
    }
}
