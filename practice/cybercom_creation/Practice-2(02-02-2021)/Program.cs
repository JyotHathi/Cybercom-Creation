using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing;
using System.Reflection;

namespace Practice_2_02_02_2021_
{
    enum day
    {
        Sunday = 2,
        Monday = 8,
        Tuesday = 6,
        Wednesday,
        Thursday,
        Friday,
        Saturday
        
    }

    class Program : Class1
    {
        static void Main(string[] args)
        {
            //Test();
            //Assembly assembly = Assembly.LoadFrom(@"E:\Cybercom-creation\practice\cybercom_creation\Practice-2(02-02-2021)\bin\Debug\Practice-2(02-02-2021).exe");
            //Type[] types = assembly.GetTypes();
            //foreach(Type type1 in types)
            //{
            //    Console.WriteLine(type1.Name +" "+type1.GetType() );
            //    MemberInfo[] typeInfos = type1.GetMembers();
            //    foreach(MemberInfo member in typeInfos)
            //    {
            //        Console.WriteLine($"\t{member.Name}");
            //    }
            //    Console.WriteLine("----------------------------------------------------");
            //    MethodInfo[] methodInfos = type1.GetMethods();
            //    foreach(MethodInfo methodInfo in methodInfos)
            //    {
            //        Console.WriteLine("\t\t"+methodInfo.Attributes.GetType()+" "+ methodInfo.Name);
            //    }
            //    Console.WriteLine("*****************************************************");
            //}
            //MethodInfo methodInfo = typeof(Program).GetMethods()[0];

            //foreach (var v in methodInfo.CustomAttributes)
            //{
            //    Console.WriteLine(v.AttributeType.FullName);
            //}

            //testClass.Value<string> = "Hello";
            //Console.WriteLine(testClass.Value);
            //GenericClass<int> testClass2 = new GenericClass<int>();
            //testClass2.Value = 5;
            //Console.WriteLine(testClass2.Value);

            //Console.WriteLine((new GenericClass().Value="5").Equals((new GenericClass().Value = "5"))+" "+
            //    (new GenericClass().Value = "5").GetHashCode());

            //------------------------------------------- Late Binding ------------------------------------
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //Type type = assembly.GetType("Practice_2_02_02_2021_.GenericClass");
            //object obj = Activator.CreateInstance(type);
            //MethodInfo method = type.GetMethod("Equals");
            //PropertyInfo prop = type.GetProperty("Value");
            //prop.SetValue(obj, "Hello");

            //object obj2 = Activator.CreateInstance(type);
            //prop.SetValue(obj2, "Hello.");
            //object[] parameters = new object[1] { obj2};

            //Console.WriteLine(method.Invoke(obj,parameters));

            //GenericClass<string> genericClass = new GenericClass<string>();
            //genericClass.Value = "5";
            //GenericClass<string> genericClass2 = new GenericClass<string>();
            //genericClass2.Value = "6";
            //Console.WriteLine(genericClass.Equals(genericClass2));

            //Indexer i = new Indexer();
            //Console.WriteLine(i[1,2]); 
            //##########################################################################################################
            //FinalClass<float> finalClass = new FinalClass<float>("Jyot Hathi",8.50f,Grade.AB);
            //FinalClass <double> finalClass2 = new FinalClass<double>("Veer",8.7,Grade.AB);
            //FinalClass<decimal> finalClass3 = new FinalClass<decimal>("Neel",7.9m,Grade.BB);
            //FinalClass<int> finalClass4 = new FinalClass<int>("Fateh",10,Grade.AA);

            //finalClass.DisplayDeatils();
            //Console.WriteLine("----------------------------------");
            //finalClass2.DisplayDeatils();
            //Console.WriteLine("----------------------------------");
            //finalClass3.DisplayDeatils();
            //Console.WriteLine("----------------------------------");
            //finalClass4.DisplayDeatils();
            //Console.WriteLine("----------------------------------");

            //FinalClass<object>.ShowUsers();
            
            var tuple = Tuple.Create(Tuple.Create(1,2,3,4,5,6,7,8));
           
            
            var valtupple = (Id:1, Id2: 2, Id3: 3, I4: 4, Id5: 1, Id6: 1, Id7: 1, Id8: 1, Id9: 1, Id10: 1, Id12: 2, Id13: 3, I14: 4, Id15: 1, Id16: 1, Id17: 1, Id18: 1, 
                Id19: 1);
            Console.WriteLine(valtupple.Id19);
            (string Fname, string Lname, string name) Person = GetPerson();
            Console.WriteLine(Person.Fname + " " + Person.Lname + " " + Person.name);
            Console.WriteLine(valtupple.Id9);
            Method<string>("TestingofGenerics");
            Console.ReadLine();
            //##########################################################################################################
        }

        //[CustomeAttribute("Main Method",Date=DateTime.Now)]
        //public void Test5(int a)
        //{
        //    base.str = "Hello";
        //}
        public static (string,string,string) GetPerson()
        {
            return ("Jyot", "Hathi","TusharBhai");
        }
        public static T Method<T>(T data)
        {
            return data;
        }
    }

    //-------------------------------------------------------------------------------------------
    [AttributeUsage(AttributeTargets.Method)]
    class CustomeAttribute : Attribute
    {
        public string message;
        DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public CustomeAttribute(string message)
        {
            this.message = message;
        }
    }

    //----------------------------------------------------------------
    class GenericClass<T>
    {
        //Y value;
        public T Value
        {
            get;
            set;
        }
        public T Value2
        {
            get;
            set;
        }
        public new bool Equals(object obj)
        {
            if (this.Value.Equals(((GenericClass<T>)obj).Value))
            {
                return true;
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    //--------------------------------------------------------------------------------------------
    class Indexer
    {
       int[] arr = new int[] { 1, 2, 3, 4 };
        
        public int this[int i]
        {
            get { return arr[i]; }
            set { arr[i] = value; }
        }
        public string this[int i,int j]
        {
            get { return arr[i]+","+arr[j]; }
            set { 
                arr[i] = Convert.ToInt32(value);
                arr[j] = Convert.ToInt32(value);
            }
        }
    }
}
