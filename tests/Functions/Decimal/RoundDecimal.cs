// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Round(decimal) over 5000 iterations for the domain -PI/2, +PI/2.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal roundDecimalDelta = 0.0006283185307180m;
        private const decimal roundDecimalExpectedResult = 2.0m;

        [Benchmark(InnerIterationCount = RoundDecimalIterations)]
        public static void RoundDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        RoundDecimalTest();
                    }
                }
            }
        }

        public static void RoundDecimalTest()
        {
            var result = 0.0m;
            var value = -1.5707963267948966m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += roundDecimalDelta;
                result += McNeight.MathM.Round(value);
            }

            var diff = McNeight.MathM.Abs(roundDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {roundDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
