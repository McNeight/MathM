// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Cosh(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal coshDecimalDelta = 0.0004m;
        private const decimal coshDecimalExpectedResult = 5876.0060465657216m;

        [Benchmark(InnerIterationCount = CoshDecimalIterations)]
        public static void CoshDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        CoshDecimalTest();
                    }
                }
            }
        }

        public static void CoshDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += coshDecimalDelta;
                result += McNeight.MathM.Cosh(value);
            }

            var diff = McNeight.MathM.Abs(coshDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {coshDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
