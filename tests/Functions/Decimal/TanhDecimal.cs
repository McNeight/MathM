// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Tanh(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal tanhDecimalDelta = 0.0004m;
        private const decimal tanhDecimalExpectedResult = 0.76159415578341827m;

        [Benchmark(InnerIterationCount = TanhDecimalIterations)]
        public static void TanhDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        TanhDecimalTest();
                    }
                }
            }
        }

        public static void TanhDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += tanhDecimalDelta;
                result += McNeight.MathM.Tanh(value);
            }

            var diff = McNeight.MathM.Abs(tanhDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {tanhDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
