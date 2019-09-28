// <copyright file="MathM.cs" company="Neil McNeight">
// Copyright © 2015-2019 Nathan P Jones.
// Copyright © 2019 Neil McNeight. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using SR = McNeight.Strings;

namespace McNeight
{
    /// <summary>
    /// Provides constants and static methods for trigonometric, logarithmic, and other common mathematical functions.
    /// </summary>
    public static class MathM
    {
        /// <summary>
        /// Represent e (Euler's constant) using a <see cref="decimal"/>.
        /// </summary>
        /// <remarks>
        /// https://oeis.org/A001113
        /// https://en.wikipedia.org/wiki/E_(mathematical_constant)
        /// </remarks>
        public const decimal E = 2.7182818284590452353602874714m;

        /// <summary>
        /// Represent π (pi) using a <see cref="decimal"/>.
        /// </summary>
        /// <remarks>
        /// https://oeis.org/A000796
        /// https://en.wikipedia.org/wiki/Pi
        /// </remarks>
        public const decimal PI = 3.1415926535897932384626433833m;

        /// <summary> π/2 - in radians is equivalent to 90 degrees. </summary>
        private const decimal PiHalf = 1.5707963267948966192313216916m;          //  90 degrees

        /// <summary> π/4 - in radians is equivalent to 45 degrees. </summary>
        private const decimal PiQuarter = 0.7853981633974483096156608458m;       //  45 degrees

        /// <summary> π/12 - in radians is equivalent to 15 degrees. </summary>
        private const decimal PiTwelfth = 0.2617993877991494365385536153m;       //  15 degrees

        /// <summary> 2π - in radians is equivalent to 360 degrees. </summary>
        private const decimal TwoPi = 6.2831853071795864769252867666m;           // 360 degrees

        private const int maxRoundingDigits = 28;

        private const decimal decimalRoundLimit = 1E28m;

        /// <summary>
        /// The value of the natural logarithm of 10.
        /// </summary>
        /// <remarks>
        /// Full value is: 2.30258509299404568401799145468436420760110148862877297603332790096757
        /// From: http://oeis.org/A002392/constant
        /// </remarks>
        private const decimal Ln10 = 2.3025850929940456840179914547m;

        /// <summary>
        /// The value of the natural logarithm of 2.
        /// </summary>
        /// <remarks>
        /// Full value is: .693147180559945309417232121458176568075500134360255254120680009493393621969694715605863326996418687
        /// From: http://oeis.org/A002162/constant
        /// </remarks>
        private const decimal Ln2 = 0.6931471805599453094172321215m;

        /// <summary>
        /// Smallest non-zero decimal value.
        /// </summary>
        private const decimal SmallestNonZeroDec = 0.0000000000000000000000000001m; // aka new decimal(1, 0, 0, false, 28); //1e-28m

        // This table is required for the Round function which can specify the number of digits to round to
        private static readonly decimal[] roundPower10Decimal = new decimal[]
        {
            1E0m,  1E1m,  1E2m,  1E3m,  1E4m,  1E5m,  1E6m,  1E7m,  1E8m,  1E9m,
            1E10m, 1E11m, 1E12m, 1E13m, 1E14m, 1E15m, 1E16m, 1E17m, 1E18m, 1E19m,
            1E20m, 1E21m, 1E22m, 1E23m, 1E24m, 1E25m, 1E26m, 1E27m, 1E28m,
        };

        /// <summary>
        /// Returns the absolute value of a <see cref="decimal"/> number.
        /// </summary>
        /// <param name="value">A number that is greater than or equal to <see cref="decimal.MinValue"/>, but less than or equal to <see cref="decimal.MaxValue"/>.</param>
        /// <returns>A <see cref="decimal"/> number, x, such that 0 ≤ x ≤ <see cref="decimal.MaxValue"/>.</returns>
        /// <remarks>
        /// The absolute value of a <see cref="decimal"/> is its numeric value without its sign. For example, the absolute value of both 1.2 and -1.2 is 1.2.
        /// </remarks>
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Abs(decimal value)
        {
            if (value < 0)
            {
                value = -value;
                if (value < 0)
                {
                    AbsHelper();
                }
            }

            return value;
        }

