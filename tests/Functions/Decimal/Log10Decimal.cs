// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Log10(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal log10DecimalDelta = 0.0004m;
        private const decimal log10DecimalExpectedResult = -664.0738490217647398565657331m;

        [Benchmark(InnerIterationCount = Log10DecimalIterations)]
        public static void Log10DecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        Log10DecimalTest();
                    }
                }
            }
        }

        public static void Log10DecimalTest()
        {
            var result = 0.0m;
            var value = 0.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += log10DecimalDelta;
                result += McNeight.MathM.Log10(value);
            }

            var diff = McNeight.MathM.Abs(log10DecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {log10DecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
