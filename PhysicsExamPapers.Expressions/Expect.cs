using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Expressions
{
    public class Expect
    {

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
            else
            {
                result.Success = false;
            }

            return result;
        }
    }
}
