using System;
using System.Collections.Generic;

namespace PhysicsExamPapers.Expressions
{
    public class UnableToEvaluateException : Exception { }

    public class Evaluator
    {
        public static Expression EvaluateExpression(Expression expression)
        {
            if (expression is BinomialOperator) { return EvaluateBinomialOperator(expression as BinomialOperator); }
            if (expression is Number<int>) { return expression; }

            throw new NotImplementedException();
        }

        private static Expression EvaluateBinomialOperator(BinomialOperator binomialOperator)
        {
            binomialOperator.Operand1 = EvaluateExpression(binomialOperator.Operand1);
            binomialOperator.Operand2 = EvaluateExpression(binomialOperator.Operand2);

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

        private static Number<double> EvaluateRoundFunction(RoundFunction roundFunction)
        {
            if (!(roundFunction.Operand is Number<double>))
            {
                roundFunction.Operand = EvaluateExpression(roundFunction.Operand);
            }

            var operand = (roundFunction.Operand as Number<double>).Value;
            var numberOfDecimalPlaces = roundFunction.NumberOfDecimalPlaces.Value;

            var number = new Number<double>();

            number.Value = Math.Round(operand, numberOfDecimalPlaces);

            return number;
        }

        public static Expression SubstituteVariableWithNumber(Expression expression, Variable variable, Number<int> number)
        {
            if (expression is Variable && (expression as Variable).Name == variable.Name)
            {
                if ((expression as Variable).Name == variable.Name)
                {
                    return number;
                }
            }
            if (expression is BinomialOperator)
            {
                var binomialOperator = expression as BinomialOperator;

                binomialOperator.Operand1 = SubstituteVariableWithNumber(binomialOperator.Operand1, variable, number);
                binomialOperator.Operand2 = SubstituteVariableWithNumber(binomialOperator.Operand2, variable, number);

                return binomialOperator;
            }

            return expression;
        }
    }
}