        /// <summary>
        /// Returns the angle whose cosine is the specified number.
        /// </summary>
        /// <param name="z">A number representing a cosine, where -1 ≤d≤ 1.</param>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Inverse_trigonometric_function
        /// and http://mathworld.wolfram.com/InverseCosine.html
        /// </remarks>
        /// <returns></returns>
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Acos(decimal z)
        {
            if (z < -1 || z > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(z), "Argument must be in the range -1 to 1 inclusive.");
            }

            // Special cases
            if (z == -1)
            {
                return PI;
            }

            if (z == 0)
            {
                return PiHalf;
            }

            if (z == 1)
            {
                return 0;
            }

            return 2m * Atan(Sqrt(1 - (z * z)) / (1 + z));
        }

        public static decimal Acosh(decimal x) { throw null; }

        /// <summary>
        /// Returns the angle whose sine is the specified number.
        /// </summary>
        /// <param name="z">A number representing a sine, where -1 ≤d≤ 1.</param>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Inverse_trigonometric_function
        /// and http://mathworld.wolfram.com/InverseSine.html
        /// I originally used the Taylor series for ASin, but it was extremely slow
        /// around -1 and 1 (millions of iterations) and still ends up being less
        /// accurate than deriving from the ATan function.
        /// </remarks>
        /// <returns></returns>
        public static decimal Asin(decimal z)
        {
            if (z < -1 || z > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(z), "Argument must be in the range -1 to 1 inclusive.");
            }

            // Special cases
            if (z == -1)
            {
                return -PiHalf;
            }

            if (z == 0)
            {
                return 0;
            }

            if (z == 1)
            {
                return PiHalf;
            }

            return 2m * Atan(z / (1 + Sqrt(1 - (z * z))));
        }

        public static decimal Asinh(decimal x) { throw null; }

        /// <summary>
        /// Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="x">A number representing a tangent.</param>
        /// <remarks>
        /// See http://mathworld.wolfram.com/InverseTangent.html for faster converging 
        /// series from Euler that was used here.
        /// </remarks>
        /// <returns></returns>
        public static decimal Atan(decimal x)
        {
            // Special cases
            if (x == -1)
            {
                return -PiQuarter;
            }
            else if (x == 0)
            {
                return 0;
            }
            else if (x == 1)
            {
                return PiQuarter;
            }

            if (x < -1)
            {
                // Force down to -1 to 1 interval for faster convergence
                return -PiHalf - Atan(1 / x);
            }
            else if (x > 1)
            {
                // Force down to -1 to 1 interval for faster convergence
                return PiHalf - Atan(1 / x);
            }

            var result = 0m;
            var doubleIteration = 0; // current iteration * 2
            var y = (x * x) / (1 + (x * x));
            var nextAdd = 0m;

            while (true)
            {
                if (doubleIteration == 0)
                {
                    nextAdd = x / (1 + (x * x));  // is = y / x  but this is better for very small numbers where y = 9
                }
                else
                {
                    // We multiply by -1 each time so that the sign of the component
                    // changes each time. The first item is positive and it
                    // alternates back and forth after that.
                    // Following is equivalent to: nextAdd *= y * (iteration * 2) / (iteration * 2 + 1);
                    nextAdd *= y * doubleIteration / (doubleIteration + 1);
                }

                if (nextAdd == 0)
                {
                    break;
                }

                result += nextAdd;

                doubleIteration += 2;
            }

            return result;
        }

        /// <summary>
        /// Returns the angle whose tangent is the quotient of two specified numbers.
        /// </summary>
        /// <param name="y">The y coordinate of a point.</param>
        /// <param name="x">The x coordinate of a point.</param>
        /// <returns>
        /// An angle, θ, measured in radians, such that -π ≤ θ ≤ π, and tan(θ) = y / x,
        /// where (x, y) is a point in the Cartesian plane. Observe the following:
        /// For (x, y) in quadrant 1, 0 &lt; θ &lt; π/2.
        /// For (x, y) in quadrant 2, π/2 &lt; θ ≤ π.
        /// For (x, y) in quadrant 3, -π &lt; θ &lt; -π/2.
        /// For (x, y) in quadrant 4, -π/2 &lt; θ &lt; 0.
        /// </returns>
        public static decimal Atan2(decimal y, decimal x)
        {
            if (x == 0 && y == 0)
            {
                return 0;                   // X0, Y0
            }

            if (x == 0)
            {
                return y > 0
                           ? PiHalf         // X0, Y+
                           : -PiHalf;       // X0, Y-
            }

            if (y == 0)
            {
                return x > 0
                           ? 0              // X+, Y0
                           : PI;            // X-, Y0
            }

            var aTan = Atan(y / x);

            if (x > 0)
            {
                return aTan;         // Q1&4: X+, Y+-
            }

            return y > 0
                       ? aTan + PI          //   Q2: X-, Y+
                       : aTan - PI;         //   Q3: X-, Y-

        }

        public static decimal Atanh(decimal x) { throw null; }

        public static decimal Cbrt(decimal x) { throw null; }

