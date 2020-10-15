using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RandomDateTime
{
    class Program
    {


        static readonly Random rnd = new Random();
        private static Timer aTimer;

        static void Main(string[] args)
        {
            DateTime timeNow = DateTime.Now;/*ToString("yyyy-MM-dd h:mm:ss tt");*/
            DateTime timeWeek = DateTime.Now.AddSeconds(30);/*.ToString("dd.MM.yy");*/
            DateTime randomdate = GetRandomDate(timeNow, timeWeek);

            Console.WriteLine(timeNow);
            Console.WriteLine(timeWeek);
            Console.WriteLine(randomdate);

            double inter = (randomdate - timeNow).TotalMilliseconds;
          
            Console.WriteLine(inter);



            SetTimer(inter);

           
            Console.ReadLine();

            aTimer.Stop();
            aTimer.Dispose();
            
        }



        private static void SetTimer(double time)
        {
            // Create a timer with a two second interval.
            aTimer = new Timer(time);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }
    


        public static DateTime GetRandomDate(DateTime from, DateTime to)
        {
            var range = to - from;

            var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }
    }
}
