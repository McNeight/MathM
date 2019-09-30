// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Atan2(decimal, decimal) over 5000 iterations for the domain y: -1, +1; x: +1, -1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal atan2DecimalDeltaX = -0.0004m;
        private const decimal atan2DecimalDeltaY = 0.0004m;
        private const decimal atan2DecimalExpectedResult = 3927.7762151506389963879198876m;

        [Benchmark(InnerIterationCount = Atan2DecimalIterations)]
        public static void Atan2DecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        Atan2DecimalTest();
                    }
                }
            }
        }

        public static void Atan2DecimalTest()
        {
            var result = 0.0m;
            var valueX = 1.0m;
            var valueY = -1.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                valueX += atan2DecimalDeltaX;
                valueY += atan2DecimalDeltaY;
                result += McNeight.MathM.Atan2(valueY, valueX);
            }

            var diff = McNeight.MathM.Abs(atan2DecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {atan2DecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
