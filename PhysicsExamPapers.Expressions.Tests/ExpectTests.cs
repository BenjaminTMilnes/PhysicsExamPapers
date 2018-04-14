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
    }
}
