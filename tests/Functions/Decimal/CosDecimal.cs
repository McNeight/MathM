// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Cos(decimal) over 5000 iterations for the domain 0, PI.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal cosDecimalDelta = 0.0006283185307179586476925286766559m; // McNeight.MathM.PI / 5000.0m;
        private const decimal cosDecimalExpectedResult = -1.0000000000000000000001843474m; // Why not -1.0m?

        [Benchmark(InnerIterationCount = CosDecimalIterations)]
        public static void CosDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (var i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        CosDecimalTest();
                    }
                }
            }
        }

        public static void CosDecimalTest()
        {
            var result = 0.0m;
            var value = 0.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += cosDecimalDelta;
                result += McNeight.MathM.Cos(value);
            }

            var diff = McNeight.MathM.Abs(cosDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {cosDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
