using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Expressions
{
    public class ExpressionBuilder
    {

        public IEnumerable<Lexeme> BuildExpression(IEnumerable<Lexeme> lexemes)
        {
            var operands = new Stack<Lexeme>();
            var operators = new Stack<Lexeme>();
            var orderedLexemes = new Stack<Lexeme>();

            foreach (var lexeme in lexemes)
            {
                if (lexeme.Type == LexemeType.BinomialOperator)
                {

                    while (operators.Any() && PrecedenceIsGreater(operators.Peek().Value, lexeme.Value))
                    {
                        orderedLexemes.Push(operators.Pop());
                    }

                    operators.Push(lexeme);
                }

                if (lexeme.Type == LexemeType.Number)
                {
                    orderedLexemes.Push(lexeme);
                }
            }

            while (operators.Any())
            {
                orderedLexemes.Push(operators.Pop());
            }

            return orderedLexemes.Reverse().ToArray();
        }

        public Expression BuildExpression2(IEnumerable<Lexeme> lexemes)
        {
            var expressions = new Stack<Expression>();

            foreach (var lexeme in lexemes)
            {
                if (lexeme.Type == LexemeType.Number)
                {
                    var number = new Number<int>();

                    number.Value = int.Parse(lexeme.Value);

                    expressions.Push(number);
                }
                if (lexeme.Type == LexemeType.BinomialOperator && lexeme.Value == "*")
                {
                    var multiplicationOperator = new MultiplicationOperator();

                    multiplicationOperator.Operand2 = expressions.Pop();
                    multiplicationOperator.Operand1 = expressions.Pop();

                    expressions.Push(multiplicationOperator);
                }
                if (lexeme.Type == LexemeType.BinomialOperator && lexeme.Value == "+")
                {
                    var additionOperator = new AdditionOperator();

                    additionOperator.Operand2 = expressions.Pop();
                    additionOperator.Operand1 = expressions.Pop();

                    expressions.Push(additionOperator);
                }
            }

            return expressions.Pop();
        }

        private bool PrecedenceIsGreater(string operator1, string operator2)
        {
            var precedence1 = 0;
            var precedence2 = 0;

            if (operator1 == "^")
            {
                precedence1 = 3;
            }
            if (operator1 == "*" || operator1 == "/")
            {
                precedence1 = 2;
            }
            if (operator1 == "+" || operator1 == "-")
            {
                precedence1 = 1;
            }

            if (operator2 == "^")
            {
                precedence2 = 3;
            }
            if (operator2 == "*" || operator2 == "/")
            {
                precedence2 = 2;
            }
            if (operator2 == "+" || operator2 == "-")
            {
                precedence2 = 1;
            }

            return precedence1 > precedence2;
        }
    }
}
