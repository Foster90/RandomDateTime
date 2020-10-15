using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomDateTime
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime timeNow = DateTime.Now;/*ToString("yyyy-MM-dd h:mm:ss tt");*/
            DateTime timeWeek = DateTime.Now.AddMinutes(5);/*.ToString("dd.MM.yy");*/
            DateTime randomdate = GetRandomDate(timeNow, timeWeek);

            Console.WriteLine(timeNow);
            Console.WriteLine(timeWeek);
            Console.WriteLine(randomdate);
            Console.ReadLine();
         

        }

        static readonly Random rnd = new Random();
        public static DateTime GetRandomDate(DateTime from, DateTime to)
        {
            var range = to - from;

            var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }
    }
}
