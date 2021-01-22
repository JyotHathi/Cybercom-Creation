using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///< GenerateDocumentationFile > true </ GenerateDocumentationFile >
///<DocumentationFile>bin\$(AssemblyName).xml</DocumentationFile>
namespace Practice_1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*------------------------------Round,Floor,Ceil--------------*/
            //int roundValue = (int)Math.Round(0.5254);
            //int floorValue = (int)Math.Floor(0.5254);
            //int sealedValue = (int)Math.Ceiling(0.5254);
            //Console.WriteLine($"{roundValue} -- {floorValue} -- {sealedValue}");
            //Console.ReadLine();

            /*----------------------------Array Resize-------------------*/
            //int[] array = new int[0];
            //for(int i=0;i<5;i++)
            //{
            //    Array.Resize(ref array, array.Length + 1);
            //    array[array.Length-1] = i;
            //}
            //for (int i = 0; i < 5; i++)
            //{
            //    Console.WriteLine($"{array[i]}");
            //}
            //Console.ReadLine();
        }
    }

    /// <summary>
    /// XmlComments Class for Testing of XmlComments.
    /// </summary>
    /// <remarks>
    /// Class Contains IsComment method.
    /// </remarks>
    class XmlComments
    {

        /// <summary>
        /// To Check input Line Is Commented or Not.
        /// </summary>
        /// <returns>
        /// <para><c>true</c> If line is comment.</para>
        /// <para><c>false</c> If line is not comment.</para>
        /// </returns>
        public static bool IsComment(string input)
        {
            //logic
            return true;
        }

        /// <exception cref="System.IO.IOException">Exception Details (Optional But Not Visible)</exception>
        public static void MayThrowException(int a)
        {
            throw new Exception("Exception");
        }
        string line = "";
        /// <value>
        /// To Set or Get Value of non-static member <b>line</b> of XmlComment Class.
        /// </value>
        public string Line
        {
            get { return line; }
            set { line = value;}
        }
    }
    /*
    The main Math class
    Contains all methods for performing basic math functions
*/
    /// <summary>
    /// The main <c>Math</c> class.
    /// Contains all methods for performing basic math functions.
    /// </summary>
    public class Math
    {
        /// <summary>
        /// Adds two doubles and returns the result.
        /// </summary>
        /// <returns>
        /// The sum of two doubles.
        /// </returns>
        /// <exception cref="System.OverflowException">Thrown when one parameter is max
        /// and the other is greater than zero.</exception>
        /// See <see cref="Math.Add(int, int)"/> to add integers.
        /// <param name="a">A double precision number.</param>
        /// <param name="b">A double precision number.</param>
        public static double Add(double a, double b)
        {
            if ((a == double.MaxValue && b > 0) || (b == double.MaxValue && a > 0))
                throw new System.OverflowException();

            return a + b;
        }
    }
}

