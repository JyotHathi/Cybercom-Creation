using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace FinalTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            byte userCodeChoice = 0;
            try
            {
                do
                {
                    userCodeChoice = UserChoiceInput();
                    switch (userCodeChoice)
                    {
                        //1. Palindrome String Checking
                        case 1:
                            Console.WriteLine("Please Enter a Word");
                            try
                            {
                                Palindrome.Word = Console.ReadLine();
                                if (Palindrome.IsPalindrome())
                                    Console.WriteLine("String is Palindrome");
                                else
                                    Console.WriteLine("String is Not Palidrome");

                            }
                            catch (InvalidInputException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            break;

                        // Text Input To Numeric Input
                        case 2:
                            TextInput textInput = new NumericInput();
                            Console.WriteLine("Please Enter Input Text");
                            Regex regexText = new Regex(@"^\w+$");
                            string word = Console.ReadLine();
                            if (regexText.IsMatch(word))
                            {
                                foreach (char c in word.ToCharArray())
                                {
                                    textInput.Add(c);
                                }
                                Console.WriteLine($"Your TextInput: {word} => Numeric Input: {textInput.GetValue()}");
                            }
                            else
                            {
                                Console.WriteLine("Text Input Must Contains Charcters and Numbers Only");
                            }
                            break;

                        // Count the number of 1's  Given Number
                        case 3:
                            Console.WriteLine("Please Enter Number");
                            ulong numberValue;
                            if (ulong.TryParse(Console.ReadLine(), out numberValue))
                            {
                                Console.WriteLine($"Number of 1's in Number {numberValue} is {OnesInNumber(numberValue)}");
                            }
                            else
                            {
                                Console.WriteLine($"Please Enter Number Only and in range {ulong.MinValue} - {ulong.MaxValue}");
                            }
                            break;

                        // 4.ATM Transaction Application: Already Devloped Individual Application 
                        case 4:
                            Console.WriteLine("ATM Transaction Is Already Developed as " +
                         "Individual Application");
                            break;

                        //Binary Triangle
                        case 5:
                            Console.WriteLine("Please Enter Number of Rows");
                            byte rows;
                            if (byte.TryParse(Console.ReadLine(), out rows))
                            {
                                BinaryTriangle(rows);
                            }
                            else
                            {
                                Console.WriteLine($"Please Enter Number Only and in range {ulong.MinValue} - {ulong.MaxValue}");
                            }
                            break;

                        // Find Smallest in the Matrix
                        case 6:
                            int row, col, i, j;
                            bool isChecked = false;
                            // Taking Numbers of Rows
                            do
                            {
                                Console.WriteLine("Enter Row Count");
                                if (int.TryParse(Console.ReadLine(), out row))
                                {
                                    isChecked = true;
                                }
                                else
                                {
                                    Console.WriteLine("Enter Valid Numeric Value");
                                }
                            } while (!isChecked);

                            isChecked = false;

                            // Taking Number of Columns
                            do
                            {
                                Console.WriteLine("Enter Column Count");
                                if (int.TryParse(Console.ReadLine(), out col))
                                {
                                    isChecked = true;
                                }
                                else
                                {
                                    Console.WriteLine("Enter Valid Numeric Value");
                                }
                            } while (!isChecked);

                            // Taking Array
                            int[,] matrics = new int[row, col];
                            for (i = 0; i < row; i++)
                            {
                                for (j = 0; j < col; j++)
                                {
                                    isChecked = false;
                                    do
                                    {
                                        Console.WriteLine($"Enter Element at [{i}][{j}]");
                                        isChecked = int.TryParse(Console.ReadLine(), out matrics[i, j]) ? true : false;
                                    } while (!isChecked);

                                }
                            }

                            Console.WriteLine($"Smallest Element is : {SmallestInMatrix(matrics, row, col)}");
                            break;

                        // Celsius to Fahrenheit Conversion
                        case 7:
                            Console.WriteLine("Please Enter Tempreture in Celsius");
                            float temp;
                            if (float.TryParse(Console.ReadLine(), out temp))
                            {
                                Console.WriteLine($"{temp} Celsius = {CelsiustoFahrenheit(temp)} Fahrenheit");
                            }
                            else
                            {
                                Console.WriteLine($"Please Enter Numeric/Floating Point value Only and in range {byte.MinValue} - {byte.MaxValue}");
                            }
                            break;

                        //Reverse the array using Predefine Function
                        case 8:
                            ReverseTheArray();
                            break;

                        // C# Program for Multilevel Inheritance
                        case 9:
                            Console.WriteLine("For Multilvel Inheritance Refer Class Region of FinalTest1" +
                         "NameSpce in that Region Code Number 9,10:.... and \nIn that" +
                         "Grandfather is Base of Father and Father is base of Son which Show Multilevel Inheritance");
                            break;

                        // C# Program For Multiple Inheritance including Virtual methods
                        case 10:
                            Console.WriteLine("For Multilvel Inheritance Refer Class Region of FinalTest1" +
                        "NameSpce in that Region Code Number 9,10:.... and \nIn that" +
                        "Grandfather is Base of Father and Father is base of Son which Show Multilevel Inheritance" +
                        "and GrandFather has Virtual Method Print Details Which is Overrided By Both Father and Son" +
                        "\n\nEx GrandFather gfather =new Son(\"live:cid.....\",\"9408244998\",\"370110\",18,\"Jyot Hathi\");\n" +
                        "Then Print Deatils");
                            GrandFather gfather = new Son("live:cid.....", "9408244998", "370110", 21, "Jyot Hathi");
                            gfather.PrintDetails();
                            break;

                        // Code Number 11:Output of Snipet Code
                        case 11:
                            Console.WriteLine("Code Snippet is given to Answer\n\nstatic void Main(string[] args){\n" +
                        "\tfloat i = 1.0f, j = 0.05f;\n" +
                        "\tdo\n\t{" +
                        "Console.WriteLine(i++ - ++j);\n\t} while (i < 2.0f && j <= 2.0f);\n\tConsole.ReadLine();\n}" +
                        "\n\n Output Of This Is 1.0f - 1.05f= -0.04999995f");
                            break;

                        // 2D Array to 1D array
                        case 12:
                            TwoDtoOneDArray();
                            break;

                        // Upper Bound and Lower Bound Of An array
                        case 13:
                            UpperLowerArray();
                            break;

                        // Divide By zero
                        case 14:
                            Console.WriteLine("For Code of DevideByZero Refer Program.DivisionOperation() in FinalTest1 " +
                                "NameSpace\nEx:\n");
                            DivisionOperation();
                            break;

                        // Bubble Short
                        case 15:
                            int numOfElements;
                            bool isDone = false;
                            // Taking Numbers of Elements
                            do
                            {
                                Console.WriteLine("Enter Number of Elements");
                                if (int.TryParse(Console.ReadLine(), out numOfElements))
                                {
                                    isDone = true;
                                }
                                else
                                {
                                    Console.WriteLine("Enter Valid Numeric Value");
                                }
                            } while (!isDone);

                            isDone = false;

                            // Taking Array
                            int[] elements = new int[numOfElements];
                            for (int k = 0; k < numOfElements; k++)
                            {
                                isDone = false;
                                do
                                {
                                    Console.WriteLine($"Enter Element at [{k}]");
                                    isDone = int.TryParse(Console.ReadLine(), out elements[k]) ? true : false;
                                } while (!isDone);

                            }
                            BubbleShort(ref elements);
                            Console.WriteLine("\nSorted Array:");
                            foreach (int element in elements)
                            {
                                Console.Write($"{element}, ");
                            }
                            break;

                        // User define Exception
                        case 16:
                            Console.WriteLine("InvalidInputException is User Defined Exception\n" +
                        "For Code Check In FinalTest1 NameSpace, Region Classes in That Class InvalidInputException");
                            break;

                        // Percentage Value of Given Number using Goto Statement
                        case 17:
                            byte isEnetry = 0; bool isValid = false;
                            int total, number;
                        Enetry:
                            do
                            {
                                Console.WriteLine("Enter Total value");

                                isValid = int.TryParse(Console.ReadLine(), out total);
                            } while (!isValid); isValid = false;
                            do
                            {
                                Console.WriteLine("Enter Total Number");

                                isValid = int.TryParse(Console.ReadLine(), out number);
                            } while (!isValid); isValid = false;
                            Console.WriteLine($"Result :{PercentageOfNumbers(total, number)}%");
                        Valid:

                            Console.WriteLine("Enter 1.Again\t2.Quit");
                            isValid = byte.TryParse(Console.ReadLine(), out isEnetry);
                            if (isValid && (isEnetry == 1 || isEnetry == 2))
                            {
                                if (isEnetry == 1)
                                    goto Enetry;
                                else
                                    break;
                            }
                            else
                                goto Valid;

                        //File Handling
                        case 18:
                            ReadWriteFile("I am Jyot Hathi Editing This File");
                            break;

                        // Constructor OverLoading
                        case 19:
                            Console.WriteLine("For Constructor OverLoading Refer FinalTest1 NameSpaces's Region Class" +
                        " and Exception and In That Region Code Number 19: Overloading Of Constructor\n" +
                        "In That Class Product Have Three Constructors 1. Default 2.Parameterised (2 Paras) " +
                        "3.Parameterised (4 Paras)");
                            break;

                        // Nth Number of Fibonacic Series
                        case 20:
                            bool isCorrect = false;
                            int term;
                            do
                            {
                                Console.WriteLine("Enter Term");
                                if (int.TryParse(Console.ReadLine(), out term))
                                    isCorrect = true;
                                else
                                    Console.WriteLine("Please Enter Proper numeric Value");
                            } while (!isCorrect);
                            Console.WriteLine($"{term} Term is {Fibonacci(term-1)}");
                            break;

                        // To Exit
                        case 21:
                            Console.WriteLine("You Quit");
                            break;
                    }
                    Console.WriteLine("\n-----------------------------------------------------------------\n");
                } while (userCodeChoice != 21);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Terminating Application");
            }
            Console.ReadLine();
        }
        #region Method

        // ---To Take Choice From User
        /// <summary>To Take Choice of Code Number as Input</summary>
        /// <returns><code>byte:</code> User Choice</returns>
        /// <remarks>Return Value Is in Range 1-21<br/>1-20:Code umbers<br />21.To Exit</remarks>
        public static byte UserChoiceInput()
        {
            byte userCodeChoice = 0;
            bool Ischoiced;
            do
            {
                Console.WriteLine("\nPlease Choice Code Number:\n--------------------------------\n" +
                    "1.Palindrome String\n" +
                    "2.TextValue to NumricTextValue\n" +
                    "3.Count 1's in Number\n" +
                    "4.ATM Transaction" +
                    "5.Binary Triangle\n" +
                    "6.Smallest Element Of Matrix\n" +
                    "7.CleciusToFahrenheit\n" +
                    "8.Reverse The Array\n" +
                    "9.Multilevel Inheritance\n" +
                    "10.Multilevel Inheritance with Virtual Methods\n" +
                    "11.Output Of Code\n" +
                    "12.2D Array to 1DArray\n" +
                    "13.Lower bound and Upper Bound Of An Array\n" +
                    "14.DivideByZero Exception\n" +
                    "15.Bubble Short\n" +
                    "16.UserDefine Exception\n" +
                    "17.Percentage Value of Number\n" +
                    "18.File Handling\n" +
                    "19.Constructor OverLoading\n" +
                    "20.nth of Fibonacic Series\n" +
                    "21.Exit\n");
                Ischoiced = byte.TryParse(Console.ReadLine(), out userCodeChoice);
                if (Ischoiced)
                {
                    if (userCodeChoice <= 0 || userCodeChoice > 21)
                    {
                        Console.WriteLine("Please Enter Valid Choice");
                    }
                }
                else
                {
                    Console.WriteLine("Please Enter Valid Input");
                }
            } while (!Ischoiced);
            return userCodeChoice;
        }

        // -- Code Number 3:Count the number of 1's in given number
        /// <summary>
        /// Count the number of 1's in given number
        /// </summary>
        /// <param name="numberValue"></param>
        /// <returns>Number of 1's</returns>
        public static int OnesInNumber(ulong numberValue)
        {
            int number, counter = 0;
            while (numberValue != 0)
            {
                number = (int)(numberValue % 10);
                if (number == 1)
                {
                    counter++;
                }
                numberValue /= 10;
            }
            return counter;
        }

        // --Code Number 5: BinaryTriangle
        /// <summary>
        /// To Print Binary Triangele
        /// </summary>
        /// <param name="rows">How Many Rows Wnat to print in Triangle</param>
        public static void BinaryTriangle(byte rows)
        {
            int flag = 1;
            for (byte i = 0; i < rows; i++)
            {
                for (byte j = 0; j < i + 1; j++)
                {
                    if (flag == 1)
                    {
                        Console.Write(flag);
                        flag = 0;
                    }
                    else
                    {
                        Console.Write(flag);
                        flag = 1;
                    }
                }
                Console.WriteLine("");
            }
        }

        // -- Code Number 6: Find Smallest Element is Matrix
        /// <summary>
        /// To Find Minimum Element of the matrics
        /// </summary>
        /// <param name="matrics">Matrics(2D)</param>
        /// <param name="rows">Number Of Rows In Matrics</param>
        /// <param name="cols">Number Of Cols In Matrics</param>
        /// <returns></returns>
        public static int SmallestInMatrix(int[,] matrics, int rows, int cols)
        {
            int minimum = matrics[0, 0];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    minimum = minimum > matrics[i, j] ? matrics[i, j] : minimum;
                }
            }
            return minimum;
        }

        // -- Code Number 7:Celsius to Fahrenheit Conversion
        /// <summary>
        /// Celsius to Fahrenheit Conversion
        /// </summary>
        /// <param name="tempreture">Tempreture in Celsius</param>
        /// <returns>Tempreture in Fahrenheit</returns>
        public static float CelsiustoFahrenheit(float tempreture)
        {
            return (((9f / 5f) * tempreture) + 32);
        }

        // -- Code Number 8:Reverse the array using Predefine Function
        /// <summary>
        /// Reverse the array using Predefine Function
        /// </summary>
        public static void ReverseTheArray()
        {
            int numOfElements;
            bool isDone = false;
            // Taking Numbers of Elements
            do
            {
                Console.WriteLine("Enter Number of Elements");
                if (int.TryParse(Console.ReadLine(), out numOfElements))
                {
                    isDone = true;
                }
                else
                {
                    Console.WriteLine("Enter Valid Numeric Value");
                }
            } while (!isDone);

            isDone = false;

            // Taking Array
            int[] elements = new int[numOfElements];
            for (int i = 0; i < numOfElements; i++)
            {
                isDone = false;
                do
                {
                    Console.WriteLine($"Enter Element at [{i}]");
                    isDone = int.TryParse(Console.ReadLine(), out elements[i]) ? true : false;
                } while (!isDone);

            }

            Array.Reverse(elements, 0, elements.Length);
            Console.WriteLine("\nReversed Array:");
            foreach (int element in elements)
            {
                Console.WriteLine(element);
            }
        }

        // -- Code Number 12: 2D array to One 1D Array
        /// <summary>
        /// 2D array to One 1D Array
        /// </summary>
        public static void TwoDtoOneDArray()
        {
            int row, col, i, j;
            Console.WriteLine("Taking 2D Array:");
            bool isChecked = false;
            // Taking Numbers of Rows
            do
            {
                Console.WriteLine("Enter Row Count");
                if (int.TryParse(Console.ReadLine(), out row))
                {
                    isChecked = true;
                }
                else
                {
                    Console.WriteLine("Enter Valid Numeric Value");
                }
            } while (!isChecked);

            isChecked = false;

            // Taking Number of Columns
            do
            {
                Console.WriteLine("Enter Column Count");
                if (int.TryParse(Console.ReadLine(), out col))
                {
                    isChecked = true;
                }
                else
                {
                    Console.WriteLine("Enter Valid Numeric Value");
                }
            } while (!isChecked);

            // Taking Array
            int[,] int2DArray = new int[row, col];
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < col; j++)
                {
                    isChecked = false;
                    do
                    {
                        Console.WriteLine($"Enter Element at [{i}][{j}]");
                        isChecked = int.TryParse(Console.ReadLine(), out int2DArray[i, j]) ? true : false;
                    } while (!isChecked);

                }
            }


            //Converting in Single D Array
            int[] int1DArray = new int[row * col];
            int k = 0;
            for (i = 0; i < row; i++)
            {
                for (j = 0; j < col; j++)
                {
                    int1DArray[k++] = int2DArray[i, j];
                }
            }

            Console.WriteLine("Your 1D Array:");
            // Printing an array 
            foreach (int element in int1DArray)
            {
                Console.Write(element + ",");
            }


        }

        // -- Code Number 13:Get Upper Bound and Lower Bound Of An Array 
        /// <summary>
        /// Demostration of Upper Bound and Lower Bound of an array
        /// </summary>
        public static void UpperLowerArray()
        {
            Console.WriteLine("Please Enter Charcter Array(String)");
            char[] charArray = Console.ReadLine().ToCharArray();
            Console.WriteLine($"Lower Bound of An Array is {charArray.GetLowerBound(0)}\n" +
                $"Upper Bound Of An array is {charArray.GetUpperBound(0)}");
        }

        // --- Code Number 14: To Explain Divide By Zero Exception
        /// <summary>
        /// Demostration Of Devide By Zero
        /// </summary>
        public static void DivisionOperation()
        {
            int num1, num2 = 0;
            bool IsChecked = false;
            do
            {
                Console.WriteLine("Please Enter Number:1");
                IsChecked = int.TryParse(Console.ReadLine(), out num1);
                if (IsChecked)
                {
                    Console.WriteLine("Now Lets Assump Second Numer is Zero and perform Division with" +
                        "second Number as denomenator");
                }
                else
                {
                    Console.WriteLine("Please Enter Number Properly");
                }
            } while (!IsChecked);
            try
            {
                Console.WriteLine(num1 / num2);
            }
            catch (DivideByZeroException dex)
            {
                Console.WriteLine(dex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // -- Code Number 15: Bubble Short
        /// <summary>
        /// Function To Perform Bubble Short Algorithm
        /// </summary>
        /// <param name="elements"></param>
        public static void BubbleShort(ref int[] elements)
        {
            int i, j, temp;
            for (i = 0; i < elements.Length; i++)
            {
                for (j = 0; j < elements.Length - 1; j++)
                {
                    if (elements[j] > elements[j + 1])
                    {
                        temp = elements[j];
                        elements[j] = elements[j + 1];
                        elements[j + 1] = temp;
                    }
                }
            }
        }

        // --Code Number 17: Calculate Percentage of number
        /// <summary>
        /// To Calculate Percentage 
        /// </summary>
        /// <param name="total"></param>
        /// <param name="num"></param>
        /// <returns>Percentage</returns>
        public static float PercentageOfNumbers(int total,int num)
        {
            return ((100f * num) / total);
        }

        // --Code Number 18:File Reading ANd Writing
        /// <summary>
        /// Function Demonstrate File Reading Writing Using FileStream,StreamReader, StreamWritter,
        /// TextReader and TextWritter.
        /// </summary>
        /// <param name="message"></param>
        public static void ReadWriteFile(string message)
        {
            FileStream fileStream;
            FileStream fileStream2;
            StreamReader streamReader;
            StreamWriter streamWriter;
            TextReader textReader;
            TextWriter textWriter;
            try
            {
                //---- Reading Using File Stream and StramReader Classes
                fileStream = new FileStream(@"D:\csharp\example.txt",FileMode.OpenOrCreate);
                Console.WriteLine("Reading Date Form D:\\csharp\\example.txt using StreamReader");
                streamReader = new StreamReader(fileStream);
                string text;
                while((text=streamReader.ReadLine())!=null)
                {
                    Console.WriteLine(text);
                }
                streamReader.Close();
                fileStream.Close();

                string message1 = message + "using StreamWritter";
                //---- Writting Using File Stream and StramReader Classes
                fileStream2 = new FileStream(@"D:\csharp\example.txt", FileMode.Append);
                Console.WriteLine($"Writting \"{message1}\" in D:\\csharp\\example.txt");
                streamWriter = new StreamWriter(fileStream2);
                streamWriter.WriteLine(message);
                streamWriter.Close();
                fileStream2.Close();


                //---- Reading Using File Stream and TextReader Classes
                fileStream = new FileStream(@"D:\csharp\example.txt", FileMode.OpenOrCreate);
                Console.WriteLine("\nReading Date Form D:\\csharp\\example.txt using TextReader");
                textReader = new StreamReader(fileStream);
                while ((text = textReader.ReadLine()) != null)
                {
                    Console.WriteLine(text);
                }
                textReader.Close();
                fileStream.Close();

                string message2 = message + "using TextWritter";
                //---- Writting Using FileStream and TextWritter Classes
                fileStream2 = new FileStream(@"D:\csharp\example.txt", FileMode.Append);
                Console.WriteLine($"Writting \"{message2}\" in D:\\csharp\\example.txt");
                textWriter = new StreamWriter(fileStream2);
                streamWriter.WriteLine(message);
                streamWriter.Close();
                textWriter.Close();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        // --Code Number 20:Nth Teram of Fibonacci using recursive Function
        /// <summary>
        /// Nth Terms of Fibonacci Series Using Recursive Function
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public static int Fibonacci(int term)
        {
            if (term <= 1)
                return 1;
            else
                return Fibonacci(term - 1) + Fibonacci(term - 2);
        }
        #endregion
    }

    #region Structures and Interfaces
    // Code Number 1:Structure For Palindrome checking
    /// <summary>
    /// Structure For Palindrome Checking
    /// </summary>
    /// <remarks>
    /// Contain Two Different Methods two Check String is Palidrome or Not
    /// </remarks>
    /// <exception cref="InvalidInputException">InvalidInputException</exception>
    struct Palindrome
    {
        static string word;
        static Palindrome()
        {
            word = "";
        }
        public static string Word
        {
            get { return word; }
            set
            {
                Regex wordRegex = new Regex(@"^\w+$");
                if (!string.IsNullOrEmpty(value) && wordRegex.IsMatch(value))
                {
                    word = value;
                }
                else
                {
                    throw new InvalidInputException("Please Enter Valid Word contain Alphabets and Numbers Only");
                }
            }
        }

        ///<summary>To Check Given Input String is Palindrome or Not</summary>
        ///<returns><code>true:</code>String Is Palindrome<code>false:</code>string is not Palindrome</returns>
        public static bool IsPalindrome(string wordVal)
        {
            char[] revOfWord = wordVal.ToUpper().ToCharArray();
            Array.Reverse(revOfWord);
            if (wordVal.ToUpper().Equals(revOfWord))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ///<summary>To Check Word is Palindrome or Not (Default Value of Word is "")</summary>
        ///<remarks>Here Word is Static Member of Structure</remarks>
        ///<returns><code>true:</code>If Is Palindrome<code>false:</code>If not Palindrome</returns>
        public static bool IsPalindrome()
        {
            string revOfResult = "";
            for (int i = 1; i <= word.Length; i++)
            {
                revOfResult += word[word.Length - i];
            }
            if (revOfResult.ToUpper().Equals(word.ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    #endregion

    // Contains Exception and Clases
    #region Classes
    
    // --Code Number 2:TextInput to Numeric Input
    #region CodeNumber 2: TextInput To Numeric Input
    class TextInput
    {
        protected static StringBuilder textInput = new StringBuilder();

        //To get the Value of String
        /// <summary>
        /// To Get Value of String
        /// </summary>
        /// <returns><code>string</code>String of Added characters</returns>
        public string GetValue()
        {
            return textInput.ToString();
        }

        //To Add the Value in String
        /// <summary>
        /// To Add The character in string/Text Input
        /// </summary>
        /// <param name="c">Input char</param>
        public virtual void Add(char c)
        {
            textInput.Append(c);
        }
    }
    class NumericInput : TextInput
    {
        public override void Add(char c)
        {
            if (Char.IsDigit(c))
            {
                textInput.Append(c);
            }
        }
    }
    #endregion

    
    // --Code Number 9,10 : MultiLevel Inheritance and Virtual Methods in multilvel Inhertitance
    #region Code Number 9,10 : Multilvel Inheritance and Virtual Methods

    // Class Grandfather Base of Father
    /// <summary>
    /// GrandFather contains Basic Details as Per Need Name, Age, Postal Code, 
    /// and virtual PrintDetails Method
    /// </summary>
    class GrandFather
    {
        string name;
        byte age;
        string postalCode;
        public GrandFather(string postalcode, byte age, string name)
        {
            this.Name = name;
            this.Age = age;
            this.PostalCode = postalcode;
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    name = value;
                }
                else
                {
                    throw new InvalidInputException("Invalid Input Name");
                }
            }
        }
        public byte Age
        {
            get { return age; }
            set
            {
                if (value > 0 && value < 100)
                {
                    age = value;
                }
                else
                {
                    throw new InvalidInputException("Age Value is Not Valid");
                }
            }
        }
        public string PostalCode
        {
            get { return postalCode; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    postalCode = value;
                }
                else
                {
                    throw new InvalidInputException("Invalid Input Postal Code");
                }
            }
        }
        public virtual void PrintDetails()
        {
            Console.WriteLine($"My Name is {Name}, {age} yaers old and Postal Code is {PostalCode}");
        }
    }

    //Class Father which Inherited GrandFather
    /// <summary>
    /// Class Father Inherited Form GrandFather and Added One Member Mobile Number 
    /// and Overriding PrintDetails
    /// </summary>
    class Father : GrandFather
    {
        string mobileNumber;
        public Father(string mobilenumber, string postalcode, byte age, string name) :
            base(postalcode, age, name)
        {
            this.MobileNumber = mobilenumber;
        }

        public string MobileNumber
        {
            get { return mobileNumber; }
            set
            {
                Regex mobilRegex = new Regex("^[1-9][0-9]{9}");
                if (!string.IsNullOrEmpty(value) && mobilRegex.IsMatch(value))
                {
                    mobileNumber = value;
                }
                else
                {
                    throw new InvalidInputException("Please Enter Valid Mobile Number");
                }
            }
        }

        public override void PrintDetails()
        {
            base.PrintDetails();
            Console.WriteLine($"My Mobile Number is {MobileNumber}");
        }
    }

    // Class Son Which Inherited Father
    /// <summary>
    /// ClassS Son Inherited Father and Added Skypeid as per Need
    /// And Puerly Override / Define PrintDetails Method
    /// </summary>
    class Son : Father
    {
        string skypeId = "";
        public Son(string skypeid, string mobilenumber, string postalcode, byte age, string name)
            : base(mobilenumber, postalcode, age, name)
        {
            this.SkypeId = skypeid;
        }
        public string SkypeId
        {
            get { return skypeId; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    skypeId = value;
                }
            }
        }

        public override void PrintDetails()
        {
            Console.WriteLine($"Hi, My Name is {Name},My age is {Age},My Mobile Number is {MobileNumber}" +
                $"\n My Skype Id is {SkypeId} and Postal Code is {PostalCode}");
        }
    }

    #endregion

    
    // --Code Number 19: Overloading Of Constructor
    /// <summary>
    /// Product Class Which Have Basic Fields For Product
    /// </summary>
    class Product
    {
        #region Data Members and Properties
        string productName;
        string productType;
        int productCost;
        byte productratting;

        public string ProductName
        {
            get { return productName; }
            set
            {
                if(!string.IsNullOrEmpty(value))
                {
                    productName = value;
                }
                else
                {
                    throw new InvalidInputException("InvalidInputException for Product Name");
                }
            }
        }
        public string ProductType
        {
            get { return productType; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    productType = value;
                }
                else
                {
                    throw new InvalidInputException("InvalidInputException for Product Type");
                }
            }
        }
        public int ProductCost
        {
            get { return productCost; }
            set
            {
                if(value>0)
                {
                    productCost = value;
                }
                else
                {
                    throw new InvalidInputException("InvalidInput Exception For Product Cost, It must be Non Negative and Non Zero");
                }
            }
        }
        #endregion
        #region Constructors and Methods
        public byte ProductRatting
        {
            get { return productratting; }
        }
        public Product() //Default Constructor
        {
            productType = "defaultName";
            productName = "defaultType";
            productratting = 0;
            productCost = 10;
        }
        public Product (string name,int cost)// Parameterized Constructor Using Two Paras
        {
            this.ProductCost = cost;
            this.ProductName = name;
        }
        public Product (string name,int cost, string producttype,byte productrating):this(name,cost)// Parameterized Constructor with all four para
        {
            this.ProductType = producttype;
            this.productratting = productrating;
        }
        public void ShowProductDetails()
        {
            Console.WriteLine($"Product Name: {ProductName}\nProduct Type: {ProductType}\n" +
                $"Product Ratting: {ProductRatting}\nProduct Cost: {ProductCost}");
        }
        #endregion

    }


    // --Code Number 16:User Defined Exception, also used where Needed
    /// <summary>
    /// Exception for Invalid User Input
    /// </summary>
    class InvalidInputException : Exception
    {
        public InvalidInputException() : base() { }
        public InvalidInputException(string message) : base(message) { }

        public InvalidInputException(string message, Exception innException) : base(message, innException) { }

    }

    #endregion
}
