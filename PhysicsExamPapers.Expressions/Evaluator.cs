using System;

namespace PhysicsExamPapers.Expressions
{
    public class UnableToEvaluateException : Exception { }

    public class Evaluator
    {
        public Expression EvaluateExpression(Expression expression)
        {
            if (expression is MultiplicationOperator) { return EvaluateMultiplicationOperator(expression as MultiplicationOperator); }
            if (expression is AdditionOperator) { return EvaluateAdditionOperator(expression as AdditionOperator); }

            throw new NotImplementedException();
        }

        public Expression EvaluateMultiplicationOperator(MultiplicationOperator multiplicationOperator)
        {
            if (!(multiplicationOperator.Operand1 is Number<int>))
            {
                multiplicationOperator.Operand1 = EvaluateExpression(multiplicationOperator.Operand1);
            }
            if (!(multiplicationOperator.Operand2 is Number<int>))
            {
                multiplicationOperator.Operand2 = EvaluateExpression(multiplicationOperator.Operand2);
            }

            if (multiplicationOperator.Operand1 is Number<int> && multiplicationOperator.Operand2 is Number<int>)
            {
                var number = new Number<int>();

                number.Value = (multiplicationOperator.Operand1 as Number<int>).Value * (multiplicationOperator.Operand2 as Number<int>).Value;

                return number;
            }

            throw new UnableToEvaluateException();
        }

        public Expression EvaluateAdditionOperator(AdditionOperator additionOperator)
        {
            if (!(additionOperator.Operand1 is Number<int>))
            {
                additionOperator.Operand1 = EvaluateExpression(additionOperator.Operand1);
            }
            if (!(additionOperator.Operand2 is Number<int>))
            {
                additionOperator.Operand2 = EvaluateExpression(additionOperator.Operand2);
            }

            if (additionOperator.Operand1 is Number<int> && additionOperator.Operand2 is Number<int>)
            {
                var number = new Number<int>();

                number.Value = (additionOperator.Operand1 as Number<int>).Value + (additionOperator.Operand2 as Number<int>).Value;

                return number;
            }

            throw new UnableToEvaluateException();
        }
    }
}
