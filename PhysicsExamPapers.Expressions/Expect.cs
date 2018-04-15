using System;
using System.Collections.Generic;
using System.Linq;

namespace PhysicsExamPapers.Expressions
{
    public class UnexpectedLexemeException : Exception
    {
        public int Position { get; set; }
        public string ResultText { get; set; }

        public UnexpectedLexemeException(int position, string resultText)
        {
            Position = position;
            ResultText = resultText;
        }

        public override string ToString()
        {
            return $"Unexpected lexeme: '{ResultText}' at position {Position}.";
        }
    }

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

                if (!result.IsSuccessful) { result = Identifier(text, i); }
                if (!result.IsSuccessful) { result = BinomialOperator(text, i); }
                if (!result.IsSuccessful) { result = AssignmentOperator(text, i); }
                if (!result.IsSuccessful) { result = Bracket(text, i); }

                if (!result.IsSuccessful)
                {
                    result = WhiteSpace(text, i);

                    if (result.IsSuccessful)
                    {
                        i += result.Length;
                        continue;
                    }
                }

                if (!result.IsSuccessful)
                {
                    throw new UnexpectedLexemeException(i, text[i].ToString());
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

            if (matchedText.Length > 0)
            {
                return SuccessfulResult(position, matchedText, LexemeType.WhiteSpace);
            }

            return UnsuccessfulResult();
        }

        public static ExpectResult<Lexeme> Comma(string text, int position)
        {
            var matchedText = text[position].ToString();

            if (matchedText == ",")
            {
                return SuccessfulResult(position, matchedText, LexemeType.Comma);
            }

            return UnsuccessfulResult();
        }

        public static ExpectResult<Lexeme> Bracket(string text, int position)
        {
            var matchedText = text[position].ToString();

            if (matchedText == "(")
            {
                return SuccessfulResult(position, matchedText, LexemeType.OpeningBracket);
            }

            if (matchedText == ")")
            {
                return SuccessfulResult(position, matchedText, LexemeType.ClosingBracket);
            }

            return UnsuccessfulResult();
        }

        public static ExpectResult<Lexeme> AssignmentOperator(string text, int position)
        {
            var matchedText = text[position].ToString();

            if (matchedText == "=")
            {
                return SuccessfulResult(position, matchedText, LexemeType.AssignmentOperator);
            }

            return UnsuccessfulResult();
        }

        public static ExpectResult<Lexeme> BinomialOperator(string text, int position)
        {
            var binomialOperators = "+-*/^";

            if (binomialOperators.Any(c => c == text[position]))
            {
                var matchedText = text[position].ToString();

                return SuccessfulResult(position, matchedText, LexemeType.BinomialOperator);
            }

            return UnsuccessfulResult();
        }

        public static ExpectResult<Lexeme> Identifier(string text, int position)
        {
            var identifierCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
            var matchedText = "";

            for (var i = position; i < text.Length; i++)
            {
                if (identifierCharacters.Any(c => c == text[i]))
                {
                    matchedText += text[i];
                }
                else { break; }
            }

            if (matchedText.Length > 0)
            {
                return SuccessfulResult(position, matchedText, LexemeType.Identifier);
            }

            return UnsuccessfulResult();
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

            if (matchedText.Length > 0)
            {
                return SuccessfulResult(position, matchedText, LexemeType.Number);
            }

            return UnsuccessfulResult();
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

        private static ExpectResult<Lexeme> UnsuccessfulResult()
        {
            var result = new ExpectResult<Lexeme>();

            result.IsSuccessful = false;

            return result;
        }
    }
}
