// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

using Xunit;

namespace Simple
{
    /// <summary>
    /// Copied from corefx\src\System.Runtime.Extensions\tests\System\MathF.netcoreapp.cs
    /// </summary>
    public static partial class CoreFxMathFTests
    {
        public static IEnumerable<object[]> AbsData => new List<object[]>
        {
            new object[] { -3.1415926535897932384626433832795m, 3.1415926535897932384626433832795m }, // value: -(pi)             expected: (pi)
            new object[] { -2.7182818284590452353602874713527m, 2.7182818284590452353602874713527m }, // value: -(e)              expected: (e)
            new object[] { -2.3025850929940456840179914546844m, 2.3025850929940456840179914546844m }, // value: -(ln(10))         expected: (ln(10))
            new object[] { -1.5707963267948966192313216916398m, 1.5707963267948966192313216916398m }, // value: -(pi / 2)         expected: (pi / 2)
            new object[] { -1.4426950408889634073599246810019m, 1.4426950408889634073599246810019m }, // value: -(log2(e))        expected: (log2(e))
            new object[] { -1.4142135623730950488016887242097m, 1.4142135623730950488016887242097m }, // value: -(sqrt(2))        expected: (sqrt(2))
            new object[] { -1.1283791670955125738961589031215m, 1.1283791670955125738961589031215m }, // value: -(2 / sqrt(pi))   expected: (2 / sqrt(pi))
            new object[] { -1.0m, 1.0m },
            new object[] { -0.78539816339744830961566084581988m, 0.78539816339744830961566084581988m }, // value: -(pi / 4)         expected: (pi / 4)
            new object[] { -0.70710678118654752440084436210485m, 0.70710678118654752440084436210485m }, // value: -(1 / sqrt(2))    expected: (1 / sqrt(2))
            new object[] { -0.69314718055994530941723212145818m, 0.69314718055994530941723212145818m }, // value: -(ln(2))          expected: (ln(2))
            new object[] { -0.63661977236758134307553505349006m, 0.63661977236758134307553505349006m }, // value: -(2 / pi)         expected: (2 / pi)
            new object[] { -0.43429448190325182765112891891661m, 0.43429448190325182765112891891661m }, // value: -(log10(e))       expected: (log10(e))
            new object[] { -0.31830988618379067153776752674503m, 0.31830988618379067153776752674503m }, // value: -(1 / pi)         expected: (1 / pi)
            new object[] { -0.0m, 0.0m },
            new object[] {  0.0m, 0.0m },
            new object[] {  0.31830988618379067153776752674503m, 0.31830988618379067153776752674503m }, // value:  (1 / pi)         expected: (1 / pi)
            new object[] {  0.43429448190325182765112891891661m, 0.43429448190325182765112891891661m }, // value:  (log10(e))       expected: (log10(e))
            new object[] {  0.63661977236758134307553505349006m, 0.63661977236758134307553505349006m }, // value:  (2 / pi)         expected: (2 / pi)
            new object[] {  0.69314718055994530941723212145818m, 0.69314718055994530941723212145818m }, // value:  (ln(2))          expected: (ln(2))
            new object[] {  0.70710678118654752440084436210485m, 0.70710678118654752440084436210485m }, // value:  (1 / sqrt(2))    expected: (1 / sqrt(2))
            new object[] {  0.78539816339744830961566084581988m, 0.78539816339744830961566084581988m }, // value:  (pi / 4)         expected: (pi / 4)
            new object[] {  1.0m, 1.0m },
            new object[] {  1.1283791670955125738961589031215m, 1.1283791670955125738961589031215m }, // value:  (2 / sqrt(pi))   expected: (2 / sqrt(pi))
            new object[] {  1.4142135623730950488016887242097m, 1.4142135623730950488016887242097m }, // value:  (sqrt(2))        expected: (sqrt(2))
            new object[] {  1.4426950408889634073599246810019m, 1.4426950408889634073599246810019m }, // value:  (log2(e))        expected: (log2(e))
            new object[] {  1.5707963267948966192313216916398m, 1.5707963267948966192313216916398m }, // value:  (pi / 2)         expected: (pi / 2)
            new object[] {  2.3025850929940456840179914546844m, 2.3025850929940456840179914546844m }, // value:  (ln(10))         expected: (ln(10))
            new object[] {  2.7182818284590452353602874713527m, 2.7182818284590452353602874713527m }, // value:  (e)              expected: (e)
            new object[] {  3.1415926535897932384626433832795m, 3.1415926535897932384626433832795m }, // value:  (pi)             expected: (pi)
        };

        [Theory]
        [MemberData(nameof(AbsData))]
        public static void Abs_Decimal(decimal value, decimal expectedResult)
        {
            Assert.Equal(expectedResult, McNeight.MathM.Abs(value));
        }

