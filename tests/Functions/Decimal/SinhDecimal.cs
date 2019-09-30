// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Sinh(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal sinhDecimalDelta = 0.0004m;
        private const decimal sinhDecimalExpectedResult = 1.17520119337903m;

        [Benchmark(InnerIterationCount = SinhDecimalIterations)]
        public static void SinhDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        SinhDecimalTest();
                    }
                }
            }
        }

        public static void SinhDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += sinhDecimalDelta;
                result += McNeight.MathM.Sinh(value);
            }

            var diff = McNeight.MathM.Abs(sinhDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {sinhDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