#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Ceiling(decimal d)
        {
            return decimal.Ceiling(d);
        }

        /// <summary>
        /// Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="x">An angle, measured in radians.</param>
        /// <remarks>
        /// Uses a Taylor series to calculate sine. See 
        /// http://en.wikipedia.org/wiki/Trigonometric_functions for details.
        /// </remarks>
        /// <returns></returns>
        public static decimal Cos(decimal x)
        {
            // Normalize to between -2Pi <= x <= 2Pi
            x = Remainder(x, TwoPi);

            if (x == 0 || x == TwoPi)
            {
                return 1;
            }

            if (x == PI)
            {
                return -1;
            }

            if (x == PiHalf || x == PI + PiHalf)
            {
                return 0;
            }

            var result = 0m;
            var doubleIteration = 0; // current iteration * 2
            var xSquared = x * x;
            var nextAdd = 0m;

            while (true)
            {
                if (doubleIteration == 0)
                {
                    nextAdd = 1;
                }
                else
                {
                    // We multiply by -1 each time so that the sign of the component
                    // changes each time. The first item is positive and it
                    // alternates back and forth after that.
                    // Following is equivalent to: nextAdd *= -1 * x * x / ((2 * iteration - 1) * (2 * iteration));
                    nextAdd *= -1 * xSquared / ((doubleIteration * doubleIteration) - doubleIteration);
                }

                if (nextAdd == 0)
                {
                    break;
                }

                result += nextAdd;

                doubleIteration += 2;
            }

            return result;
        }

        public static decimal Cosh(decimal x) { throw null; }

        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="d">A number specifying a power.</param>
        /// <returns></returns>
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Exp(decimal d)
        {
            decimal result;
            decimal nextAdd;
            int iteration;
            bool reciprocal;
            decimal t;

            reciprocal = d < 0;
            d = Abs(d);

            t = decimal.Truncate(d);

            if (d == 0)
            {
                result = 1;
            }
            else if (d == 1)
            {
                result = E;
            }
            else if (Abs(d) > 1 && t != d)
            {
                // Split up into integer and fractional
                result = Exp(t) * Exp(d - t);
            }
            else if (d == t)
            {
                // Integer power
                result = ExpBySquaring(E, d);
            }
            else
            {
                // Fractional power < 1
                // See http://mathworld.wolfram.com/ExponentialFunction.html
                iteration = 0;
                nextAdd = 0;
                result = 0;

                while (true)
                {
                    if (iteration == 0)
                    {
                        nextAdd = 1;               // == Pow(d, 0) / Factorial(0) == 1 / 1 == 1
                    }
                    else
                    {
                        nextAdd *= d / iteration;  // == Pow(d, iteration) / Factorial(iteration)
                    }

                    if (nextAdd == 0)
                    {
                        break;
                    }

                    result += nextAdd;

                    iteration += 1;
                }
            }

            // Take reciprocal if this was a negative power
            // Note that result will never be zero at this point.
            if (reciprocal)
            {
                result = 1 / result;
            }

            return result;
        }

