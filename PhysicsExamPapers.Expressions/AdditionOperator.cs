namespace PhysicsExamPapers.Expressions
{
    public class AdditionOperator : BinomialOperator
    {
        public override string ToString()
        {
            return Operand1.ToString() + " + " + Operand2.ToString();
        }
    }
}
