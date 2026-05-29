using NUnit.Framework;
using System;

namespace NelderMeadApp.Tests
{
    [TestFixture]
    public class ExpressionParserTests
    {
        private const double Tol = 1e-10;

        [Test]
        public void Addition() { var f = ExpressionParser.Compile("x1 + x2", 2); Assert.That(f(new[] { 3.0, 5.0 }), Is.EqualTo(8.0).Within(Tol)); }
        [Test]
        public void Subtraction() { var f = ExpressionParser.Compile("x1 - x2", 2); Assert.That(f(new[] { 10.0, 3.0 }), Is.EqualTo(7.0).Within(Tol)); }
        [Test]
        public void Multiplication() { var f = ExpressionParser.Compile("x1 * x2", 2); Assert.That(f(new[] { 4.0, 5.0 }), Is.EqualTo(20.0).Within(Tol)); }
        [Test]
        public void Division() { var f = ExpressionParser.Compile("x1 / x2", 2); Assert.That(f(new[] { 10.0, 2.0 }), Is.EqualTo(5.0).Within(Tol)); }
        [Test]
        public void Power() { var f = ExpressionParser.Compile("x1^2 + x2^2", 2); Assert.That(f(new[] { 3.0, 4.0 }), Is.EqualTo(25.0).Within(Tol)); }
        [Test]
        public void PowerWithParentheses() { var f = ExpressionParser.Compile("(x1 - 2)^2 + (x2 + 3)^2", 2); Assert.That(f(new[] { 2.0, -3.0 }), Is.EqualTo(0.0).Within(Tol)); }
        [Test]
        public void PowerNumber() { var f = ExpressionParser.Compile("2^3", 1); Assert.That(f(new[] { 0.0 }), Is.EqualTo(8.0).Within(Tol)); }
        [Test]
        public void PowerVariableToVariable() { var f = ExpressionParser.Compile("x1^x2", 2); Assert.That(f(new[] { 2.0, 3.0 }), Is.EqualTo(8.0).Within(Tol)); }
        [Test]
        public void Sin() { var f = ExpressionParser.Compile("sin(x1)", 1); Assert.That(f(new[] { Math.PI / 2 }), Is.EqualTo(1.0).Within(Tol)); }
        [Test]
        public void Cos() { var f = ExpressionParser.Compile("cos(x1)", 1); Assert.That(f(new[] { 0.0 }), Is.EqualTo(1.0).Within(Tol)); }
        [Test]
        public void Tan() { var f = ExpressionParser.Compile("tan(x1)", 1); Assert.That(f(new[] { 0.0 }), Is.EqualTo(0.0).Within(Tol)); }
        [Test]
        public void Exp() { var f = ExpressionParser.Compile("exp(x1)", 1); Assert.That(f(new[] { 0.0 }), Is.EqualTo(1.0).Within(Tol)); }
        [Test]
        public void Sqrt() { var f = ExpressionParser.Compile("sqrt(x1)", 1); Assert.That(f(new[] { 16.0 }), Is.EqualTo(4.0).Within(Tol)); }
        [Test]
        public void Abs() { var f = ExpressionParser.Compile("abs(x1)", 1); Assert.That(f(new[] { -5.0 }), Is.EqualTo(5.0).Within(Tol)); }
        [Test]
        public void Log() { var f = ExpressionParser.Compile("log(x1)", 1); Assert.That(f(new[] { Math.E }), Is.EqualTo(1.0).Within(Tol)); }
        [Test]
        public void SingleX() { var f = ExpressionParser.Compile("x^2", 1); Assert.That(f(new[] { 5.0 }), Is.EqualTo(25.0).Within(Tol)); }
        [Test]
        public void XAndY() { var f = ExpressionParser.Compile("x^2 + y^2", 2); Assert.That(f(new[] { 3.0, 4.0 }), Is.EqualTo(25.0).Within(Tol)); }
        [Test]
        public void ThreeVariables() { var f = ExpressionParser.Compile("x + y + z", 3); Assert.That(f(new[] { 1.0, 2.0, 3.0 }), Is.EqualTo(6.0).Within(Tol)); }
        [Test]
        public void RosenbrockExpression() { var f = ExpressionParser.Compile("100*(x2 - x1^2)^2 + (1 - x1)^2", 2); Assert.That(f(new[] { 1.0, 1.0 }), Is.EqualTo(0.0).Within(Tol)); }
        [Test]
        public void ComplexExpression()
        {
            var f = ExpressionParser.Compile("sin(x1)*sin(x1) + cos(x2)*cos(x2)", 2);
            Assert.That(f(new[] { 0.0, 0.0 }), Is.EqualTo(1.0).Within(Tol));
        }
        [Test]
        public void NestedParentheses() { var f = ExpressionParser.Compile("((x1 + 1)) * 2", 1); Assert.That(f(new[] { 3.0 }), Is.EqualTo(8.0).Within(Tol)); }
        [Test]
        public void NegativeNumbers() { var f = ExpressionParser.Compile("x1 + (-5)", 1); Assert.That(f(new[] { 10.0 }), Is.EqualTo(5.0).Within(Tol)); }
        [Test]
        public void InvalidExpression_Throws()
        {
            Assert.Throws<Exception>(() => ExpressionParser.Compile("x1 + ", 1));
        }
    }
}