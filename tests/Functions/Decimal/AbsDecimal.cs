// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Abs(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal absDecimalDelta = 0.0004m;
        private const decimal absDecimalExpectedResult = 2500.0m;

        [Benchmark(InnerIterationCount = AbsDecimalIterations)]
        public static void AbsDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (var i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        AbsDecimalTest();
                    }
                }
            }
        }

        public static void AbsDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += absDecimalDelta;
                result += McNeight.MathM.Abs(value);
            }

            var diff = McNeight.MathM.Abs(absDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {absDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
