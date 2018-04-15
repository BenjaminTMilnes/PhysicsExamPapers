namespace PhysicsExamPapers.Expressions
{
    public class ExponentiationOperator : BinomialOperator
    {
        public override string ToString()
        {
            return Operand1.ToString() + " ^ " + Operand2.ToString();
        }
    }
}
