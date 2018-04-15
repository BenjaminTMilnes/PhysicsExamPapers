namespace PhysicsExamPapers.Expressions
{
    public class SubtractionOperator : BinomialOperator
    {
        public override string ToString()
        {
            return Operand1.ToString() + " - " + Operand2.ToString();
        }
    }
}
