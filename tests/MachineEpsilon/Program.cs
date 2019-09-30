using System;

namespace MachineEpsilon
{
    /// <summary>
    /// 
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// https://gist.github.com/AndrewBarfield/2557034
        /// </remarks>
        private static void Main()
        {
            // Print Banner
            Console.WriteLine(
                "\nUNCW - CSC 340 - Scientific Computing\n" +
                "Machine Epsilon Homework\nAndrew M. Barfield\n" +
                DateTime.Now.ToLongDateString() + "\n" +
                DateTime.Now.ToLongTimeString() +
                "\n\n");

            // Functions that calclulate and print epsilon
            CalculateMachineEpsilonForFloat();
            Console.WriteLine("\n");
            CalculateMachineEpsilonForDouble();
            Console.WriteLine("\n");
            CalculateMachineEpsilonForDecimal();

            // Wait for key press
            Console.Read();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CalculateMachineEpsilonForFloat()
        {
            Console.WriteLine("Float:");

            var machineEpsilon = 1.0f;
            var x = 0.0f;
            var loopCount = 0;

            do
            {
                machineEpsilon /= 2.0f;
                x = 1.0f + machineEpsilon;
                loopCount++;
                Console.WriteLine("\t" + loopCount.ToString("00") + ") " + machineEpsilon.ToString());
            }
            while (x > 1.0);

            Console.WriteLine("\n\tMantissa Bit Count: " + loopCount);
            Console.WriteLine("\tMachine epsilon for float: " + (2 * machineEpsilon));
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CalculateMachineEpsilonForDouble()
        {
            Console.WriteLine("Double:");

            var machineEpsilon = 1.0;
            var x = 0.0;
            var loopCount = 0;

            do
            {
                machineEpsilon /= 2.0;
                x = 1.0 + machineEpsilon;
                loopCount++;
                Console.WriteLine("\t" + loopCount.ToString("00") + ") " + machineEpsilon.ToString());
            }
            while (x > 1.0);

            Console.WriteLine("\n\tMantissa Bit Count: " + loopCount);
            Console.WriteLine("\tMachine epsilon for double: " + (2 * machineEpsilon));
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CalculateMachineEpsilonForDecimal()
        {
            Console.WriteLine("Decimal:");

            var machineEpsilon = 1.0m;
            var x = 0.0m;
            var loopCount = 0;

            do
            {
                machineEpsilon /= 2.0m;
                x = 1.0m + machineEpsilon;
                loopCount++;
                Console.WriteLine("\t" + loopCount.ToString("00") + ") " + machineEpsilon.ToString());
            }
            while (x > 1.0m);

            Console.WriteLine("\n\tMantissa Bit Count: " + loopCount);
            Console.WriteLine("\tMachine epsilon for decimal: " + (2 * machineEpsilon));
        }
    }
}
