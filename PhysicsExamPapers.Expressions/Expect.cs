using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Expressions
{
    public class Expect
    {
        public ExpectResult<string> WhiteSpace(string text, int position)
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

            var result = new ExpectResult<string>();

            result.Success = false;

            if (matchedText.Length > 0)
            {
                result.Success = true;
                result.Position = position;
                result.Length = matchedText.Length;
                result.Text = matchedText;
                result.ResultObject = matchedText;
            }

            return result;
        }

        public ExpectResult<MultiplicationOperator> MultiplicationOperator(string text, int position)
        {
            var result = new ExpectResult<MultiplicationOperator>();

            result.Success = false;

            if (text[position] == '*')
            {
                result.Success = true;
                result.Position = position;
                result.Length = 1;
                result.Text = text[position].ToString();
                result.ResultObject = new MultiplicationOperator();
            }

            return result;
        }

        public ExpectResult<Number<int>> Number(string text, int position)
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

            var result = new ExpectResult<Number<int>>();

            result.Success = false;

            if (matchedText.Length > 0)
            {
                var number = new Number<int>();

                number.Value = int.Parse(matchedText);

                result.Success = true;
                result.Position = position;
                result.Length = matchedText.Length;
                result.Text = matchedText;
                result.ResultObject = number;
            }

            return result;
        }
    }
}
