// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Asin(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal asinDecimalDelta = 0.0004m;
        private const decimal asinDecimalExpectedResult = 1.5707963267948966192313218368m;

        [Benchmark(InnerIterationCount = AsinDecimalIterations)]
        public static void AsinDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        AsinDecimalTest();
                    }
                }
            }
        }

        public static void AsinDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += asinDecimalDelta;
                result += McNeight.MathM.Asin(value);
            }

            var diff = McNeight.MathM.Abs(asinDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {asinDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
