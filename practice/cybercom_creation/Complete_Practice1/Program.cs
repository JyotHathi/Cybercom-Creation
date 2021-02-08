using System;
using System.Text;
using System.Collections.Generic;
namespace Complete_Practice1
{
    class Program
    {
        static void Main(string[] args)
        {

            //ArrayPractice();

            //****************************Basics Of Console*************************
            //Some Basics of Console
            //ConsoleKeyInfo consoleKey = Console.ReadKey();
            //Console.WriteLine("------->{0}",consoleKey.Key);
            //Console.WriteLine("------->{0}",consoleKey.KeyChar);

            //int x = Console.Read();
            //Console.WriteLine(x);
            //string str = Console.ReadLine();
            //Console.WriteLine($"{x}---{str}");

            //Console.Title = "Hello";
            //Console.CursorVisible = false;
            //Console.Beep();
            //Console.BackgroundColor = ConsoleColor.Red;
            //Console.Clear();
            //Console.ForegroundColor = ConsoleColor.Green;

            //********************************* Class, Exception ************************ 
            //try
            //{
            //    Person person1 = new Employee("Jyot", 22, "jyothathi38@gmail.com");
            //    Employee employee1 = new Employee("Jyot Hathi",21,"jyothathi@gmail.com");
            //    Person eperson1 = new Employee("abc",01,"abc@gmai.com");

            //    //Console.WriteLine("Is Person=new Employee is type of Employee:"+typeof(Employee).IsInstanceOfType(eperson1));
            //    //Console.WriteLine("Is Person=new Employee is type of Person:" + typeof(Person).IsInstanceOfType(eperson1));
            //    //Console.WriteLine("Is Employee=new Employee is type of Employee:" + typeof(Employee).IsInstanceOfType(employee1));
            //    //Console.WriteLine("Is Employee=new Employee is type of Person:" + typeof(Person).IsInstanceOfType(employee1));

            //    //eperson1.DisplayDetails();
            //    //Console.WriteLine("--------------------------");
            //    //Person person11 = employee1;
            //    //person11.DisplayDetails();

            //    //*****************Operator Overloading****************************
            //    //Console.WriteLine(person1 + person11);

            //    //*********************Lamda Expression**************************
            //    //Func<int, string, int> func = (x, y) =>
            //    //  { x = x * x; Console.WriteLine(y); return x; };
            //    //func(5,"Hello");

            //    //*****************Use of Delegate*********************************
            //    //PrintDeatils ShowEmployeeDetails = new PrintDeatils(()=>Console.WriteLine("Using Delegates\n--------------------------\n" +
            //    //    "Employee's DisplayDetails:\n"));
            //    //ShowEmployeeDetails += employee1.DisplayDetails;
            //    //ShowEmployeeDetails +=() => Console.WriteLine("\nEmployee's DisplayDetails2:\n");
            //    //ShowEmployeeDetails += employee1.DisplayDetails2;
            //    //ShowEmployeeDetails();
            //    //Console.WriteLine(60m / 7m);
            //}
            //catch(WrongDataException we)
            //{
            //    try
            //    {
            //        throw new Exception("Manual Exception For Testing of Inner Exception", we);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message + "\n" + ex.InnerException.Message);
            //    }
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //finally
            //{
            //    Console.WriteLine(FinallyReturn());
            //}

            //List<Employee> list = new List<Employee>();
            //list.Find((employee)=> employee.Name.StartsWith("J"));
            //list.FindAll((employee) => employee.Name.EndsWith("K"));
            //Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();
           
            Console.ReadLine();
            
        }
        #region Array Practice
        /// <summary>
        /// Function For Pratice of Array
        /// </summary>
        /// <remarks>
        /// How To Create Differen Types of Array
        /// <br />
        /// How to Print Them
        /// </remarks>
        public static void ArrayPractice()
        {
            //-------------Declaration of Different Types Of Array---------
            byte[] oneDimensionArray = new byte[5];
            byte[,] twoDimensionArray = new byte[5, 5];
            byte[][] jaggedArray = new byte[5][];
            byte i, j, k;

            //-------------Defining All Arrays---------------------
            //Defining One Dimensional Array
            for (i = 0; i < 5; i++)
            {
                oneDimensionArray[i] = i;
            }

            //Defining two Dimensional Array
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    twoDimensionArray[i, j] = (byte)(i + j);
                }
            }
            i = 0;
            //Defining Jagged Array
            Random randomIndex = new Random();
            for (i = 0; i < 5; i++)
            {
                k = (byte)randomIndex.Next(1, 5);
                jaggedArray[i] = new byte[k];
                for (j = 0; j < k; j++)
                {
                    jaggedArray[i][j] = (byte)(j + i);
                }

            }

