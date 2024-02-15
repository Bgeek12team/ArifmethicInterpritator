using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Tests
{
    [TestClass()]
    public class ExpressionTests
    {
        [TestMethod]
        public void CalculateAt_ExpressionWithComplexConstants_ReturnsCorrectResult()
        {
            var calculator = new Expression("e * pi");

            var result = calculator.CalculateAt(new Dictionary<char, double>(), out _);

            Assert.AreEqual(Math.E * Math.PI, result, 1e-10);
        }
        [TestMethod]
        public void CalculateAt_LongExpression_ReturnsCorrectResult()
        {
            var calculator = new Expression("sin(2*1^2 + 3*1 - 5)      / (1 + 1)");

            var result = calculator.CalculateAt(new Dictionary<char, double>(), out _);

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void CalculateAt_LongExpression2_ReturnsCorrectResult()
        {
            var calculator = new Expression("(3 * ln(0 + 2) - sqrt(0)) / (cos(2*0) + 1)");

            var result = calculator.CalculateAt(new Dictionary<char, double>(), out _);

            Assert.AreEqual(0, result);
        }
        [TestMethod]
        public void CalculateAt_LongExpression3_ReturnsCorrectResult()
        {
            var calculator = new Expression("(e^5 + ln(5^2 + 1)) / sqrt(5)");

            var result = calculator.CalculateAt(new Dictionary<char, double>(), out _);

            Assert.AreEqual(67.82944756902512, result, 1e-10);
        }

        [TestMethod]
        public void CalculateAt_ExpressionWithUnaryMinus_ReturnsCorrectResult()
        {
            var calculator = new Expression("-(2 + 3)");

            var result = calculator.CalculateAt(new Dictionary<char, double>(), out _);

            Assert.AreEqual(-5, result);
        }

        [TestMethod]
        public void CalculateAt_ExpressionWithALotOfSpaces_ReturnsCorrectResult()
        {
            var calculator = new Expression("2   +   3");

            var result = calculator.CalculateAt(new Dictionary<char, double>(), out _);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void CalculateAt_ExpressionWithZeroExponent_ReturnsCorrectResult()
        {
            var calculator = new Expression("2^0");

            var result = calculator.CalculateAt(new Dictionary<char, double>(), out _);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void CalculateAt_ExpressionWithNegativeExponent_ReturnsCorrectResult()
        {
            var calculator = new Expression("2^-2");

            var result = calculator.CalculateAt(new Dictionary<char, double>(), out _);

            Assert.AreEqual(0.25, result);
        }

        [TestMethod]
        public void CalculateAt_ExpressionWithNumber_ReturnsCorrectResult()
        {
            var calculator = new Expression("1000000 + 500000");

            var result = calculator.CalculateAt(new Dictionary<char, double>(), out _);

            Assert.AreEqual(1500000, result);
        }

        [TestMethod]
        public void CalculateAt_ExpressionWithTrigonometricFunc_ReturnsCorrectResult()
        {
            var calculator = new Expression("ctg(pi/4)");

            var result = calculator.CalculateAt(new Dictionary<char, double>(), out _);

            Assert.AreEqual(1, result, 1e-10);
        }
    }
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void Tokenize_SingleNumber_ReturnsSingleNumberToken()
        {
            var expression = "123";

            var tokens = Token.Tokenize(expression);

            CollectionAssert.AreEqual(new string[] { "123 : INT_NUM : 11" }, Array.ConvertAll(tokens, t => t.ToString()));
        }
        [TestMethod]
        public void TokenizeAndInversePolish_ExpressionWithFunctionCall_ReturnsCorrectNotation()
        {
            var expression = "sin(x) + cos(y)";

            var tokens = Token.Tokenize(expression);
            var inverse = Polish.ToInversePolishView(tokens);

            var expectedOutput = new string[] { "x : VARIABLE : 11", "sin : FUNCTION : 3", "y : VARIABLE : 11", "cos : FUNCTION : 3", "+ : BINARY_OPERATOR : 0" };
            CollectionAssert.AreEqual(expectedOutput, Array.ConvertAll(inverse, t => t.ToString()));
        }
        [TestMethod]
        public void TokenizeAndInversePolish_ExpressionWithExponentiation_ReturnsCorrectNotation()
        {
            var expression = "a ^ (b + c)";

            var tokens = Token.Tokenize(expression);
            var inverse = Polish.ToInversePolishView(tokens);

            var expectedOutput = new string[] { "a : VARIABLE : 11", "b : VARIABLE : 11", "c : VARIABLE : 11", "+ : BINARY_OPERATOR : 0", "^ : BINARY_OPERATOR : 2" };
            CollectionAssert.AreEqual(expectedOutput, Array.ConvertAll(inverse, t => t.ToString()));
        }
        [TestMethod]
        public void TokenizeAndInversePolish_ExpressionWithConstants_ReturnsCorrectNotation()
        {
            var expression = "pi + e";

            var tokens = Token.Tokenize(expression);
            var inverse = Polish.ToInversePolishView(tokens);

            var expectedOutput = new string[] { "pi : CONSTANT : 11", "e : CONSTANT : 11", "+ : BINARY_OPERATOR : 0" };
            CollectionAssert.AreEqual(expectedOutput, Array.ConvertAll(inverse, t => t.ToString()));
        }

        [TestMethod]
        public void TokenizeAndInversePolish_ExpressionWithSpecialCharacters_ReturnsCorrectNotation()
        {
            var expression = "! + x * &";

            var tokens = Token.Tokenize(expression);
            var inverse = Polish.ToInversePolishView(tokens);

            var expectedOutput = new string[] { "! : VARIABLE : 11", "x : VARIABLE : 11", "& : VARIABLE : 11", "* : BINARY_OPERATOR : 1", "+ : BINARY_OPERATOR : 0" };
            CollectionAssert.AreEqual(expectedOutput, Array.ConvertAll(inverse, t => t.ToString()));
        }

        [TestMethod]
        public void TokenizeAndInversePolish_ExpressionWithMixedTypes_ReturnsCorrectNotation()
        {
            var expression = "x + 42,5 * (y - z)";

            var tokens = Token.Tokenize(expression);
            var inverse = Polish.ToInversePolishView(tokens);

            var expectedOutput = new string[] { "x : VARIABLE : 11", "42,5  : FLOAT_NUM : 11", "y : VARIABLE : 11", "z : VARIABLE : 11", "- : BINARY_OPERATOR : 0", "* : BINARY_OPERATOR : 1", "+ : BINARY_OPERATOR : 0" };
            CollectionAssert.AreEqual(expectedOutput, Array.ConvertAll(inverse, t => t.ToString()));
        }


        [TestMethod]
        public void TokenizeAndInversePolish_ValidExpression_CorrectInversePolishNotation()
        {
            var expression = "x + 12 + y";

            var tokens = Token.Tokenize(expression);
            var inverse = Polish.ToInversePolishView(tokens);

            var expectedOutput = new string[]
            {
                "x : VARIABLE : 11",
                "12  : INT_NUM : 11",
                "+ : BINARY_OPERATOR : 0",
                "y : VARIABLE : 11",
                "+ : BINARY_OPERATOR : 0"
            };

            CollectionAssert.AreEqual(expectedOutput, Array.ConvertAll(inverse, t => t.ToString()));
        }
        [TestMethod]
        public void TokenizeAndInversePolish_ExpressionWithUnaryMinus_ReturnsCorrectNotation()
        {
            var expression = "-x + y";

            var tokens = Token.Tokenize(expression);
            var inverse = Polish.ToInversePolishView(tokens);

            var expectedOutput = new string[] { "x : VARIABLE : 11", "- : FUNCTION : 0", "y : VARIABLE : 11", "+ : BINARY_OPERATOR : 0" };
            CollectionAssert.AreEqual(expectedOutput, Array.ConvertAll(inverse, t => t.ToString()));
        }
    }
}
