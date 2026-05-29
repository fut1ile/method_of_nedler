using NUnit.Framework;
using System;

namespace NelderMeadApp.Tests
{
    [TestFixture]
    public class NelderMeadSimplexTests
    {
        private const double Tol = 1e-4;
        private const double LooseTol = 1e-3;

        [Test]
        public void Sphere_FindsMinimumAtOrigin()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { 5.0, 5.0 },
                tolerance: 1e-8);

            Assert.That(result.BestPoint[0], Is.EqualTo(0.0).Within(Tol));
            Assert.That(result.BestPoint[1], Is.EqualTo(0.0).Within(Tol));
            Assert.That(result.BestValue, Is.EqualTo(0.0).Within(Tol));
        }

        [Test]
        public void Sphere_NegativeStart_FindsOrigin()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { -5.0, -5.0 },
                tolerance: 1e-8);

            Assert.That(result.BestPoint[0], Is.EqualTo(0.0).Within(Tol));
            Assert.That(result.BestPoint[1], Is.EqualTo(0.0).Within(Tol));
        }

        [Test]
        public void ShiftedSphere_FindsShiftedMinimum()
        {
            var result = NelderMeadSimplex.Run(
                x => (x[0] - 2.0) * (x[0] - 2.0) + (x[1] + 3.0) * (x[1] + 3.0),
                new[] { 0.0, 0.0 },
                tolerance: 1e-8);

            Assert.That(result.BestPoint[0], Is.EqualTo(2.0).Within(Tol));
            Assert.That(result.BestPoint[1], Is.EqualTo(-3.0).Within(Tol));
        }

        [Test]
        public void Booth_FindsMinimum()
        {
            var result = NelderMeadSimplex.Run(
                x => Math.Pow(x[0] + 2 * x[1] - 7, 2) + Math.Pow(2 * x[0] + x[1] - 5, 2),
                new[] { 0.0, 0.0 },
                tolerance: 1e-8);

            Assert.That(result.BestPoint[0], Is.EqualTo(1.0).Within(Tol));
            Assert.That(result.BestPoint[1], Is.EqualTo(3.0).Within(Tol));
        }

        [Test]
        public void Rosenbrock_FindsMinimum()
        {
            var result = NelderMeadSimplex.Run(
                x => 100 * Math.Pow(x[1] - x[0] * x[0], 2) + Math.Pow(1 - x[0], 2),
                new[] { -1.2, 1.0 },
                maxIter: 5000,
                tolerance: 1e-8);

            Assert.That(result.BestPoint[0], Is.EqualTo(1.0).Within(Tol));
            Assert.That(result.BestPoint[1], Is.EqualTo(1.0).Within(Tol));
        }

        [Test]
        public void ThreeDimensionalSphere_FindsOrigin()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1] + x[2] * x[2],
                new[] { 3.0, -2.0, 5.0 },
                tolerance: 1e-8);

            Assert.That(result.BestPoint[0], Is.EqualTo(0.0).Within(Tol));
            Assert.That(result.BestPoint[1], Is.EqualTo(0.0).Within(Tol));
            Assert.That(result.BestPoint[2], Is.EqualTo(0.0).Within(Tol));
        }

        [Test]
        public void FourDimensionalSphere_FindsOrigin()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1] + x[2] * x[2] + x[3] * x[3],
                new[] { 1.0, 2.0, 3.0, 4.0 },
                tolerance: 1e-8);

            Assert.That(result.BestPoint[0], Is.EqualTo(0.0).Within(Tol));
            Assert.That(result.BestPoint[1], Is.EqualTo(0.0).Within(Tol));
            Assert.That(result.BestPoint[2], Is.EqualTo(0.0).Within(Tol));
            Assert.That(result.BestPoint[3], Is.EqualTo(0.0).Within(Tol));
        }

        [Test]
        public void FiveDimensionalSphere_FindsOrigin()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1] + x[2] * x[2] + x[3] * x[3] + x[4] * x[4],
                new[] { 1.0, 2.0, 3.0, 4.0, 5.0 },
                tolerance: 1e-8);

            Assert.That(result.BestPoint[0], Is.EqualTo(0.0).Within(LooseTol));
            Assert.That(result.BestPoint[1], Is.EqualTo(0.0).Within(LooseTol));
            Assert.That(result.BestPoint[2], Is.EqualTo(0.0).Within(LooseTol));
            Assert.That(result.BestPoint[3], Is.EqualTo(0.0).Within(LooseTol));
            Assert.That(result.BestPoint[4], Is.EqualTo(0.0).Within(LooseTol));
        }

        [Test]
        public void SingleVariable_FindsMinimum()
        {
            var result = NelderMeadSimplex.Run(
                x => (x[0] - 3.0) * (x[0] - 3.0),
                new[] { 0.0 },
                tolerance: 1e-10);

            Assert.That(result.BestPoint[0], Is.EqualTo(3.0).Within(1e-3));
        }

        [Test]
        public void History_IsNotEmpty()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { 5.0, 5.0 });

            Assert.That(result.History.Count, Is.GreaterThan(1));
        }

        [Test]
        public void History_StartsWithInitialPoint()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { 5.0, 5.0 });

            Assert.That(result.History[0].point[0], Is.EqualTo(5.0).Within(Tol));
            Assert.That(result.History[0].point[1], Is.EqualTo(5.0).Within(Tol));
        }

        [Test]
        public void History_LastIsBest()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { 5.0, 5.0 },
                tolerance: 1e-8);

            var last = result.History[result.History.Count - 1];
            Assert.That(last.point[0], Is.EqualTo(result.BestPoint[0]).Within(Tol));
            Assert.That(last.point[1], Is.EqualTo(result.BestPoint[1]).Within(Tol));
        }

        [Test]
        public void Result_NotNull()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0],
                new[] { 1.0 });

            Assert.That(result, Is.Not.Null);
            Assert.That(result.BestPoint, Is.Not.Null);
            Assert.That(result.History, Is.Not.Null);
        }

        [Test]
        public void BestValue_MatchesFunctionAtBestPoint()
        {
            Func<double[], double> func = x => x[0] * x[0] + x[1] * x[1];
            var result = NelderMeadSimplex.Run(func, new[] { 5.0, 5.0 }, tolerance: 1e-8);

            double computedValue = func(result.BestPoint);
            Assert.That(result.BestValue, Is.EqualTo(computedValue).Within(Tol));
        }

        [Test]
        public void BestValue_DecreasesFromStart()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { 5.0, 5.0 });

            double initialValue = 5.0 * 5.0 + 5.0 * 5.0;
            Assert.That(result.BestValue, Is.LessThan(initialValue));
        }

        [Test]
        public void MaxIter_StopsEarly()
        {
            var result = NelderMeadSimplex.Run(
                x => 100 * Math.Pow(x[1] - x[0] * x[0], 2) + Math.Pow(1 - x[0], 2),
                new[] { -1.2, 1.0 },
                maxIter: 10);

            Assert.That(result.History.Count, Is.LessThanOrEqualTo(11));
        }

        [Test]
        public void DefaultParameters_Work()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { 5.0, 5.0 });

            Assert.That(result, Is.Not.Null);
            Assert.That(result.BestPoint, Is.Not.Null);
        }

        [Test]
        public void CustomAlphaBetaGammaDelta_Work()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { 5.0, 5.0 },
                alpha: 1.5,
                beta: 0.3,
                gamma: 2.5,
                delta: 0.7,
                tolerance: 1e-8);

            Assert.That(result.BestPoint[0], Is.EqualTo(0.0).Within(Tol));
            Assert.That(result.BestPoint[1], Is.EqualTo(0.0).Within(Tol));
        }

        [Test]
        public void LargeTolerance_StopsEarly()
        {
            var result = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { 5.0, 5.0 },
                tolerance: 10.0);

            Assert.That(result.History.Count, Is.LessThan(10));
        }

        [Test]
        public void DifferentInitialPoints_ConvergeToSameMinimum()
        {
            var r1 = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { 5.0, 5.0 },
                tolerance: 1e-8);

            var r2 = NelderMeadSimplex.Run(
                x => x[0] * x[0] + x[1] * x[1],
                new[] { -3.0, 4.0 },
                tolerance: 1e-8);

            Assert.That(r1.BestPoint[0], Is.EqualTo(0.0).Within(Tol));
            Assert.That(r1.BestPoint[1], Is.EqualTo(0.0).Within(Tol));
            Assert.That(r2.BestPoint[0], Is.EqualTo(0.0).Within(Tol));
            Assert.That(r2.BestPoint[1], Is.EqualTo(0.0).Within(Tol));
        }

        [Test]
        public void Himmelblau_FindsOneOfFourMinima()
        {
            var result = NelderMeadSimplex.Run(
                x => Math.Pow(x[0] * x[0] + x[1] - 11, 2) + Math.Pow(x[0] + x[1] * x[1] - 7, 2),
                new[] { 0.0, 0.0 },
                maxIter: 5000,
                tolerance: 1e-8);

            double f = result.BestValue;
            Assert.That(f, Is.LessThan(1e-5));
        }
    }
}