#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Floor(decimal d)
        {
            return decimal.Floor(d);
        }

        public static decimal IEEERemainder(decimal x, decimal y) { throw null; }

        /// <summary>
        /// Returns the natural (base e) logarithm of a specified number.
        /// </summary>
        /// <param name="d">A number whose logarithm is to be found.</param>
        /// <remarks>
        /// I'm still not satisfied with the speed. I tried several different
        /// algorithms that you can find in a historical version of this
        /// source file. The one I settled on was the best of mediocrity.
        /// </remarks>
        /// <returns></returns>
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Log(decimal d)
        {
            if (d < 0)
            {
                throw new ArgumentException("Natural logarithm is a complex number for values less than zero!", nameof(d));
            }

            if (d == 0)
            {
                throw new OverflowException("Natural logarithm is defined as negative infinity at zero which the Decimal data type can't represent!");
            }

            if (d == 1)
            {
                return 0;
            }

            if (d >= 1)
            {
                var power = 0m;

                var x = d;
                while (x > 1)
                {
                    x /= 10;
                    power += 1;
                }

                return Log(x) + (power * Ln10);
            }

            // See http://en.wikipedia.org/wiki/Natural_logarithm#Numerical_value
            // for more information on this faster-converging series.

            decimal y;
            decimal ySquared;

            var iteration = 0;
            var exponent = 0m;
            var nextAdd = 0m;
            var result = 0m;

            y = (d - 1) / (d + 1);
            ySquared = y * y;

            while (true)
            {
                if (iteration == 0)
                {
                    exponent = 2 * y;
                }
                else
                {
                    exponent *= ySquared;
                }

                nextAdd = exponent / ((2 * iteration) + 1);

                if (nextAdd == 0)
                {
                    break;
                }

                result += nextAdd;

                iteration += 1;
            }

            return result;
        }

        /// <summary>
        /// Returns the logarithm of a specified number in a specified base.
        /// </summary>
        /// <param name="d">A number whose logarithm is to be found.</param>
        /// <param name="newBase">The base of the logarithm.</param>
        /// <remarks>
        /// This is a relatively naive implementation that simply divides the
        /// natural log of <paramref name="d"/> by the natural log of the base.
        /// </remarks>
        /// <returns></returns>
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Log(decimal d, decimal newBase)
        {
            // Short circuit the checks below if d is 1 because
            // that will yield 0 in the numerator below and give us
            // 0 for any base, even ones that would yield infinity.
            if (d == 1)
            {
                return 0m;
            }

            if (newBase == 1)
            {
                throw new InvalidOperationException("Logarithm for base 1 is undefined.");
            }

            if (d < 0)
            {
                throw new ArgumentException("Logarithm is a complex number for values less than zero!", nameof(d));
            }

            if (d == 0)
            {
                throw new OverflowException("Logarithm is defined as negative infinity at zero which the Decimal data type can't represent!");
            }

            if (newBase < 0)
            {
                throw new ArgumentException("Logarithm base would be a complex number for values less than zero!", nameof(newBase));
            }

            if (newBase == 0)
            {
                throw new OverflowException("Logarithm base would be negative infinity at zero which the Decimal data type can't represent!");
            }

            return Log(d) / Log(newBase);
        }

        /// <summary>
        /// Returns the base 10 logarithm of a specified number.
        /// </summary>
        /// <param name="d">A number whose logarithm is to be found.</param>
        /// <returns></returns>
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Log10(decimal d)
        {
            if (d < 0)
            {
                throw new ArgumentException("Logarithm is a complex number for values less than zero!", nameof(d));
            }

            if (d == 0)
            {
                throw new OverflowException("Logarithm is defined as negative infinity at zero which the Decimal data type can't represent!");
            }

            return Log(d) / Ln10;
        }

        /// <summary>
        /// Returns the base 2 logarithm of a specified number.
        /// </summary>
        /// <param name="d">A number whose logarithm is to be found.</param>
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static decimal Log2(decimal d)
        {
            if (d < 0)
            {
                throw new ArgumentException("Logarithm is a complex number for values less than zero!", nameof(d));
            }

            if (d == 0)
            {
                throw new OverflowException("Logarithm is defined as negative infinity at zero which the Decimal data type can't represent!");
            }

            return Log(d) / Ln2;
        }

#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Max(decimal val1, decimal val2)
        {
            // return decimal.Max(ref val1, ref val2);
            //
            // Since decimal.Max() is inaccessable, perform the comparison
            // the old fashioned way.
            return (val1 >= val2) ? val1 : val2;
        }

