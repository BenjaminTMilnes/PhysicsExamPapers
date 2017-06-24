using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace PhysicsExamPapers.Content.Tests
{
    [TestClass]
    public class TextResolverTests
    {
        private TextResolver _textResolver;

        public TextResolverTests()
        {
            _textResolver = new TextResolver();
        }

        [TestMethod]
        public void TestThatOneValueIsResolved()
        {
            Assert.AreEqual("abc", _textResolver.Resolve("[[0]]", "abc"));
        }

        [TestMethod]
        public void TestThatTwoValuesAreResolved()
        {
            Assert.AreEqual("abcdef", _textResolver.Resolve("[[0]][[1]]", "abc", "def"));
        }

        [TestMethod]
        public void TestThatOneValueAmongTextIsResolved()
        {
            Assert.AreEqual("abc def ghi", _textResolver.Resolve("abc [[0]] ghi", "def"));
        }

        [TestMethod]
        public void TestThatTwoValuesAmongTextAreResolved()
        {
            Assert.AreEqual("abc def ghi jkl mno", _textResolver.Resolve("abc [[0]] ghi [[1]] mno", "def", "jkl"));
        }
    }
}
