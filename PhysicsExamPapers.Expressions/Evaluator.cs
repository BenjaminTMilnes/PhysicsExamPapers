using System;

namespace PhysicsExamPapers.Expressions
{
    public class UnableToEvaluateException : Exception { }

    public class Evaluator
    {
        public Expression EvaluateMultiplicationOperator(MultiplicationOperator multiplicationOperator)
        {
            if (multiplicationOperator.Operand1 is Number<int> && multiplicationOperator.Operand2 is Number<int>)
            {
                var number = new Number<int>();

                number.Value = (multiplicationOperator.Operand1 as Number<int>).Value * (multiplicationOperator.Operand2 as Number<int>).Value;

                return number;
            }

            throw new UnableToEvaluateException();
        }
    }
}
