// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Atan(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal atanDecimalDelta = 0.0004m;
        private const decimal atanDecimalExpectedResult = 0.7853981633974483096156606913m;

        [Benchmark(InnerIterationCount = AtanDecimalIterations)]
        public static void AtanDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        AtanDecimalTest();
                    }
                }
            }
        }

        public static void AtanDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += atanDecimalDelta;
                result += McNeight.MathM.Atan(value);
            }

            var diff = McNeight.MathM.Abs(atanDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {atanDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
