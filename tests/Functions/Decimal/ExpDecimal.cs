// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Exp(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal expDecimalDelta = 0.0004m;
        private const decimal expDecimalExpectedResult = 5877.1812477593971198642329104m;

        [Benchmark(InnerIterationCount = ExpDecimalIterations)]
        public static void ExpDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        ExpDecimalTest();
                    }
                }
            }
        }

        public static void ExpDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += expDecimalDelta;
                result += McNeight.MathM.Exp(value);
            }

            var diff = McNeight.MathM.Abs(expDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {expDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
