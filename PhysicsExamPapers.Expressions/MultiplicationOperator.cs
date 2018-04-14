namespace PhysicsExamPapers.Expressions
{
    public class MultiplicationOperator : Expression
    {
        public Expression Operand1 { get; set; }
        public Expression Operand2 { get; set; }

        public override string ToString()
        {
            return Operand1.ToString() + " * " + Operand2.ToString();
        }
    }
}
