// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Log(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal logDecimalDelta = 0.0004m;
        private const decimal logDecimalExpectedResult = -1529.086545404694017014286996m;

        [Benchmark(InnerIterationCount = LogDecimalIterations)]
        public static void LogDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        LogDecimalTest();
                    }
                }
            }
        }

        public static void LogDecimalTest()
        {
            var result = 0.0m;
            var value = 0.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += logDecimalDelta;
                result += McNeight.MathM.Log(value);
            }

            var diff = McNeight.MathM.Abs(logDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {logDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }

}
