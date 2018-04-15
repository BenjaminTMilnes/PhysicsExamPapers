namespace PhysicsExamPapers.Expressions
{
    public class MultiplicationOperator : BinomialOperator
    {
        public override string ToString()
        {
            return Operand1.ToString() + " * " + Operand2.ToString();
        }
    }
}
