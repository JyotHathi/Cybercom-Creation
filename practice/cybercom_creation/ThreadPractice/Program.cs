using System;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
public class Example
{
    static Thread thread1, thread2, thread3, thread4;
    static long total1 = 0;
    static Mutex mutex = new Mutex();
    public static void Main()
    {
        Stopwatch stopwatch = new Stopwatch();

        //AsyncAwait(); 
        //ThreadMethod();

        Console.WriteLine(DateTime.Now.ToShortTimeString());

        #region Commented Text
        //// Threads Intialization
        //thread1 = new Thread(AddNumbers);
        //thread1.Name = "Thread1";

        //thread2 = new Thread(AddNumbers);
        //thread2.Name = "Thread2";
        //thread3 = new Thread(AddNumbers);
        //thread4 = new Thread(AddNumbers);

        //stopwatch.Start();
        //// Thread Starting
        //thread1.Start(new object());
        //thread2.Start(new object());
        //// thread3.Start();
        ////thread4.Start();

        //// Waiting To Main Thread To Complete Task
        //thread1.Join();
        //thread2.Join();
        ////thread3.Join();
        ////thread4.Join();
        //stopwatch.Stop();
        //// Final Output
        //Console.WriteLine(thread1.IsAlive + " " + thread2.IsAlive + " " + " " + total1 + " " + stopwatch.ElapsedMilliseconds);
        #endregion
        
        Console.ReadLine();

    }
    
    public async static void AsyncAwait()
    {
        Task task1 = new Task(AddNumbers);
        Task task2 = new Task(AddNumbers);
        task1.Start();
        await task1;
        Console.WriteLine("Task-1 Completed:" + total1);
        task2.Start();
        await task2;
        Console.WriteLine("Task-2 Completed:" + total1);
    }
    public static void ThreadMethod()
    {
        thread1 = new Thread(new ThreadStart(AddNumbers));
        thread2 = new Thread(new ThreadStart(AddNumbers));

        thread1.Start();
        thread1.Join();
        Console.WriteLine("Task-1 Completed:" + total1);
        Thread.Sleep(5000);
        thread2.Start();
        thread2.Join();
        Console.WriteLine("Task-2 Completed:" + total1);
    }

    private static void ThreadProc()
    {

        if (Thread.CurrentThread.Name == "Thread1" &&
            thread2.ThreadState != System.Threading.ThreadState.Unstarted)
            thread2.Join();

        Thread.Sleep(4000);
        Console.WriteLine("\nCurrent thread: {0}", Thread.CurrentThread.Name);
        Console.WriteLine("Thread1: {0}", thread1.ThreadState);
        Console.WriteLine("Thread2: {0}\n", thread2.ThreadState);
    }
    static object ob;
    static object ob1;
    public static object Ob
    {
        get
        {
            return ob;
        }
        set
        {
            ob = value;
        }

    }
    public static void AddNumbers(object objectData)
    {
        bool IsObAquired = false, IsOb1Aquired = false;
        if (!IsObAquired && !IsOb1Aquired)
        {
            ob = objectData;
            ob1 = objectData;

        }
        lock (ob)
        {
            IsObAquired = true;

            for (long i = 0; i < 100000; i++)
            {
                //Interlocked.Increment(ref  total1);
                total1++;
            }
            lock (ob1)
            {
                IsOb1Aquired = true;
                Console.WriteLine(total1);
            }
            IsOb1Aquired = false;
            IsObAquired = false;
        }

    }
    public static void AddNumbers()
    {

        for (long i = 0; i < 100000; i++)
        {
            Interlocked.Increment(ref total1);
            //total1++;
        }
    }

}