#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Min(decimal val1, decimal val2)
        {
            // return decimal.Min(ref val1, ref val2);
            //
            // Since decimal.Min() is inaccessable, perform the comparison
            // the old fashioned way.
            return (val1 <= val2) ? val1 : val2;
        }

        /// <summary>
        /// Returns a specified number raised to the specified power.
        /// </summary>
        /// <param name="x">A number to be raised to a power.</param>
        /// <param name="y">A number that specifies a power.</param>
        /// <returns></returns>
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Pow(decimal x, decimal y)
        {
            decimal result;
            var isNegativeExponent = false;

            // Handle negative exponents
            if (y < 0)
            {
                isNegativeExponent = true;
                y = Abs(y);
            }

            if (y == 0)
            {
                result = 1;
            }
            else if (y == 1)
            {
                result = x;
            }
            else
            {
                var t = decimal.Truncate(y);

                if (y == t)
                {
                    // Integer powers
                    result = ExpBySquaring(x, y);
                }
                else
                {
                    // Fractional power < 1
                    // See http://en.wikipedia.org/wiki/Exponent#Real_powers
                    // The next line is an optimization of Exp(y * Log(x)) for better precision
                    result = ExpBySquaring(x, t) * Exp((y - t) * Log(x));
                }
            }

            if (isNegativeExponent)
            {
                // Note, for IEEE floats this would be Infinity and not an exception...
                if (result == 0)
                {
                    throw new OverflowException("Negative power of 0 is undefined!");
                }

                result = 1 / result;
            }

            return result;
        }

#if ROUND
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Round(decimal x)
        {
#if NETFRAMEWORK || NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
            return decimal.Round(x);
#else
            // Returns a binary representation of a Decimal. The return value is an
            // integer array with four elements. Elements 0, 1, and 2 contain the low,
            // middle, and high 32 bits of the 96-bit integer part of the Decimal.
            // Element 3 contains the scale factor and sign of the Decimal: bits 0-15
            // (the lower word) are unused; bits 16-23 contain a value between 0 and
            // 28, indicating the power of 10 to divide the 96-bit integer part by to
            // produce the Decimal value; bits 24-30 are unused; and finally bit 31
            // indicates the sign of the Decimal value, 0 meaning positive and 1
            // meaning negative.
            int[] bits = decimal.GetBits(x);
            int exponent = float.ExtractExponentFromBits(bits);

            if (exponent <= 0x7E)
            {
                if ((bits << 1) == 0)
                {
                    // Exactly +/- zero should return the original value
                    return x;
                }

                // Any value less than or equal to 0.5 will always round to exactly zero
                // and any value greater than 0.5 will always round to exactly one. However,
                // we need to preserve the original sign for IEEE compliance.

                float result = ((exponent == 0x7E) && (float.ExtractSignificandFromBits(bits) != 0)) ? 1.0f : 0.0f;
                return CopySign(result, x);
            }

            if (exponent >= 0x96)
            {
                // Any value greater than or equal to 2^23 cannot have a fractional part,
                // So it will always round to exactly itself.

                return x;
            }

            // The absolute value should be greater than or equal to 1.0 and less than 2^23
            Debug.Assert((0x7F <= exponent) && (exponent <= 0x95));

            // Determine the last bit that represents the integral portion of the value
            // and the bits representing the fractional portion

            uint lastBitMask = 1U << (0x96 - exponent);
            uint roundBitsMask = lastBitMask - 1;

            // Increment the first fractional bit, which represents the midpoint between
            // two integral values in the current window.

            bits += lastBitMask >> 1;

            if ((bits & roundBitsMask) == 0)
            {
                // If that overflowed and the rest of the fractional bits are zero
                // then we were exactly x.5 and we want to round to the even result

                bits &= ~lastBitMask;
            }
            else
            {
                // Otherwise, we just want to strip the fractional bits off, truncating
                // to the current integer value.

                bits &= ~roundBitsMask;
            }

            return BitConverter.Int32BitsToSingle((int)bits);
#endif
        }

#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Round(decimal x, int digits)
        {
#if NETFRAMEWORK || NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
            return decimal.Round(x, digits);
#else
            return Round(x, digits, MidpointRounding.ToEven);
#endif
        }

#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Round(decimal x, MidpointRounding mode)
        {
#if NETFRAMEWORK || NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
            return decimal.Round(x, mode);
#else
            return Round(x, 0, mode);
#endif
        }

