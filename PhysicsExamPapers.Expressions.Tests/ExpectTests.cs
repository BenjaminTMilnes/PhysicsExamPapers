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
    public class ExpectTests
    {
        [TestMethod]
        public void TestExpectNumber1()
        {
            var expect = new Expect();
            var result = expect.Number("123", 0);

            Assert.AreEqual(123, result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectMultiplicationOperator1()
        {
            var expect = new Expect();
            var result = expect.BinomialOperator("*", 0);

            Assert.AreEqual(new MultiplicationOperator(), result.ResultObject);
        }

        [TestMethod]
        public void TestExpectExpression1()
        {
            var expect = new Expect();
            var lexemes = expect.Expression("1 * 2 + 3 * 4 ^ 5 - 6 / 7", 0);

            var text = string.Join("", expect.BuildExpression(lexemes));

            Assert.AreEqual("12*345^*+67/-", text);

        }
    }
}
