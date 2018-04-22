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
    public class InterpreterTests
    {
        [TestMethod]
        public void TestAssignmentStatement1()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("y = 2");

            Assert.AreEqual(2, interpreter.GetVariableValueByName("y"));
        }

        [TestMethod]
        public void TestAssignmentStatement2()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("y = 2 + 3");

            Assert.AreEqual(5, interpreter.GetVariableValueByName("y"));
        }

        [TestMethod]
        public void TestAssignmentStatement3()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("y = 2 * 3");

            Assert.AreEqual(6, interpreter.GetVariableValueByName("y"));
        }

        [TestMethod]
        public void TestAssignmentStatement4()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("y = 2 + 3 * 4");

            Assert.AreEqual(14, interpreter.GetVariableValueByName("y"));
        }

        [TestMethod]
        public void TestAssignmentStatement5()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("y = (2 + 3) * (4 - 1)");

            Assert.AreEqual(15, interpreter.GetVariableValueByName("y"));
        }

        [TestMethod]
        public void TestAssignmentStatement6()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("y = 1 - (2 + 3 * 4 - 8) * (3 - 1)");

            Assert.AreEqual(-11, interpreter.GetVariableValueByName("y"));
        }

        [TestMethod]
        public void TestSubstitution1()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("x = 2");
            interpreter.InterpretLine("y = x");

            Assert.AreEqual(2, interpreter.GetVariableValueByName("y"));
        }

        [TestMethod]
        public void TestSubstitution2()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("x = 2");
            interpreter.InterpretLine("y = 3");
            interpreter.InterpretLine("z = x + y");

            Assert.AreEqual(5, interpreter.GetVariableValueByName("z"));
        }

        [TestMethod]
        public void TestSubstitution3()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("x = 2");
            interpreter.InterpretLine("y = x + 3");
            interpreter.InterpretLine("z = 2 * x + y");

            Assert.AreEqual(9, interpreter.GetVariableValueByName("z"));
        }

        [TestMethod]
        public void TestSubstitution4()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("x = 2");
            interpreter.InterpretLine("y = x ^ 2 + 2 * x + 3");
            interpreter.InterpretLine("z = x + y");

            Assert.AreEqual(13, interpreter.GetVariableValueByName("z"));
        }

        [TestMethod]
        public void TestNamedFunctions1()
        {
            var interpreter = new Interpreter();

            interpreter.InterpretLine("x = round(2.345, 1)");

            Assert.AreEqual((decimal)2.3, interpreter.GetVariableDecimalValueByName("x"));
        }
    }
}
