// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace Simple
{
    /// <summary>
    /// Copied from corefx\src\System.Runtime.Extensions\tests\System\Math.cs
    /// </summary>
    public static partial class CoreFxMathTests
    {
        [Fact]
        public static void Abs_Decimal()
        {
            Assert.Equal(3.0m, McNeight.MathM.Abs(3.0m));
            Assert.Equal(0.0m, McNeight.MathM.Abs(0.0m));
            Assert.Equal(0.0m, McNeight.MathM.Abs(-0.0m));
            Assert.Equal(3.0m, McNeight.MathM.Abs(-3.0m));
            Assert.Equal(decimal.MaxValue, McNeight.MathM.Abs(decimal.MinValue));
        }

        [Fact]
        public static void Ceiling_Decimal()
        {
            Assert.Equal(2.0m, McNeight.MathM.Ceiling(1.1m));
            Assert.Equal(2.0m, McNeight.MathM.Ceiling(1.9m));
            Assert.Equal(-1.0m, McNeight.MathM.Ceiling(-1.1m));
        }

        [Fact]
        public static void Floor_Decimal()
        {
            Assert.Equal(1.0m, McNeight.MathM.Floor(1.1m));
            Assert.Equal(1.0m, McNeight.MathM.Floor(1.9m));
            Assert.Equal(-2.0m, McNeight.MathM.Floor(-1.1m));
        }

        [Fact]
        public static void Max_Decimal()
        {
            Assert.Equal(3.0m, McNeight.MathM.Max(-2.0m, 3.0m));
            Assert.Equal(decimal.MaxValue, McNeight.MathM.Max(decimal.MinValue, decimal.MaxValue));
        }

        [Fact]
        public static void Min_Decimal()
        {
            Assert.Equal(-2.0m, McNeight.MathM.Min(3.0m, -2.0m));
            Assert.Equal(decimal.MinValue, McNeight.MathM.Min(decimal.MinValue, decimal.MaxValue));
        }

        [Fact]
        public static void Round_Decimal()
        {
            //Assert.Equal(0.0m, McNeight.MathM.Round(0.0m));
            //Assert.Equal(1.0m, McNeight.MathM.Round(1.4m));
            //Assert.Equal(2.0m, McNeight.MathM.Round(1.5m));
            //Assert.Equal(2e16m, McNeight.MathM.Round(2e16m));
            //Assert.Equal(0.0m, McNeight.MathM.Round(-0.0m));
            //Assert.Equal(-1.0m, McNeight.MathM.Round(-1.4m));
            //Assert.Equal(-2.0m, McNeight.MathM.Round(-1.5m));
            //Assert.Equal(-2e16m, McNeight.MathM.Round(-2e16m));
        }

        [Fact]
        public static void Round_Decimal_Digits()
        {
            //Assert.Equal(3.422m, McNeight.MathM.Round(3.42156m, 3, System.MidpointRounding.AwayFromZero));
            //Assert.Equal(-3.422m, McNeight.MathM.Round(-3.42156m, 3, System.MidpointRounding.AwayFromZero));
            //Assert.Equal(decimal.Zero, McNeight.MathM.Round(decimal.Zero, 3, System.MidpointRounding.AwayFromZero));
        }

        [Fact]
        public static void Sign_Decimal()
        {
            Assert.Equal(0, McNeight.MathM.Sign(0.0m));
            Assert.Equal(0, McNeight.MathM.Sign(-0.0m));
            Assert.Equal(-1, McNeight.MathM.Sign(-3.14m));
            Assert.Equal(1, McNeight.MathM.Sign(3.14m));
        }

        [Fact]
        public static void Truncate_Decimal()
        {
            Assert.Equal(0.0m, McNeight.MathM.Truncate(0.12345m));
            Assert.Equal(3.0m, McNeight.MathM.Truncate(3.14159m));
            Assert.Equal(-3.0m, McNeight.MathM.Truncate(-3.14159m));
        }
    }
}