#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Round(decimal x, int digits, MidpointRounding mode)
        {
#if NETFRAMEWORK || NETSTANDARD2_0 || NETSTANDARD2_1 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
            return decimal.Round(x, digits, mode);
#else
            if ((digits < 0) || (digits > maxRoundingDigits))
            {
                throw new ArgumentOutOfRangeException(nameof(digits), string.Format(SR.ArgumentOutOfRange_RoundingDigits, maxRoundingDigits));
            }

            if (mode < MidpointRounding.ToEven)
            {
                throw new ArgumentException(string.Format(SR.Argument_InvalidEnumValue, mode, nameof(MidpointRounding)), nameof(mode));
            }

            if (Abs(x) < decimalRoundLimit)
            {
                decimal power10 = roundPower10Decimal[digits];

                x *= power10;

                switch (mode)
                {
                    // Rounds to the nearest value; if the number falls midway,
                    // it is rounded to the nearest value with an even least significant digit
                    case MidpointRounding.ToEven:
                        {
                            x = Round(x);
                            break;
                        }

                    // Rounds to the nearest value; if the number falls midway,
                    // it is rounded to the nearest value above (for positive numbers) or below (for negative numbers)
                    case MidpointRounding.AwayFromZero:
                        {
                            decimal fraction = 5; // ModF(x, &x);

                            if (Abs(fraction) >= 0.5m)
                            {
                                x += Sign(fraction);
                            }

                            break;
                        }

                    default:
                        {
                            throw new ArgumentException(string.Format(SR.Argument_InvalidEnumValue, mode, nameof(MidpointRounding)), nameof(mode));
                        }
                }

                x /= power10;
            }

            return x;
#endif
        }
#endif // ROUND

