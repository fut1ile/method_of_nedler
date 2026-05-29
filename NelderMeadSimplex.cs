using System;
using System.Collections.Generic;
using System.Linq;

namespace NelderMeadApp
{
    public class NelderMeadResult
    {
        public double[] BestPoint { get; set; }
        public double BestValue { get; set; }
        public List<(double[] point, double value)> History { get; set; }
    }

    public static class NelderMeadSimplex
    {
        public static NelderMeadResult Run(
            Func<double[], double> func,
            double[] x0,
            int maxIter = 1000,
            double tolerance = 1e-6,
            double alpha = 1.0,
            double beta = 0.5,
            double gamma = 2.0,
            double delta = 0.5)
        {
            int n = x0.Length;

            List<double[]> simplex = new List<double[]>();
            simplex.Add((double[])x0.Clone());

            for (int i = 0; i < n; i++)
            {
                double[] point = (double[])x0.Clone();
                point[i] = x0[i] != 0 ? x0[i] * 1.05 + 0.001 : 0.00025;
                simplex.Add(point);
            }

            double[] values = simplex.Select(v => func(v)).ToArray();
            var history = new List<(double[], double)>();
            history.Add(((double[])simplex[0].Clone(), values[0]));

            for (int iter = 0; iter < maxIter; iter++)
            {
                int[] indices = Enumerable.Range(0, n + 1).ToArray();
                Array.Sort(indices, (a, b) => values[a].CompareTo(values[b]));

                var sortedSimplex = indices.Select(i => simplex[i]).ToArray();
                var sortedValues = indices.Select(i => values[i]).ToArray();

                double range = sortedValues[n] - sortedValues[0];
                if (range < tolerance) break;

                double[] centroid = new double[n];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        centroid[j] += sortedSimplex[i][j];
                for (int j = 0; j < n; j++)
                    centroid[j] /= n;

                double[] xr = new double[n];
                for (int j = 0; j < n; j++)
                    xr[j] = centroid[j] + alpha * (centroid[j] - sortedSimplex[n][j]);
                double fxr = func(xr);

                if (fxr < sortedValues[0])
                {
                    double[] xe = new double[n];
                    for (int j = 0; j < n; j++)
                        xe[j] = centroid[j] + gamma * (xr[j] - centroid[j]);
                    double fxe = func(xe);
                    if (fxe < fxr)
                    {
                        simplex[indices[n]] = xe;
                        values[indices[n]] = fxe;
                    }
                    else
                    {
                        simplex[indices[n]] = xr;
                        values[indices[n]] = fxr;
                    }
                }
                else if (fxr < sortedValues[n - 1])
                {
                    simplex[indices[n]] = xr;
                    values[indices[n]] = fxr;
                }
                else
                {
                    if (fxr < sortedValues[n])
                    {
                        double[] xc = new double[n];
                        for (int j = 0; j < n; j++)
                            xc[j] = centroid[j] + beta * (xr[j] - centroid[j]);
                        double fxc = func(xc);
                        if (fxc <= fxr)
                        {
                            simplex[indices[n]] = xc;
                            values[indices[n]] = fxc;
                        }
                        else Shrink(simplex, values, sortedSimplex[0], func, delta, indices);
                    }
                    else
                    {
                        double[] xc = new double[n];
                        for (int j = 0; j < n; j++)
                            xc[j] = centroid[j] + beta * (sortedSimplex[n][j] - centroid[j]);
                        double fxc = func(xc);
                        if (fxc < sortedValues[n])
                        {
                            simplex[indices[n]] = xc;
                            values[indices[n]] = fxc;
                        }
                        else Shrink(simplex, values, sortedSimplex[0], func, delta, indices);
                    }
                }

                double currentBest = values.Min();
                int bestIdx = Array.IndexOf(values, currentBest);
                history.Add(((double[])simplex[bestIdx].Clone(), currentBest));
            }

            double minVal = values.Min();
            int minIdx = Array.IndexOf(values, minVal);
            return new NelderMeadResult
            {
                BestPoint = (double[])simplex[minIdx].Clone(),
                BestValue = minVal,
                History = history
            };
        }

        private static void Shrink(List<double[]> simplex, double[] values,
            double[] bestPoint, Func<double[], double> func, double delta, int[] indices)
        {
            int n = bestPoint.Length;
            for (int i = 0; i < simplex.Count; i++)
            {
                if (i == indices[0]) continue;
                for (int j = 0; j < n; j++)
                    simplex[i][j] = bestPoint[j] + delta * (simplex[i][j] - bestPoint[j]);
                values[i] = func(simplex[i]);
            }
        }
    }
}