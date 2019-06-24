using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2406
{
    class Program
    {
        static Random random = new Random();
        static int sum = 0;

        static void Main(string[] args)
        {

            Func<int> func = new Func<int>(foo);
            func += goo;

            //int res1 = func.Invoke();
            //Console.WriteLine(res1);

            //int res2 = func();
            //Console.WriteLine(res2);

            //int res3 = (int)func.DynamicInvoke();
            //Console.WriteLine(res3);
            /*
            int sum = 0;
            List<Task<int>> resList = new List<Task<int>>();
            foreach (Delegate del in func.GetInvocationList())
            {
                //new Thread(() => { del.DynamicInvoke(); }).Start();
                //sum = sum + (int)del.DynamicInvoke();
                resList.Add(Task.Run<int>(() => { return (int)del.DynamicInvoke(); }));
            }

            Thread.Sleep(1000);

            Task.WaitAll(resList.ToArray());
            foreach(Task<int> t in resList)
            {
                sum += t.Result;
            }
            
 
            foreach (Func<int> del in func.GetInvocationList())
            {
                IAsyncResult iAsyncResult = del.BeginInvoke(null, "hello"); // non-blocking
            }
            */

            // IAsyncResult iAsyncResult =  func.BeginInvoke(MyAsyncCallBack_AfterDelegateCompleted, func); // async
            //
            //
            //
            //
            //

            //int res4 = func.EndInvoke(iAsyncResult); // blocking
            //Console.WriteLine(res4);

            foreach (Func<int> del in func.GetInvocationList())
            {
                IAsyncResult iAsyncResult = del.BeginInvoke(MyAsyncCallBack_AfterDelegateCompleted, del); // non-blocking
            }

            Console.ReadLine();
            Console.WriteLine(sum);
            Console.WriteLine("Finish...");


        }

        static void MyAsyncCallBack_AfterDelegateCompleted(IAsyncResult iAsyncResult)
        {
            Console.WriteLine("hey i am after the delegate completed .....");
            Func<int> func = iAsyncResult.AsyncState as Func<int>;
            int resInCallback = func.EndInvoke(iAsyncResult); // blocking
            Console.WriteLine(resInCallback);
            sum += resInCallback;
        }

        static int foo()
        {
            int number = random.Next(1000);
            Thread.Sleep(number);
            Console.WriteLine($"print within the delegate foo {number}");
            return number;
        }

        static int goo()
        {
            int number = random.Next(200);
            Thread.Sleep(number);
            Console.WriteLine($"print within the delegate goo {number}");
            return number;
        }

    }
}
