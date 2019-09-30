// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

using Microsoft.Xunit.Performance;

namespace Functions
{
    /// <summary>
    /// Tests MathM.Pow(decimal, decimal) over 5000 iterations for the domain x: +2, +1; y: -2, -1.
    /// </summary>
    public static partial class MathTests
    {
        private const decimal powDecimalDeltaX = -0.0004m;
        private const decimal powDecimalDeltaY = 0.0004m;
        private const decimal powDecimalExpectedResult = 4659.4627376139710306530085064m;

        [Benchmark(InnerIterationCount = PowDecimalIterations)]
        public static void PowDecimalBenchmark()
        {
            foreach (var iteration in Benchmark.Iterations)
            {
                using (iteration.StartMeasurement())
                {
                    for (int i = 0; i < Benchmark.InnerIterationCount; i++)
                    {
                        PowDecimalTest();
                    }
                }
            }
        }

        public static void PowDecimalTest()
        {
            var result = 0.0m;
            var valueX = 2.0m;
            var valueY = -2.0m;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                valueX += powDecimalDeltaX;
                valueY += powDecimalDeltaY;
                result += McNeight.MathM.Pow(valueX, valueY);
            }

            var diff = McNeight.MathM.Abs(powDecimalExpectedResult - result);

            if (diff > decimalEpsilon)
            {
                throw new Exception($"Expected Result {powDecimalExpectedResult,33:g31}; Actual Result {result,33:g31}");
            }
        }
    }
}