        public static IEnumerable<object[]> AcosData => new List<object[]>
        {
            new object[] { -1.0m, 3.1415926535897932384626433832795m }, // value: -1.0              expected: (pi)
            new object[] { -0.91173391478696509789371731780543m, 2.7182818284590452353602874713527m }, // expected: (e)
            new object[] { -0.66820151019031294624233069665614m, 2.3025850929940456840179914546844m }, // expected: (ln(10))
            new object[] { -0.0m, 1.5707963267948966192313216916398m }, // expected: (pi / 2)
            new object[] {  0.0m, 1.5707963267948966192313216916398m }, // expected: (pi / 2)
            new object[] {  0.31830988618379067153776752674503m, 0.31830988618379067153776752674503m }, // value:  (1 / pi)         expected: (1 / pi)
            new object[] {  0.43429448190325182765112891891661m, 0.43429448190325182765112891891661m }, // value:  (log10(e))       expected: (log10(e))
            new object[] {  0.63661977236758134307553505349006m, 0.63661977236758134307553505349006m }, // value:  (2 / pi)         expected: (2 / pi)
            new object[] {  0.69314718055994530941723212145818m, 0.69314718055994530941723212145818m }, // value:  (ln(2))          expected: (ln(2))
            new object[] {  0.70710678118654752440084436210485m, 0.70710678118654752440084436210485m }, // value:  (1 / sqrt(2))    expected: (1 / sqrt(2))
            new object[] {  0.78539816339744830961566084581988m, 0.78539816339744830961566084581988m }, // value:  (pi / 4)         expected: (pi / 4)
            new object[] {  1.0m, 0.0m },
        };

        [Theory]
        //[InlineData(0.12775121753523991, 1.4426950408889634, CrossPlatformMachineEpsilon * 10)]   // expected:  (log2(e))
        //[InlineData(0.15594369476537447, 1.4142135623730950, CrossPlatformMachineEpsilon * 10)]   // expected:  (sqrt(2))
        //[InlineData(0.42812514788535792, 1.1283791670955126, CrossPlatformMachineEpsilon * 10)]   // expected:  (2 / sqrt(pi))
        //[InlineData(0.54030230586813972, 1.0, CrossPlatformMachineEpsilon * 10)]
        //[InlineData(0.70710678118654752, 0.78539816339744831, CrossPlatformMachineEpsilon)]        // expected:  (pi / 4),         value:  (1 / sqrt(2))
        //[InlineData(0.76024459707563015, 0.70710678118654752, CrossPlatformMachineEpsilon)]        // expected:  (1 / sqrt(2))
        //[InlineData(0.76923890136397213, 0.69314718055994531, CrossPlatformMachineEpsilon)]        // expected:  (ln(2))
        //[InlineData(0.80410982822879171, 0.63661977236758134, CrossPlatformMachineEpsilon)]        // expected:  (2 / pi)
        //[InlineData(0.90716712923909839, 0.43429448190325183, CrossPlatformMachineEpsilon)]        // expected:  (log10(e))
        //[InlineData(0.94976571538163866, 0.31830988618379067, CrossPlatformMachineEpsilon)]        // expected:  (1 / pi)
        [MemberData(nameof(AcosData))]
        public static void Acos_Decimal(decimal value, decimal expectedResult)
        {
            Assert.Equal(expectedResult, McNeight.MathM.Acos(value));
        }

        public static IEnumerable<object[]> AcosInvalid => new List<object[]>
        {
            new object[] { -3.1415926535897932384626433832795m }, // value: -(pi)
            new object[] { -2.7182818284590452353602874713527m }, // value: -(e)
            new object[] { -2.3025850929940456840179914546844m }, // value: -(ln(10))
            new object[] { -1.5707963267948966192313216916398m }, // value: -(pi / 2)
            new object[] { -1.4426950408889634073599246810019m }, // value: -(log2(e))
            new object[] { -1.4142135623730950488016887242097m }, // value: -(sqrt(2))
            new object[] { -1.1283791670955125738961589031215m }, // value: -(2 / sqrt(pi))
            new object[] {  1.1283791670955125738961589031215m }, // value:  (2 / sqrt(pi))
            new object[] {  1.4142135623730950488016887242097m }, // value:  (sqrt(2))
            new object[] {  1.4426950408889634073599246810019m }, // value:  (log2(e))
            new object[] {  1.5707963267948966192313216916398m }, // value:  (pi / 2)
            new object[] {  2.3025850929940456840179914546844m }, // value:  (ln(10))
            new object[] {  2.7182818284590452353602874713527m }, // value:  (e)
            new object[] {  3.1415926535897932384626433832795m }, // value:  (pi)
        };

        [Theory]
        [MemberData(nameof(AcosInvalid))]
        public static void Acos_Exception(decimal value)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => McNeight.MathM.Acos(value));
        }
    }
}
