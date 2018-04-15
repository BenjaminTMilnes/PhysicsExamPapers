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

            Assert.AreEqual(123, result.ResultObject.Value);
        }

        [TestMethod]
        public void TestExpectMultiplicationOperator1()
        {
            var result = Expect.BinomialOperator("*", 0);

            Assert.AreEqual(new MultiplicationOperator(), result.ResultObject);
        }
    }
}
