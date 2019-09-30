# MathM
C# math library for supporting System.Decimal floating-point numberss.

The .NET Decimal data type is included in .NET, but is often overlooked for scientific calculations. It's high precision and exact up to 28 decimal places and it's available in any .NET environment.

You might be in a situation where you just need a lot more precision than Double can provide. I used Decimal for calculating locations in space for CNC manufacturing and pick and place control. I've found the increased precision of Decimal reduces overall errors throughout the set of calculations and it also improves the odds of reversing the calculations if necessary for debugging. In that case it ends up being a kind of oversampling.

Unfortunately, a lot of the usual number functionality is not provided for .NET. For example, you can't calculate a square root or even perform [exponentiation](http://stackoverflow.com/questions/6425501/is-there-a-math-api-for-powdecimal-decimal). You can cast to Double for these operations, but you can end up with a significant loss of precision.

## System.MathF

```cs
namespace System {
    public static class MathF {
        public const float E = 2.71828175f;
        public const float PI = 3.14159274f;
        public static float Abs(float x);
        public static float Acos(float x);
        public static float Acosh(float x);
        public static float Asin(float x);
        public static float Asinh(float x);
        public static float Atan(float x);
        public static float Atan2(float y, float x);
        public static float Atanh(float x);
        public static float Cbrt(float x);
        public static float Ceiling(float x);
        public static float Cos(float x);
        public static float Cosh(float x);
        public static float Exp(float x);
        public static float Floor(float x);
        public static float IEEERemainder(float x, float y);
        public static float Log(float x);
        public static float Log(float x, float y);
        public static float Log10(float x);
        public static float Max(float x, float y);
        public static float Min(float x, float y);
        public static float Pow(float x, float y);
        public static float Round(float x);
        public static float Round(float x, int digits);
        public static float Round(float x, int digits, MidpointRounding mode);
        public static float Round(float x, MidpointRounding mode);
        public static int Sign(float x);
        public static float Sin(float x);
        public static float Sinh(float x);
        public static float Sqrt(float x);
        public static float Tan(float x);
        public static float Tanh(float x);
        public static float Truncate(float x);
    }
}
```

## License

This project uses the MIT License. See the LICENSE file in the same folder as this README.
