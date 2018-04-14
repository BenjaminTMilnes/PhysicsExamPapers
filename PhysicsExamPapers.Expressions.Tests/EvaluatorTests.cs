﻿using System;
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
            var evaluator = new Evaluator();

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
            Assert.AreEqual(number3.Value, (evaluator.EvaluateMultiplicationOperator(multiplicationOperator) as Number<int>).Value);
        }
    }
}
