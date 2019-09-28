// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;
using Xunit.Sdk;
using System.Collections.Generic;

namespace McNeight
{
    public static partial class DecimalTests
    {
        [Fact]
        public static void Abs_Decimal()
        {
            Assert.Equal(3.0m, MathM.Abs(3.0m));
            Assert.Equal(0.0m, MathM.Abs(0.0m));
            Assert.Equal(0.0m, MathM.Abs(-0.0m));
            Assert.Equal(3.0m, MathM.Abs(-3.0m));
            Assert.Equal(decimal.MaxValue, MathM.Abs(decimal.MinValue));
        }

        [Fact]
        public static void Ceiling_Decimal()
        {
            Assert.Equal(2.0m, MathM.Ceiling(1.1m));
            Assert.Equal(2.0m, MathM.Ceiling(1.9m));
            Assert.Equal(-1.0m, MathM.Ceiling(-1.1m));
        }

        [Fact]
        public static void Floor_Decimal()
        {
            Assert.Equal(1.0m, MathM.Floor(1.1m));
            Assert.Equal(1.0m, MathM.Floor(1.9m));
            Assert.Equal(-2.0m, MathM.Floor(-1.1m));
        }

        [Fact]
        public static void Max_Decimal()
        {
            Assert.Equal(3.0m, MathM.Max(-2.0m, 3.0m));
            Assert.Equal(decimal.MaxValue, MathM.Max(decimal.MinValue, decimal.MaxValue));
        }

        [Fact]
        public static void Min_Decimal()
        {
            Assert.Equal(-2.0m, MathM.Min(3.0m, -2.0m));
            Assert.Equal(decimal.MinValue, MathM.Min(decimal.MinValue, decimal.MaxValue));
        }

        [Fact]
        public static void Sign_Decimal()
        {
            Assert.Equal(0, MathM.Sign(0.0m));
            Assert.Equal(0, MathM.Sign(-0.0m));
            Assert.Equal(-1, MathM.Sign(-3.14m));
            Assert.Equal(1, MathM.Sign(3.14m));
        }

        [Fact]
        public static void Truncate_Decimal()
        {
            Assert.Equal(0.0m, MathM.Truncate(0.12345m));
            Assert.Equal(3.0m, MathM.Truncate(3.14159m));
            Assert.Equal(-3.0m, MathM.Truncate(-3.14159m));
        }
    }
}
