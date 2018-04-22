using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhysicsExamPapers.Expressions;

namespace PhysicsExamPapers.Expressions.Tests
{
    [TestClass]
    public class EvaluatorTests
    {
        [TestMethod]
        public void TestEvaluateMultiplicationOperator1()
        {
            var number1 = new Number<int>();
            var number2 = new Number<int>();
            var number3 = new Number<int>();

            var multiplicationOperator = new MultiplicationOperator();

            number1.Value = 3;
            number2.Value = 5;
            number3.Value = 15;

            multiplicationOperator.Operand1 = number1;
            multiplicationOperator.Operand2 = number2;

            Assert.AreEqual("3 * 5", multiplicationOperator.ToString());
            Assert.AreEqual(number3.Value, (Evaluator.EvaluateExpression(multiplicationOperator) as Number<int>).Value);
        }

        [TestMethod]
        public void TestBrackets1()
        {
            var lexemes = Expect.Expression("3 * (1 + 4)", 0).ResultObject;
            var expression = ExpressionBuilder.BuildExpression(lexemes);
            var answer = (Evaluator.EvaluateExpression(expression) as Number<int>).Value;

            Assert.AreEqual(15, answer);
        }

        [TestMethod]
        public void TestBrackets2()
        {
            var lexemes = Expect.Expression("3 * (1 + (4 * 2))", 0).ResultObject;
            var expression = ExpressionBuilder.BuildExpression(lexemes);
            var answer = (Evaluator.EvaluateExpression(expression) as Number<int>).Value;

            Assert.AreEqual(27, answer);
        }

        [TestMethod]
        public void TestBrackets3()
        {
            var lexemes = Expect.Expression("(1 + 2) * (2 + 3) * (2 + 4)", 0).ResultObject;
            var expression = ExpressionBuilder.BuildExpression(lexemes);
            var answer = (Evaluator.EvaluateExpression(expression) as Number<int>).Value;

            Assert.AreEqual(90, answer);
        }
    }
}
