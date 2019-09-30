// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Ceiling(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal ceilingDecimalDelta = 0.0004m;
        private const decimal ceilingDecimalExpectedResult = 2500.0m;

        [Benchmark(InnerIterationCount = CeilingDecimalIterations)]
        public static void CeilingDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        CeilingDecimalTest();
                    }
                }
            }
        }

        public static void CeilingDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += ceilingDecimalDelta;
                result += McNeight.MathM.Ceiling(value);
            }

            var diff = McNeight.MathM.Abs(ceilingDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {ceilingDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
