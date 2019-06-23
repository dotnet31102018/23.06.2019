using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2306_
{
    class Program
    {

       
        static void Main(string[] args)
        {
            Random random = new Random();

            Func<int> func = new Func<int>(() => {
                int number = random.Next(1000);
                Thread.Sleep(number);
                Console.WriteLine(number);
                return number;
            });

            object o1 = func.DynamicInvoke(); // blocking
            Console.WriteLine(o1);

            IAsyncResult async2 = func.BeginInvoke((IAsyncResult ar) =>
            {
                Console.WriteLine("after..........");

                Console.WriteLine(ar.AsyncState); // this is the "hello" parameter

                int res = func.EndInvoke(ar);
                Console.WriteLine("what was the result? " + res);

            }, "hello");

            IAsyncResult async = func.BeginInvoke(MyCallback, func);

            int result = func.EndInvoke(async);
            Console.WriteLine("what was the result? " + result);

            foreach(Func<int> method in  func.GetInvocationList())
            {
                method.BeginInvoke(null, null);
            }
        }

        private static void MyCallback(IAsyncResult ar)
        {
            Func<int> func = ar.AsyncState as Func<int>;
            Console.WriteLine(func.EndInvoke(ar));
        }
    }
}