            //-----------------Printing All Arrays--------------------
            Console.WriteLine("Printing One Dimensional Array");
            foreach (byte element in oneDimensionArray)
            {
                Console.Write(element + "\t");
            }

            Console.WriteLine("\n\nTwo Dimensional Array");
            for (i = 0; i < 5; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    Console.Write(twoDimensionArray[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nJagged Array");
            foreach (byte[] ele in jaggedArray)
            {
                foreach (byte elem in ele)
                {
                    Console.Write(elem + "\t");
                }
                Console.WriteLine();
            }

            //*******************************************Methods of Array Class*****************

            string[] str = Array.ConvertAll(oneDimensionArray, (ele) => ele.ToString());
            Console.WriteLine("Printing One Dimensional Array After Converting To String");
            foreach (string element in str)
            {
                Console.Write(element + "\t");
            }

            Array.ForEach(oneDimensionArray, ele => Console.WriteLine((byte)Math.Pow(ele, 3)));
            Array.Sort(oneDimensionArray);

        }
        #endregion

        #region Class,abstract class,Interfaces, Inheritance, Polymorephisam

        interface I1
        {
            void Test();
        }
        interface I2
        {
            void Test();
        }
        abstract class Person:I1,I2
        {
            #region DataMembers
            string name;
            int age;
            string emailid;
            static int counter = 0;
            #endregion

            #region Properties
            static public int Counter
            {
                get { return counter; }
            }
            public string Name
            {
                get { return this.name; }
                set
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.name = value;
                    }
                    else
                    {
                        throw new WrongDataException("Please Enter Valid Name");
                    }
                }
            }
            public int Age
            {
                get { return age; }
                set
                {
                    if(value >=0)
                    {
                        this.age = value;
                    }
                    else
                    {
                        throw new WrongDataException("Please Enter Valid Age");
                    }
                }
            }
            public string EmailId
            {
                get { return this.emailid;}
                set
                {
                    if(!String.IsNullOrEmpty(value))
                    {
                        this.emailid = value;
                    }
                    else
                    {
                        throw new WrongDataException("Please Enter Valid EmailId");
                    }
                }
            }
            #endregion

            #region Constructors
            public Person(string name,int age,string email)
            {
                this.Name= name;
                this.Age = age;
                this.EmailId = email;
            }
            public Person() : this("", -1,"") { }
            #endregion

            #region Methods
            public virtual void DisplayDetails()
            {
                Console.WriteLine($"Your Name is {this.Name} \nYour Age is {this.Age}\nYour Email Id Is:{this.EmailId}");
            }
            //void I1.Test()
            //{
            //    Console.WriteLine("Test1");
            //}
            //void I2.Test()
            //{
            //    Console.WriteLine("Test1");
            //}
            public void Test()
            {
                Console.WriteLine("Test1");
            }
            public static string operator+ (Person p1, Person p2)
            {
                return p1.Name +"--"+p2.Name;
            }
            #endregion
        }
        class Employee : Person
        {
            #region DataMembers
            int empId;
            int deptId;
            static int ecounter = 0;
            static Random randomdeptid;
            #endregion

            #region Properties
            public int EmpId
            {
                get { return this.empId; }
            }
            public int DeptId
            {
                get { return this.deptId; }
            }
            static public int ECounter
            {
                get { return ecounter; }
            }
            #endregion

            #region Constructors
            static Employee()
            {
                randomdeptid = new Random();
            }
            public Employee():this("",-1,""){}
            public Employee(string name,int age,string email):base(name,age,email)
            {
                this.empId = ecounter++;
                this.deptId = randomdeptid.Next(1, 7);
            }
            #endregion

            #region Methods
            public override void DisplayDetails()
            {
                //base.DisplayDetails();
                Console.WriteLine($"Your Employee Id : {this.EmailId}\nYour DeptId : {this.DeptId}");
            }
            public void DisplayDetails2()
            {
                base.DisplayDetails();
                Console.WriteLine($"Your Employee Id : {this.EmailId}\nYour DeptId : {this.DeptId}");
            }
            #endregion

        }
        #endregion

        #region Custome Exceptions, Delegate
        class WrongDataException : Exception
        {
            public WrongDataException()
            {

            }
            public WrongDataException(string message):base(message)
            {

            }
            public WrongDataException(string message, Exception ex):base(message,ex)
            {

            }
        }
        public delegate void PrintDeatils();

        #endregion

        #region finally block with return statement
        public static string FinallyReturn()
        {
            try
            {
                //throw new Exception("Exception From Finally Return Block");
                Console.WriteLine("Try Block");
                return "Return From Try Block";
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "Return From Catch Block";
            }
            finally
            {
                Console.WriteLine("Finally Block");
                
            }
        }
        #endregion
    }
}
