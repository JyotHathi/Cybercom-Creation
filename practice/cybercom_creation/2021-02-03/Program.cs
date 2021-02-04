using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2021_02_03
{
    public delegate int MaxResult(int [] array);
    partial class Program
    {
        public int hello;
        static void Main(string[] args)
        {
            #region Commented Text
            //StringvsStringBuilder();

            /*//------------------------------------------------------------------------------------
              // How To Acess Class's Properties (Set and Get) as Array
              ClassAccessAsArray obj = new ClassAccessAsArray();
              obj["Name"] = "Object-1";
              obj["Value"] = 5;
              obj["CreationTime"] = DateTime.Now.ToLocalTime();

              Console.WriteLine($"Object Name: {obj["Name"]}\nObject Value:{obj["Value"]}\n" +
                  $"Object Creation Details: {obj["CreationTime1"]}");
              //-------------------------------------------------------------------------------------*/

            /*//---------------------------------------------------------------------------------------       
            //Overloading Using Params and Default Para
            int result=AddNumbers(5,2,7,-8)??0;
            Console.WriteLine(result);
            result=AddNumbers(5);
            Console.WriteLine(result);
            AddNumbers(num2: 10, num1: 7);
            //------------------------------------------------------------------------------------------*/

            /*//----------------------------------------------------------------------------------------\
            //Contact Book Using Dictionary
            try
            {
                //ContactBook contact = new ContactBook("Kishan Jamariya", "", "Kishan-B.E");
                ContactBook contact1 = new ContactBook("Mansi Parmar", "8200621895", "Mansi-B.E");
                ContactBook contact2 = new ContactBook("Kinjal Saradva", "9408244998", "");
                Console.WriteLine("PLease Enter Person Name TO Get Details");
                string data = ContactBook.Search(Console.ReadLine());
                if (data != "")
                {
                    Console.WriteLine(data);
                }
                else
                {
                    Console.WriteLine("\n----No Result Found-----");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            //-----------------------------------------------------------------------------------------*/

            /*//-----------------------------------------------------------------------------------------
             // Use of delegate, Dictionary and Lamda Function
            int result1 = CheckUsingDelegate(array =>
            {
                int max = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    if (max < array[i])
                        max = array[i];
                }
                return max;
            }, 1, 2, 3, 4, 5, 6);
            int result2 = CheckUsingDelegate(array =>
            {
                Dictionary<int, int> numCounts = new Dictionary<int, int>();

                for (int i = 0; i < array.Length; i++)
                {
                    if (!numCounts.ContainsKey(array[i]))
                    {
                        numCounts.Add(array[i], 1);
                    }
                    else
                    {
                        numCounts[array[i]] += 1;
                    }
                }
                Func<Dictionary<int, int>, int> func =
                kvps =>
                {
                    int result = 0,max=-1;
                    foreach (KeyValuePair<int, int> kvp in kvps)
                    {
                        if (result < kvp.Value)
                        { result = kvp.Value; max = kvp.Key; }
                    }
                    return max;
                };
                return func(numCounts);
            }, 1, 2, 1, 2, 2, 6);
            Console.WriteLine("Max From (1,2,3,4,5,6): {0}", result1);
            Console.WriteLine("\nMax Occurnce From (1, 2, 1, 2, 2, 6): {0}",result2);
            //----------------------------------------------------------------------------------*/

            /*//--------------------------------------------------------------------------------------------------
            // List and Array and Conversion to Dictionary
            List<int> list = new List<int>() { 10, 20, 30, 40 };
            Dictionary<int, int> dict = list.ToDictionary<int, int>(ele => 2 * ele);
            foreach (KeyValuePair<int, int> keyvaluepairs in dict)
            {
                Console.WriteLine($"Key: {keyvaluepairs.Key}--Value: {keyvaluepairs.Value}");
            }
            List<ContactBook> ls = new List<ContactBook>();
            ContactBook[] contacts = {
            new ContactBook("Kinjal", "9408244998", "Kinjal"),
            new ContactBook("Mansi", "8200621895", "Mansi"),
            new ContactBook("Kishan", "8200621895", "Kishan") };
            Dictionary<string, ContactBook> dict2=contacts.ToDictionary(ele=>ele.PersonName,ele=>ele);
            foreach (KeyValuePair<string, ContactBook> keyvaluepairs in dict2)
            {
                Console.WriteLine($"Key: {keyvaluepairs.Key}--Value: {keyvaluepairs.Value.ContactNumber}");
            }
            //-----------------------------------------------------------------------------------------------------*/

            /*//-----------------------------------------------------------------------------------
            //To Test Ref and Out on objects
            Program p = new Program();
            Program pq=new Program();
            Console.WriteLine(pq.GetHashCode()+" "+pq.hello);
            Program.ChangeCheck(pq);
            Console.WriteLine(pq.GetHashCode()+" "+pq.hello);
            //-------------------------------------------------------------------------------------*/
            
            /*-----------------------------------------------------------------------------------------
            // Generic Delegates 
            Method<string> method = new Method<string>((ele)=> { return "Hello" + ele; });
            Method<string> method2 = new Method<string>((ele)=> { return "Hello" + ele + "..."; });
            method += method2;
            Console.WriteLine(method2("Jyot"));
            Hello<string>.name = "Hello";
            Console.WriteLine(Hello<float>.name);
            -----------------------------------------------------------------------------------------*/
            #endregion
            Pract03_02_2021.ListPractice();
            Console.ReadLine();
        }
        #region Methods
        public static void StringvsStringBuilder()
        {
            //------------------------String
            Console.WriteLine("\nString:\n");
            string stringValue = "";
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(stringValue.GetHashCode() + $"- {stringValue}");
                stringValue += i.ToString();
            }

            //----------------------- StringBuilder
            Console.WriteLine("\nStringBuilder:\n");
            StringBuilder stringValue2 = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(stringValue.GetHashCode() + $"- {stringValue2}");
                stringValue2.Append(i.ToString());
            }
        }

        # region Overloading Using Params
        public static int AddNumbers(int num1, int num2 = 5)
        {
            return AddNumbers(num1, num2, 0) ?? 0;
        }

        public static int? AddNumbers(params int[] array)
        {
            if (array.Length == 0)
                return null;
            else
            {
                int result = 0;
                foreach (int i in array)
                {
                    result += i;
                }
                return result;
            }
        }

        public static int CheckUsingDelegate(MaxResult maxResult, params int[] array)
        {
            return maxResult(array);
        }
        #endregion

        public static void ChangeCheck(Program p)
        {
            p = new Program();
            p.hello = 5;
        }
        #endregion
       
    }

    #region Resources
    // How To Acess Class's Properties (Set and Get) as Array
    class ClassAccessAsArray
    {
        private string objName;
        private int objValue;
        private DateTime creationTime;
        public object this[string name]
        {
            get
            {
                switch (name)
                {
                    case "Name": return this.objName;

                    case "Value": return this.objValue;

                    case "CreationTime": return this.creationTime.ToString();
                }
                return "Invalid";
            }
            set
            {
                switch (name)
                {
                    case "Name":
                        this.objName = (string)value;
                        break;

                    case "Value":
                        this.objValue = (int)value;
                        break;

                    case "CreationTime":
                        this.creationTime = Convert.ToDateTime(value);
                        return;
                }
            }
        }
    }

    partial class ContactBook//:IComparable<int>
    {
        string personName;
        string contactNumber;
        string nameForSaving;
        static SortedDictionary<string, ContactBook> myContacts;
        static ContactBook contact = null;

        /*public int CompareTo(ContactBook contact)
        {
            return this.NameForSaving.CompareTo(contact.NameForSaving);
        }*/
        public ContactBook(string name, string number, string saveName)
        {
            this.PersonName = name;
            this.ContactNumber = number;
            this.NameForSaving = saveName;
            myContacts.Add(this.NameForSaving, this);
        }
        public ContactBook()
        {

        }
        static ContactBook()
        {
            myContacts = new SortedDictionary<string, ContactBook>();
        }
        public string PersonName
        {
            get { return personName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.personName = value;
                }
                else
                {
                    throw new Exception("Invalid Input(Person Name) Exception");
                }
            }
        }
        public string ContactNumber
        {
            get { return contactNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.contactNumber = value;
                }
                else
                {
                    throw new Exception("Invalid Input(Contact Number) Exception");
                }
            }
        }
        public string NameForSaving
        {
            get { return nameForSaving; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.nameForSaving = value;
                }
                else
                {
                    this.nameForSaving = this.personName;
                }
            }
        }
        public void AddPerson()
        {
            Console.WriteLine("Please Enter Person Name");
            this.PersonName = Console.ReadLine();
            Console.WriteLine("Please Enter Contact Number");
            this.ContactNumber = Console.ReadLine();
            Console.WriteLine("Please Enter Name By Which you want To Save\n(Optional, To Avoid Just Press Enter)");
            this.NameForSaving = Console.ReadLine();

            myContacts.Add(this.NameForSaving, this);
        }

        public static string Search(string name)
        {
            
            if(myContacts.TryGetValue(name,out contact))
            {
                
                string result = $"\nSerach Result:\nName: {contact.PersonName}\nNumber: {contact.ContactNumber}";
                contact = null;
                return result;
            }
            else
            {
                return "No Result Found";
            }
        }
    }
    public delegate T Method<T>(T value);
    struct Hello<T>
    {
        public static T name;
    }

    static class MathClass1
    {
        public static void Test()
        {

        }
    }
    static class MathClass2
    {
        static int i = 0;
        public static int I
        {
            get;
            set;
        }

        public static void Test()
        {

        }

    }
    #endregion


}