#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static int Sign(decimal value)
        {
            // return decimal.Sign(ref value);
            //
            // Since decimal.Sign() is inaccessable, perform the comparison
            // the old fashioned way.
            if (value < 0)
            {
                return -1;
            }
            else if (value > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns the sine of the specified angle.
        /// </summary>
        /// <param name="x">An angle, measured in radians.</param>
        /// <remarks>
        /// Uses a Taylor series to calculate sine. See 
        /// http://en.wikipedia.org/wiki/Trigonometric_functions for details.
        /// </remarks>
        /// <returns></returns>
        public static decimal Sin(decimal x)
        {
            // Normalize to between -2Pi <= x <= 2Pi
            x = Remainder(x, TwoPi);

            if (x == 0 || x == PI || x == TwoPi)
            {
                return 0;
            }

            if (x == PiHalf)
            {
                return 1;
            }

            if (x == PI + PiHalf)
            {
                return -1;
            }

            var result = 0m;
            var doubleIteration = 0; // current iteration * 2
            var xSquared = x * x;
            var nextAdd = 0m;

            while (true)
            {
                if (doubleIteration == 0)
                {
                    nextAdd = x;
                }
                else
                {
                    // We multiply by -1 each time so that the sign of the component
                    // changes each time. The first item is positive and it
                    // alternates back and forth after that.
                    // Following is equivalent to: nextAdd *= -1 * x * x / ((2 * iteration) * (2 * iteration + 1));
                    nextAdd *= -1 * xSquared / ((doubleIteration * doubleIteration) + doubleIteration);
                }

                //Debug.WriteLine("{0:000}:{1,33:+0.0000000000000000000000000000;-0.0000000000000000000000000000} ->{2,33:+0.0000000000000000000000000000;-0.0000000000000000000000000000}",
                //    doubleIteration / 2, nextAdd, result + nextAdd);
                if (nextAdd == 0)
                {
                    break;
                }

                result += nextAdd;

                doubleIteration += 2;
            }

            return result;
        }

        public static decimal Sinh(decimal x) { throw null; }

        /// <summary>
        /// Returns the square root of a given number.
        /// </summary>
        /// <param name="s">A non-negative number.</param>
        /// <remarks>
        /// Uses an implementation of the "Babylonian Method".
        /// See http://en.wikipedia.org/wiki/Methods_of_computing_square_roots#Babylonian_method
        /// </remarks>
        /// <returns></returns>
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Sqrt(decimal s)
        {
            if (s < 0)
            {
                throw new ArgumentException("Square root not defined for Decimal data type when less than zero!", nameof(s));
            }

            // Prevent divide-by-zero errors below. Dividing either
            // of the numbers below will yield a recurring 0 value
            // for halfS eventually converging on zero.
            if (s == 0 || s == SmallestNonZeroDec)
            {
                return 0;
            }

            decimal x;
            var halfS = s / 2m;
            var lastX = -1m;
            decimal nextX;

            // Begin with an estimate for the square root.
            // Use hardware to get us there quickly.
            x = (decimal)Math.Sqrt(decimal.ToDouble(s));

            while (true)
            {
                nextX = (x / 2m) + (halfS / x);

                // The next check effectively sees if we've ran out of
                // precision for our data type.
                if (nextX == x || nextX == lastX)
                {
                    break;
                }

                lastX = x;
                x = nextX;
            }

            return nextX;
        }

        /// <summary>
        /// Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="radians">An angle, measured in radians.</param>
        /// <remarks>
        /// Uses a Taylor series to calculate sine. See 
        /// http://en.wikipedia.org/wiki/Trigonometric_functions for details.
        /// </remarks>
        /// <returns></returns>
        public static decimal Tan(decimal radians)
        {
            try
            {
                return Sin(radians) / Cos(radians);
            }
            catch (DivideByZeroException)
            {
                throw new Exception("Tangent is undefined at this angle!");
            }
        }

        public static decimal Tanh(decimal x) { throw null; }

#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static decimal Truncate(decimal d)
        {
            return decimal.Truncate(d);
        }

#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static void AbsHelper()
        {
            throw new OverflowException(SR.Overflow_NegateTwosCompNum);
        }

        /// <summary>
        /// Raises one number to an integral power.
        /// </summary>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Exponentiation_by_squaring
        /// </remarks>
#if !NET20 && !NET35 && !NET40
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private static decimal ExpBySquaring(decimal x, decimal y)
        {
            Debug.Assert(y >= 0 && decimal.Truncate(y) == y, "Only non-negative, integer powers supported.");
            if (y < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(y), "Negative exponents not supported!");
            }

            if (decimal.Truncate(y) != y)
            {
                throw new ArgumentException("Exponent must be an integer!", nameof(y));
            }

            var result = 1m;
            var multiplier = x;

            while (y > 0)
            {
                if ((y % 2) == 1)
                {
                    result *= multiplier;
                    y -= 1;
                    if (y == 0)
                    {
                        break;
                    }
                }

                multiplier *= multiplier;
                y /= 2;
            }

            return result;
        }

        /// <summary>
        /// Gets the number of decimal places in a decimal value.
        /// </summary>
        /// <remarks>
        /// Started with something found here: http://stackoverflow.com/a/6092298/856595
        /// </remarks>
        private static int GetDecimalPlaces(decimal dec, bool countTrailingZeros)
        {
            const int signMask = unchecked((int)0x80000000);
            const int scaleMask = 0x00FF0000;
            const int scaleShift = 16;

            int[] bits = decimal.GetBits(dec);
            var result = (bits[3] & scaleMask) >> scaleShift;  // extract exponent

            // Return immediately for values without a fractional portion or if we're counting trailing zeros
            if (countTrailingZeros || (result == 0))
            {
                return result;
            }

            // Get a raw version of the decimal's integer
            bits[3] = bits[3] & ~unchecked(signMask | scaleMask); // clear out exponent and negative bit
            var rawValue = new decimal(bits);

            // Account for trailing zeros
            while ((result > 0) && ((rawValue % 10) == 0))
            {
                result--;
                rawValue /= 10;
            }

            return result;
        }

        /// <summary>
        /// Gets the remainder of one number divided by another number in such a way as to retain maximum precision.
        /// </summary>
        private static decimal Remainder(decimal d1, decimal d2)
        {
            if (Abs(d1) < Abs(d2))
            {
                return d1;
            }

            var timesInto = decimal.Truncate(d1 / d2);
            var shiftingNumber = d2;
            var sign = Sign(d1);

            for (var i = 0; i <= GetDecimalPlaces(d2, true); i++)
            {
                // Note that first "digit" will be the integer portion of d2
                var digit = decimal.Truncate(shiftingNumber);

                d1 -= timesInto * (digit / roundPower10Decimal[i]);

                shiftingNumber = (shiftingNumber - digit) * 10m; // remove used digit and shift for next iteration
                if (shiftingNumber == 0m)
                {
                    break;
                }
            }

            // If we've crossed zero because of the precision mismatch, 
            // we need to add a whole d2 to get a correct result.
            if (d1 != 0 && Sign(d1) != sign)
            {
                d1 = Sign(d2) == sign
                         ? d1 + d2
                         : d1 - d2;
            }

            return d1;
        }
    }
}
