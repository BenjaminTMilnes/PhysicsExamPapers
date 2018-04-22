using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Expressions
{
    public class UnmatchedBracketException : Exception { }

    public class ExpressionBuilder
    {
        public static IEnumerable<Lexeme> ReorderLexemes(IEnumerable<Lexeme> lexemes)
        {
            var lexemesArray = lexemes.ToArray();

            var operands = new Stack<Lexeme>();
            var operators = new Stack<Lexeme>();

            for (var i = 0; i < lexemesArray.Length; i++)
            {
                var lexeme = lexemesArray[i];

                if (lexeme.Type == LexemeType.OpeningBracket)
                {
                    operators.Push(lexeme);
                    continue;
                }

                if (lexeme.Type == LexemeType.ClosingBracket)
                {
                    var closedBrackets = 0;

                    while (operators.Any() && closedBrackets < 1)
                    {
                        var lastOperator = operators.Pop();

                        if (lastOperator.Type == LexemeType.OpeningBracket)
                        {
                            closedBrackets++;
                        }
                        else
                        {
                            operands.Push(lastOperator);
                        }
                    }
                }

                if (lexeme.Type == LexemeType.BinomialOperator || lexeme.Type == LexemeType.AssignmentOperator)
                {
                    while (operators.Any() && operators.Peek().Type != LexemeType.OpeningBracket && PrecedenceOf(operators.Peek()) > PrecedenceOf(lexeme))
                    {
                        operands.Push(operators.Pop());
                    }

                    operators.Push(lexeme);
                }

                if (lexeme.Type == LexemeType.Number || lexeme.Type == LexemeType.Identifier)
                {
                    operands.Push(lexeme);
                }
            }

            while (operators.Any())
            {
                operands.Push(operators.Pop());
            }

            return operands.Reverse().ToArray();
        }

        private static int PrecedenceOf(Lexeme lexeme)
        {
            return PrecedenceOf(lexeme.Value);
        }

        private static int PrecedenceOf(string operator1)
        {
            switch (operator1)
            {
                case "^":
                    return 4;
                case "*":
                case "/":
                    return 3;
                case "+":
                case "-":
                    return 2;
                case "=":
                    return 1;
                default:
                    return 0;
            }
        }

        public static Expression BuildExpression(IEnumerable<Lexeme> lexemes)
        {
            lexemes = ReorderLexemes(lexemes);

            var expressions = new Stack<Expression>();

            foreach (var lexeme in lexemes)
            {
                if (lexeme.Type == LexemeType.Number)
                {
                    var number = new Number<int>();

                    number.Value = int.Parse(lexeme.Value);

                    expressions.Push(number);
                }

                if (lexeme.Type == LexemeType.Identifier)
                {
                    var variable = new Variable();

                    variable.Name = lexeme.Value;

                    expressions.Push(variable);
                }

                if (lexeme.Type == LexemeType.BinomialOperator && lexeme.Value == "*")
                {
                    var multiplicationOperator = new MultiplicationOperator();

                    multiplicationOperator.Operand2 = expressions.Pop();
                    multiplicationOperator.Operand1 = expressions.Pop();

                    expressions.Push(multiplicationOperator);
                }

                if (lexeme.Type == LexemeType.BinomialOperator && lexeme.Value == "/")
                {
                    var divisionOperator = new DivisionOperator();

                    divisionOperator.Operand2 = expressions.Pop();
                    divisionOperator.Operand1 = expressions.Pop();

                    expressions.Push(divisionOperator);
                }

                if (lexeme.Type == LexemeType.BinomialOperator && lexeme.Value == "+")
                {
                    var additionOperator = new AdditionOperator();

                    additionOperator.Operand2 = expressions.Pop();
                    additionOperator.Operand1 = expressions.Pop();

                    expressions.Push(additionOperator);
                }

                if (lexeme.Type == LexemeType.BinomialOperator && lexeme.Value == "-")
                {
                    var subtractionOperator = new SubtractionOperator();

                    subtractionOperator.Operand2 = expressions.Pop();
                    subtractionOperator.Operand1 = expressions.Pop();

                    expressions.Push(subtractionOperator);
                }

                if (lexeme.Type == LexemeType.AssignmentOperator)
                {
                    var assignmentStatement = new AssignmentStatement();

                    assignmentStatement.RightHandSide = expressions.Pop();
                    assignmentStatement.LeftHandSide = expressions.Pop();

                    expressions.Push(assignmentStatement);
                }
            }

            return expressions.Pop();
        }
    }
}
