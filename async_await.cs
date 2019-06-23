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

       static async void foo()
        {
            // async - await
            // wait for task without overhead 
            await Task.Run(() =>
            {
                Console.WriteLine("start.......");
                Thread.Sleep(2000);
                Console.WriteLine("finish.......");
            });

            // this line will execute after t will finish
            //t.Wait();
            Console.WriteLine("after await.........");
        }
        static void Main(string[] args)
        {
            foo();
            Console.WriteLine("After foo....");
            Console.ReadLine();
        }


    }
}
