// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Acos(decimal) over 5000 iterations for the domain -1, +1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal acosDecimalDelta = 0.0004m;
        private const decimal acosDecimalExpectedResult = 7852.4108376476881995373771361m;

        [Benchmark(InnerIterationCount = AcosDecimalIterations)]
        public static void AcosDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (var i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        AcosDecimalTest();
                    }
                }
            }
        }

        public static void AcosDecimalTest()
        {
            var result = 0.0m;
            var value = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                value += acosDecimalDelta;
                result += McNeight.MathM.Acos(value);
            }

            var diff = McNeight.MathM.Abs(acosDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {acosDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
