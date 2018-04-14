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
                    result = MultiplicationOperator(text, position);
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

        public ExpectResult<Lexeme> MultiplicationOperator(string text, int position)
        {
            var result = new ExpectResult<Lexeme>();

            result.Success = false;

            if (text[position] == '*')
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
