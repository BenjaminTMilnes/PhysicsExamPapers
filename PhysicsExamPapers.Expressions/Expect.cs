using System.Collections.Generic;
using System.Linq;

namespace PhysicsExamPapers.Expressions
{
    public class Expect
    {
        public static ExpectResult<IEnumerable<Lexeme>> Expression(string text, int position)
        {
            var expressionResult = new ExpectResult<IEnumerable<Lexeme>>();

            expressionResult.IsSuccessful = false;

            var lexemes = new List<Lexeme>();
            var i = position;

            while (i < text.Length)
            {
                var result = Number(text, i);

                if (!result.IsSuccessful)
                {
                    result = BinomialOperator(text, i);
                }

                if (!result.IsSuccessful)
                {
                    result = Bracket(text, i);
                }

                if (!result.IsSuccessful)
                {
                    result = WhiteSpace(text, i);

                    if (result.IsSuccessful)
                    {
                        i += result.Length;
                        continue;
                    }
                }

                lexemes.Add(result.ResultObject);

                i += result.Length;
            }

            if (lexemes.Any())
            {
                expressionResult.IsSuccessful = true;
                expressionResult.Position = position;
                expressionResult.ResultObject = lexemes;
            }

            return expressionResult;
        }

        public static ExpectResult<Lexeme> WhiteSpace(string text, int position)
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

            result.IsSuccessful = false;

            if (matchedText.Length > 0)
            {
                result = SuccessfulResult(position, matchedText, LexemeType.WhiteSpace);
            }

            return result;
        }

        public static ExpectResult<Lexeme> Bracket(string text, int position)
        {
            var result = new ExpectResult<Lexeme>();

            result.IsSuccessful = false;

            var matchedText = text[position].ToString();

            if (matchedText == "(")
            {
                result = SuccessfulResult(position, matchedText, LexemeType.OpeningBracket);
            }

            if (matchedText == ")")
            {
                result = SuccessfulResult(position, matchedText, LexemeType.ClosingBracket);
            }

            return result;
        }

        public static ExpectResult<Lexeme> BinomialOperator(string text, int position)
        {
            var binomialOperators = "+-*/^";
            var result = new ExpectResult<Lexeme>();

            result.IsSuccessful = false;

            if (binomialOperators.Any(c => c == text[position]))
            {
                var matchedText = text[position].ToString();

                result = SuccessfulResult(position, matchedText, LexemeType.BinomialOperator);
            }

            return result;
        }

        public static ExpectResult<Lexeme> Number(string text, int position)
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

            result.IsSuccessful = false;

            if (matchedText.Length > 0)
            {
                result = SuccessfulResult(position, matchedText, LexemeType.Number);
            }

            return result;
        }

        private static ExpectResult<Lexeme> SuccessfulResult(int position, string matchedText, LexemeType lexemeType)
        {
            var result = new ExpectResult<Lexeme>();

            result.IsSuccessful = true;
            result.Position = position;
            result.Length = matchedText.Length;
            result.ResultText = matchedText;
            result.ResultObject = new Lexeme(matchedText, lexemeType);

            return result;
        }
    }
}
