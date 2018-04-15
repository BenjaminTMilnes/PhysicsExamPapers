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
            var result = Expect.Number("123", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("123", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectNumber2()
        {
            var result = Expect.Number("abc", 0);

            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void TestExpectNumber3()
        {
            var result = Expect.Number("abc 123", 4);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("123", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectNumber4()
        {
            var result = Expect.Number("123 456", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("123", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectIdentifier1()
        {
            var result = Expect.Identifier("a", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("a", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectIdentifier2()
        {
            var result = Expect.Identifier("abc", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("abc", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectIdentifier3()
        {
            var result = Expect.Identifier("long_name", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("long_name", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectIdentifier4()
        {
            var result = Expect.Identifier("A", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("A", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectIdentifier5()
        {
            var result = Expect.Identifier("LongName", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("LongName", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectIdentifier6()
        {
            var result = Expect.Identifier("123", 0);

            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void TestExpectIdentifier7()
        {
            var result = Expect.Identifier("a b", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("a", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectBinomialOperator1()
        {
            var result = Expect.BinomialOperator("+", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("+", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectBinomialOperator2()
        {
            var result = Expect.BinomialOperator("-", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("-", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectBinomialOperator3()
        {
            var result = Expect.BinomialOperator("*", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("*", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectBinomialOperator4()
        {
            var result = Expect.BinomialOperator("/", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("/", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectBinomialOperator5()
        {
            var result = Expect.BinomialOperator("++", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("+", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectBinomialOperator6()
        {
            var result = Expect.BinomialOperator("123", 0);

            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void TestExpectBinomialOperator7()
        {
            var result = Expect.BinomialOperator("a", 0);

            Assert.AreEqual(false, result.IsSuccessful);
        }

        [TestMethod]
        public void TestExpectBinomialOperator8()
        {
            var result = Expect.BinomialOperator("-a", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual("-", result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectExpression1()
        {
            var result = Expect.Expression("1 + 2", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual(3, result.ResultObject.Count());
            Assert.AreEqual("1", result.ResultObject.ToArray()[0].Value);
            Assert.AreEqual("+", result.ResultObject.ToArray()[1].Value);
            Assert.AreEqual("2", result.ResultObject.ToArray()[2].Value);
        }

        [TestMethod]
        public void TestExpectExpression2()
        {
            var result = Expect.Expression("1 + 2 - 3 * 4 / 6 + a", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual(11, result.ResultObject.Count());
            Assert.AreEqual("1", result.ResultObject.ToArray()[0].Value);
            Assert.AreEqual("+", result.ResultObject.ToArray()[1].Value);
            Assert.AreEqual("2", result.ResultObject.ToArray()[2].Value);
            Assert.AreEqual("-", result.ResultObject.ToArray()[3].Value);
            Assert.AreEqual("3", result.ResultObject.ToArray()[4].Value);
            Assert.AreEqual("*", result.ResultObject.ToArray()[5].Value);
            Assert.AreEqual("4", result.ResultObject.ToArray()[6].Value);
            Assert.AreEqual("/", result.ResultObject.ToArray()[7].Value);
            Assert.AreEqual("6", result.ResultObject.ToArray()[8].Value);
            Assert.AreEqual("+", result.ResultObject.ToArray()[9].Value);
            Assert.AreEqual("a", result.ResultObject.ToArray()[10].Value);
        }

        [TestMethod]
        public void TestExpectExpression3()
        {
            var result = Expect.Expression("a + b", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual(3, result.ResultObject.Count());
            Assert.AreEqual("a", result.ResultObject.ToArray()[0].Value);
            Assert.AreEqual("+", result.ResultObject.ToArray()[1].Value);
            Assert.AreEqual("b", result.ResultObject.ToArray()[2].Value);
        }

        [TestMethod]
        public void TestExpectExpression4()
        {
            var result = Expect.Expression("alpha + beta", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual(3, result.ResultObject.Count());
            Assert.AreEqual("alpha", result.ResultObject.ToArray()[0].Value);
            Assert.AreEqual("+", result.ResultObject.ToArray()[1].Value);
            Assert.AreEqual("beta", result.ResultObject.ToArray()[2].Value);
        }

        [TestMethod]
        public void TestExpectExpression5()
        {
            var result = Expect.Expression("function(a, b, c)", 0);

            Assert.AreEqual(true, result.IsSuccessful);
            Assert.AreEqual(8, result.ResultObject.Count());
            Assert.AreEqual("function", result.ResultObject.ToArray()[0].Value);
            Assert.AreEqual("(", result.ResultObject.ToArray()[1].Value);
            Assert.AreEqual("a", result.ResultObject.ToArray()[2].Value);
            Assert.AreEqual(",", result.ResultObject.ToArray()[3].Value);
            Assert.AreEqual("b", result.ResultObject.ToArray()[4].Value);
            Assert.AreEqual(",", result.ResultObject.ToArray()[5].Value);
            Assert.AreEqual("c", result.ResultObject.ToArray()[6].Value);
            Assert.AreEqual(")", result.ResultObject.ToArray()[7].Value);
        }
    }
}
