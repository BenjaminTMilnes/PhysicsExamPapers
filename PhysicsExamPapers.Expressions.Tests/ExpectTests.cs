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
            var result = expect.MultiplicationOperator("*", 0);

            Assert.AreEqual(new MultiplicationOperator(), result.ResultObject);
        }

        [TestMethod]
        public void TestExpectExpression1()
        {
            var expect = new Expect();
            var lexemes = expect.Expression("3 * 5", 0);

            var text = string.Join("", lexemes);

            Assert.AreEqual("3*5", text);

        }
    }
}
