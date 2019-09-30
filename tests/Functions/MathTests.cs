// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Functions
{
    public static partial class MathTests
    {
        // decimal has a machine epsilon of 0. However, due to floating-point precision
        // errors, this is too accurate when aggregating values of a set of iterations. Using the
        // double-precision machine epsilon as our epsilon should be 'good enough' for the purposes
        // of the perf testing as it ensures we get the expected value and that it is at least as precise
        // as we would have computed with the double-precision version of the function (without aggregation).
        private const decimal decimalEpsilon = 0;

        // 5000 iterations is enough to cover the full domain of inputs for certain functions (such
        // as Cos, which has a domain of 0 to PI) at reasonable intervals (in the case of Cos, the
        // interval is PI / 5000 which is 0.0006283185307180). It should also give reasonable coverage
        // for functions which have a larger domain (such as Atan, which covers the full set of real numbers).
        private const int iterations = 5000;

        // While iterations covers the domain of inputs, the full span of results doesn't run long enough
        // to meet our siginificance criteria. So each test is repeated many times, using the factors below.
        private const int AbsDecimalIterations = 200000;
        private const int AcosDecimalIterations = 10000;
        private const int AsinDecimalIterations = 10000;
        private const int Atan2DecimalIterations = 6500;
        private const int AtanDecimalIterations = 13000;
        private const int CeilingDecimalIterations = 80000;
        private const int CosDecimalIterations = 16000;
        private const int CoshDecimalIterations = 8000;
        private const int ExpDecimalIterations = 16000;
        private const int FloorDecimalIterations = 80000;
        private const int Log10DecimalIterations = 16000;
        private const int LogDecimalIterations = 20000;
        private const int PowDecimalIterations = 4000;
        private const int RoundDecimalIterations = 35000;
        private const int SinDecimalIterations = 16000;
        private const int SinhDecimalIterations = 8000;
        private const int SqrtDecimalIterations = 40000;
        private const int TanDecimalIterations = 16000;
        private const int TanhDecimalIterations = 17000;
    }
}
