using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsExamPapers.Expressions
{
    public class Interpreter
    {
        public Dictionary<Variable, Expression> NamesTable { get; set; }

        public Interpreter()
        {
            NamesTable = new Dictionary<Variable, Expression>();
        }

        public void InterpretLines(string lines)
        {
            var linesArray = lines.Split('\n');

            foreach (var line in linesArray)
            {
                InterpretLine(line);
            }
        }

        public void InterpretLine(string line)
        {
            var lexemes = Expect.Expression(line, 0).ResultObject;
            var expression = ExpressionBuilder.BuildExpression(lexemes);

            if (expression is AssignmentStatement && (expression as AssignmentStatement).LeftHandSide is Variable)
            {
                var newName = (expression as AssignmentStatement).LeftHandSide as Variable;
                var rightHandSide = (expression as AssignmentStatement).RightHandSide;

                foreach (var name in NamesTable)
                {
                    rightHandSide = Evaluator.SubstituteVariableWithNumber(rightHandSide, name.Key, name.Value as Number<int>);
                }

                var newValue = Evaluator.EvaluateExpression(rightHandSide);

                NamesTable.Add(newName, newValue);
            }
        }

        public int GetVariableValueByName(string name)
        {
            return (NamesTable.First(i => i.Key.Name == name && i.Value is Number<int>).Value as Number<int>).Value;
        }

        public decimal GetVariableDecimalValueByName(string name)
        {
            return (NamesTable.First(i => i.Key.Name == name && i.Value is Number<decimal>).Value as Number<decimal>).Value;
        }
    }
}
