// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Sin(decimal) over 5000 iterations for the domain -PI/2, +PI/2.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal sinDecimalDelta = 0.0006283185307179586476925286766559m; // McNeight.MathM.PI / 5000.0m;
        private const decimal sinDecimalExpectedResult = 1.0000000000000612151963584216m;

        [Benchmark(InnerIterationCount = SinDecimalIterations)]
        public static void SinDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        SinDecimalTest();
                    }
                }
            }
        }

        public static void SinDecimalTest()
        {
            var result = 0.0m;
            var value = -1.5707963267948966m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += sinDecimalDelta;
                result += McNeight.MathM.Sin(value);
            }

            var diff = McNeight.MathM.Abs(sinDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {sinDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
