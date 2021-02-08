using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
namespace CompletePractice2
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonDetails personDetails1 = new PersonDetails("Jyot");
            PersonDetails personDetails2 = new PersonDetails("Kishan");
            PersonDetails personDetails3 = new PersonDetails("Mansi");
            PersonDetails personDetails4 = new PersonDetails("Kinjal");
            PersonDetails personDetails5 = new PersonDetails("Dipti");

            Thread thread1 = new Thread(() => { ServiceProvider.Call(personDetails1, personDetails2); });
            Thread thread2 = new Thread(() => { ServiceProvider.Call(personDetails3, personDetails4); });
            Thread thread3 = new Thread(() => { ServiceProvider.Call(personDetails3, personDetails5); });
            Thread thread4 = new Thread(() => { ServiceProvider.Call(personDetails5, personDetails1); });


            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();

            //ServiceProvider.Call(personDetails1, personDetails2);
            //ServiceProvider.Call(personDetails3, personDetails4);
            //ServiceProvider.Call(personDetails3, personDetails5);
            //ServiceProvider.Call(personDetails5, personDetails1);

            Console.WriteLine(ServiceProvider.personDetails.Count);
            Console.ReadLine();

        }
    }
    class ServiceProvider
    {
        static Dictionary<string, PersonDetails> myContacts = new Dictionary<string, PersonDetails>();
        public static Dictionary<string, bool> personDetails = new Dictionary<string, bool>();
        static object locker = new Object();
        

        public ServiceProvider(PersonDetails PersonDetails1)
        {
            myContacts.Add(PersonDetails1.PersonNumber, PersonDetails1);
        }
        public static PersonDetails Search(string number)
        {
            if (myContacts.ContainsKey(number))
            {
                return myContacts[number];
            }
            else
            {
                return null;
            }

        }

        public static void Call(PersonDetails p1, PersonDetails p2)
        {

            bool connectable=false;
            bool Ischecked=false;
            bool tempbool1, tempbool2; ;

            lock (locker)
            {
                //Thread.Sleep(5000);
                Ischecked = true;
                Thread.Sleep(5000);

                Thread thread = new Thread(() =>
                {
                   
                   bool st1 = personDetails.TryGetValue(p1.PersonName, out tempbool1);
                   bool st2 = personDetails.TryGetValue(p2.PersonName, out tempbool2);

                   if (st1 && tempbool1 == true)
                   {
                       Console.WriteLine("You Can Not Make More Then One Call");
                       connectable = false;
                   }
                   else if (st2 && tempbool2 == true)
                   {
                       Console.WriteLine("Person Is Bussy Try After Some Time");
                       connectable = false;
                   }
                   else
                   {
                       if (!st1 && !st2)
                       {
                           personDetails.Add(p1.PersonName, true);
                           personDetails.Add(p2.PersonName, true);
                       }
                       else if (!st1)
                       {
                           personDetails.Add(p1.PersonName, true);
                       }
                       else if (!st2)
                       {
                           personDetails.Add(p2.PersonName, true);
                       }
                       else
                       {
                           personDetails[p1.PersonName] = false;
                           personDetails[p2.PersonName] = false;
                       }

                       connectable = true;
                   }
               });
                thread.Start();
                thread.Join();
            }
            if (Ischecked)
            {
                if (!connectable)
                {
                    try
                    {
                        Thread.CurrentThread.Abort();
                    }
                    catch { }
                }
                lock (p1)
                {
                    Console.WriteLine($"Call Is Connected Between {p1.PersonName} - {p2.PersonName}");
                    lock (p2)
                    {
                        Thread.Sleep(10000);
                    }
                    personDetails[p1.PersonName] = false;
                    personDetails[p2.PersonName] = false;
                }
            }

        }

    }

    class PersonDetails
    {
        #region Members
        string personName;
        string personEmail;
        string personNumber;
        #endregion
        public PersonDetails()
        {

        }
        public PersonDetails(string name)
        {
            this.personName = name;
        }
        #region Properties
        public string PersonName
        {
            get { return personName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    personName = value;
                }
                else
                {
                    throw new InvalidInputException("Please Enter Valid Input (Person Name)");
                }
            }
        }
        public string PersonEmail
        {
            get { return personEmail; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    personEmail = value;
                }
                else
                {
                    throw new InvalidInputException("Please Enter Valid Input (Person Email)");
                }
            }
        }
        public string PersonNumber
        {
            get
            {

                return personNumber;

            }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length == 10)
                {
                    personNumber = value;
                }
                else
                {
                    new InvalidInputException("Please Enter Valid Input (Person Mobile Numbers)");
                }
            }
        }
        #endregion
    }
    class InvalidInputException : Exception
    {
        public InvalidInputException() : base() { }

        public InvalidInputException(string message) : base(message) { }

        public InvalidInputException(string message, Exception inexception) : base(message, inexception) { }

    }
}
