namespace PhysicsExamPapers.Expressions
{
    public class DivisionOperator : BinomialOperator
    {
        public override string ToString()
        {
            return Operand1.ToString() + " / " + Operand2.ToString();
        }
    }
}
