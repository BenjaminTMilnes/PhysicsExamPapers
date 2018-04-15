using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Expressions
{
    public class Expect
    {
        public IEnumerable<Lexeme> Expression(string text, int position)
        {
            var lexemes = new List<Lexeme>();

            while (position < text.Length)
            {
                var result = Number(text, position);

                if (result.Success == false)
                {
                    result = BinomialOperator(text, position);
                }
                if (result.Success == false)
                {
                    result = WhiteSpace(text, position);

                    if (result.Success)
                    {
                        position += result.Length;
                        continue;
                    }
                }

                lexemes.Add(result.ResultObject);

                position += result.Length;
            }

            return lexemes;
        }

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

        public ExpectResult<Lexeme> WhiteSpace(string text, int position)
        {
            var matchedText = "";

            for (var i = position; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    matchedText += text[i];
                }
                else
                {
                    break;
                }
            }

            var result = new ExpectResult<Lexeme>();

            result.Success = false;

            if (matchedText.Length > 0)
            {
                result.Success = true;
                result.Position = position;
                result.Length = matchedText.Length;
                result.Text = matchedText;
                result.ResultObject = new Lexeme(matchedText, LexemeType.WhiteSpace);
            }

            return result;
        }

        public ExpectResult<Lexeme> BinomialOperator(string text, int position)
        {
            var operators = "+-*/^";
            var result = new ExpectResult<Lexeme>();

            result.Success = false;

            if (operators.Any(c => c == text[position]))
            {
                var matchedText = text[position].ToString();

                result.Success = true;
                result.Position = position;
                result.Length = matchedText.Length;
                result.Text = matchedText;
                result.ResultObject = new Lexeme(matchedText, LexemeType.BinomialOperator);
            }

            return result;
        }

        public ExpectResult<Lexeme> Number(string text, int position)
        {
            var numerals = "0123456789";
            var matchedText = "";

            for (var i = position; i < text.Length; i++)
            {
                if (numerals.Any(c => c == text[i]))
                {
                    matchedText += text[i];
                }
                else
                {
                    break;
                }
            }

            var result = new ExpectResult<Lexeme>();

            result.Success = false;

            if (matchedText.Length > 0)
            {
                result.Success = true;
                result.Position = position;
                result.Length = matchedText.Length;
                result.Text = matchedText;
                result.ResultObject = new Lexeme(matchedText, LexemeType.Number);
            }

            return result;
        }
    }
}
