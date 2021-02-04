using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
namespace _2021_02_03
{
    class Pract03_02_2021
    {
        public static void ListPractice()
        {
            /*//------------------------------------------------------------------------------------------
           //Basic Practice of List, Sorting of List and Sorted Dictionary
            List<ContactBook> contacts = new List<ContactBook>();
            contacts.Add(new ContactBook("Jyot Hathi", "9408244998", "Me"));
            contacts.Add(new ContactBook("Kinjal Saradva", "9408244998", "Kinjal"));
            contacts.Add(new ContactBook("Mansi Parmar", "8200621895", "Mansi."));
            contacts.Add(new ContactBook("Kishan Jamariya", "8200621895", "Kishan"));

            Console.WriteLine("Sorting List");
            Console.WriteLine("-------------");
            contacts.Sort((c1, c2) => { return c1.NameForSaving.CompareTo(c2.NameForSaving); });
            foreach(ContactBook contact in contacts)
            {
                Console.WriteLine($"{contact.NameForSaving}--{contact.PersonName}-{contact.ContactNumber}");
            }

            Console.WriteLine("\nSorted Dictionary");
            Console.WriteLine("-----------------");
            ContactBook.ShowContacts();
            //---------------------------------------------------------------------------------------------------*/
            int userChoice = 0;
            ContactBook contact;
            /*do
            {
                Console.WriteLine("1.Add Person\n2.Search\n3.Call\n4.Show Contacts\n5.Show Call Log\n6.Redial\n7.Quit");
                userChoice = Convert.ToInt32(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:
                        contact = new ContactBook();
                        try
                        {
                            contact.AddPerson();
                        }
                        catch
                        {
                            contact = null;
                        }
                        break;
                    case 2:Console.WriteLine("Please Enter Name Which You Want To Search");
                        Console.WriteLine(ContactBook.Search(Console.ReadLine()));
                        break;
                    case 3:
                        Console.WriteLine("Please Enter Name Whome You Wnat To Call");
                        ContactBook.Call(Console.ReadLine());
                        break;
                    case 4:ContactBook.ShowContacts();
                        break;
                    case 5:ContactBook.ShowCallLog();
                        break;
                    case 6:ContactBook.RedialCall();
                        break;
                    case 7:
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
                Console.WriteLine("-----------------------------------\n");
            } while (userChoice != 7);*/
            
        }

    }
    partial class ContactBook
    {
        public static Stack<string> callLog = new Stack<string>();
        public static void ShowContacts()
        {
            if (myContacts.Count != 0)
            {
                foreach (KeyValuePair<string, ContactBook> kvp in myContacts)
                {
                    Console.WriteLine($"{kvp.Key}--{kvp.Value.PersonName}--{kvp.Value.ContactNumber}");
                }
            }
            else
            {
                Console.WriteLine("No Contacts Yet");
            }
        }
        public static void Call(string personaName)
        {
            if(myContacts.ContainsKey(personaName))
            {
                Console.WriteLine($"Called To {personaName}");
                callLog.Push(personaName);
                callLog.TrimExcess();
            }
            else
            {
                Console.WriteLine("No Person is vailable With this Name");
            }
        }
        public static void ShowCallLog()
        {
            if(callLog.Count==0)
            {
                Console.WriteLine("-- No Call Yet --");
            }
            else
            {
                foreach(string personName in callLog)
                {
                    Console.WriteLine(personName);
                }
            }
        }
        public static void RedialCall()
        {
            if(callLog.Count!=0)
            {
                ContactBook.Call(callLog.Pop());
            }
            else
            {
                Console.WriteLine("Redial is Not Possible as No Calls Done Yet");
            }
        }
    }
}
