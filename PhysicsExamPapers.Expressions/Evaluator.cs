using System;

namespace PhysicsExamPapers.Expressions
{
    public class UnableToEvaluateException : Exception { }

    public class Evaluator
    {
        public static Expression EvaluateExpression(Expression expression)
        {
            if (expression is BinomialOperator) { return EvaluateBinomialOperator(expression as BinomialOperator); }

            throw new NotImplementedException();
        }

        private static Expression EvaluateBinomialOperator(BinomialOperator binomialOperator)
        {
            if (!(binomialOperator.Operand1 is Number<int>))
            {
                binomialOperator.Operand1 = EvaluateExpression(binomialOperator.Operand1);
            }

            if (!(binomialOperator.Operand2 is Number<int>))
            {
                binomialOperator.Operand2 = EvaluateExpression(binomialOperator.Operand2);
            }

            if (binomialOperator.Operand1 is Number<int> && binomialOperator.Operand2 is Number<int>)
            {
                var value1 = (binomialOperator.Operand1 as Number<int>).Value;
                var value2 = (binomialOperator.Operand2 as Number<int>).Value;
                var number = new Number<int>();

                if (binomialOperator is ExponentiationOperator)
                {
                    number.Value = value1 ^ value2;
                }
                if (binomialOperator is MultiplicationOperator)
                {
                    number.Value = value1 * value2;
                }
                if (binomialOperator is AdditionOperator)
                {
                    number.Value = value1 + value2;
                }
                if (binomialOperator is SubtractionOperator)
                {
                    number.Value = value1 - value2;
                }

                return number;
            }

            throw new UnableToEvaluateException();
        }
    }
}
