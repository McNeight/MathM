// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Tan(decimal) over 5000 iterations for the domain -PI/2, +PI/2.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal tanDecimalDelta = 0.0004m;
        private const decimal tanDecimalExpectedResult = 1.5574077246549022305069747994m;

        [Benchmark(InnerIterationCount = TanDecimalIterations)]
        public static void TanDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        TanDecimalTest();
                    }
                }
            }
        }

        public static void TanDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += tanDecimalDelta;
                result += McNeight.MathM.Tan(value);
            }

            var diff = McNeight.MathM.Abs(tanDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {tanDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
