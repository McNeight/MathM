# System.Decimal

The Decimal data type can
represent values ranging from -79,228,162,514,264,337,593,543,950,335 to
79,228,162,514,264,337,593,543,950,335 with 28 significant digits. The
Decimal data type is ideally suited to financial calculations that
require a large number of significant digits and no round-off errors.

The finite set of values of type Decimal are of the form m
/ 10e, where m is an integer such that
-296 <; m <; 296, and e is an integer
between 0 and 28 inclusive.

Contrary to the float and double data types, decimal
fractional numbers such as 0.1 can be represented exactly in the
Decimal representation. In the float and double
representations, such numbers are often infinite fractions, making those
representations more prone to round-off errors.

The Decimal class implements widening conversions from the
ubyte, char, short, int, and long types
to Decimal. These widening conversions never loose any information
and never throw exceptions. The Decimal class also implements
narrowing conversions from Decimal to ubyte, char,
short, int, and long. These narrowing conversions round
the Decimal value towards zero to the nearest integer, and then
converts that integer to the destination type. An OverflowException
is thrown if the result is not within the range of the destination type.

The Decimal class provides a widening conversion from
Currency to Decimal. This widening conversion never loses any
information and never throws exceptions. The Currency class provides
a narrowing conversion from Decimal to Currency. This
narrowing conversion rounds the Decimal to four decimals and then
converts that number to a Currency. An OverflowException
is thrown if the result is not within the range of the Currency type.

The Decimal class provides narrowing conversions to and from the
float and double types. A conversion from Decimal to
float or double may loose precision, but will not loose
information about the overall magnitude of the numeric value, and will never
throw an exception. A conversion from float or double to
Decimal throws an OverflowException if the value is not within
the range of the Decimal type.
