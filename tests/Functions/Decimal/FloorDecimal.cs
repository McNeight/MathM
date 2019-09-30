// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Floor(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal floorDecimalDelta = 0.0004m;
        private const decimal floorDecimalExpectedResult = -2498.0m;

        [Benchmark(InnerIterationCount = FloorDecimalIterations)]
        public static void FloorDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        FloorDecimalTest();
                    }
                }
            }
        }

        public static void FloorDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += floorDecimalDelta;
                result += McNeight.MathM.Floor(value);
            }

            var diff = McNeight.MathM.Abs(floorDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {floorDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
