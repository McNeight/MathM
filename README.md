# MathM

<table>
  <tr>
    <th style="text-align:center">Build Server</th>
    <th>Operating System</th>
    <th>Framework</th>
    <th style="text-align:center">Status</th>
  </tr>
  <tr>
    <td style="text-align:center" rowspan="5">AppVeyor</td>
    <td>Visual Studio 2019</td>
    <td>.NET Core SDK 3.0.100</td>
    <td style="text-align:center" rowspan="5"><a href="https://ci.appveyor.com/project/McNeight/MathM"><img src="https://ci.appveyor.com/api/projects/status/pw5vamdgg1xqbt1w?svg=true" alt="AppVeyor build status" /></a></td>
  </tr>
  <tr>
    <td>Visual Studio 2017</td>
    <td>.NET Core SDK 2.2.108</td>
  </tr>
  <tr>
    <td>Visual Studio 2015</td>
    <td></td>
  </tr>
  <tr>
    <td>Ubuntu 18.04.3 LTS (Bionic Beaver)</td>
    <td>.NET Core SDK 3.0.100</td>
  </tr>
  <tr>
    <td>Ubuntu 16.04.6 LTS (Xenial Xerus)</td>
    <td>.NET Core SDK 3.0.100</td>
  </tr>
  <tr>
    <td style="text-align:center" rowspan="2">Travis</td>
    <td>Mac OS X 10.13.3</td>
    <td rowspan="2">.NET Core SDK 2.1.802</td>
    <td style="text-align:center" rowspan="2"><a href="https://travis-ci.org/McNeight/MathM"><img src="https://travis-ci.org/McNeight/MathM.svg?branch=master" alt="Travis build status" /></a></td>
  </tr>
  <tr>
    <td>Ubuntu 16.04.6 LTS (Xenial Xerus)</td>
  </tr>
</table>

C# math library for supporting System.Decimal floating-point numberss.

The .NET Decimal data type is included in .NET, but is often overlooked for scientific calculations. It's high precision and exact up to 28 decimal places and it's available in any .NET environment.

You might be in a situation where you just need a lot more precision than Double can provide. I used Decimal for calculating locations in space for CNC manufacturing and pick and place control. I've found the increased precision of Decimal reduces overall errors throughout the set of calculations and it also improves the odds of reversing the calculations if necessary for debugging. In that case it ends up being a kind of oversampling.

Unfortunately, a lot of the usual number functionality is not provided for .NET. For example, you can't calculate a square root or even perform [exponentiation](http://stackoverflow.com/questions/6425501/is-there-a-math-api-for-powdecimal-decimal). You can cast to Double for these operations, but you can end up with a significant loss of precision.

## McNeight.MathM vs. System.MathF

<table border="0">
  <tr>
    <th style="text-align:center">McNeight.MathM</th>
    <th style="text-align:center">System.MathF</th>
  </tr>
  <tr>
    <td style="text-align:left">
      <div>
```cs
namespace McNeight {
    public static class MathM {
        public const decimal E = 2.7182818284590452353602874714m;
        public const decimal PI = 3.1415926535897932384626433833m;
        public static decimal Abs(decimal m);
        public static decimal Acos(decimal m);
        // public static decimal Acosh(decimal m);
        public static decimal Asin(decimal m);
        // public static decimal Asinh(decimal m);
        public static decimal Atan(decimal m);
        public static decimal Atan2(decimal y, decimal x);
        // public static decimal Atanh(decimal m);
        // public static decimal Cbrt(decimal m);
        public static decimal Ceiling(decimal m);
        public static decimal Cos(decimal m);
        // public static decimal Cosh(decimal m);
        public static decimal Exp(decimal m);
        public static decimal Floor(decimal m);
        // public static decimal IEEERemainder(decimal x, decimal y);
        public static decimal Log(decimal m);
        public static decimal Log(decimal m, decimal newBase);
        public static decimal Log10(decimal m);
        public static decimal Max(decimal m1, decimal m2);
        public static decimal Min(decimal m1, decimal m2);
        public static decimal Pow(decimal x, decimal y);
        // public static decimal Round(decimal m);
        // public static decimal Round(decimal m, int digits);
        // public static decimal Round(decimal m, int digits, MidpointRounding mode);
        // public static decimal Round(decimal m, MidpointRounding mode);
        public static int Sign(decimal m);
        public static decimal Sin(decimal m);
        // public static decimal Sinh(decimal m);
        public static decimal Sqrt(decimal m);
        public static decimal Tan(decimal m);
        // public static decimal Tanh(decimal m);
        public static decimal Truncate(decimal m);
    }
}
```
      </div>
    </td>
    <td style="text-align:left">
      <div>
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
      </div>
    </td>
  </tr>
</table>

## License

This project uses the MIT License. See the LICENSE file in the same folder as this README.
