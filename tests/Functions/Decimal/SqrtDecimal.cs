// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Sqrt(decimal) over 5000 iterations for the domain 0, PI.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal sqrtDecimalDelta = 0.0006283185307179586476925286766559m; // McNeight.MathM.PI / 5000.0m;
        private const decimal sqrtDecimalExpectedResult = 5909.0605337793939204521769672m;

        [Benchmark(InnerIterationCount = SqrtDecimalIterations)]
        public static void SqrtDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        SqrtDecimalTest();
                    }
                }
            }
        }

        public static void SqrtDecimalTest()
        {
            var result = 0.0m;
            var value = 0.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += sqrtDecimalDelta;
                result += McNeight.MathM.Sqrt(value);
            }

            var diff = McNeight.MathM.Abs(sqrtDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {sqrtDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
