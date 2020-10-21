using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace RandomDateTime
{
    class Program
    {


        static readonly Random rnd = new Random();
        private static System.Timers.Timer aTimer;
        private static ManualResetEvent mre = new ManualResetEvent(false);
        public static List<Quote> qlist = new List<Quote>();
        public static int quotecount;
        public static bool listempty = false;
       

        static void Main(string[] args)
        {

            //List <Quote> qlist = new List<Quote>();
            string json = File.ReadAllText("../../datafile.json");
            qlist = JsonConvert.DeserializeObject<List<Quote>>(json);
            //qlist.Add(JsonConvert.DeserializeObject<Quote>(json));

            qlist.Add(new Quote("London"));
            qlist.Add(new Quote ( "New York" ));

            string list = qlist[quotecount].qutoe;

            while (listempty == false) {

                DateTime timeNow = DateTime.Now;/*ToString("yyyy-MM-dd h:mm:ss tt");*/
                DateTime timeWeek = DateTime.Now.AddSeconds(10);/*.ToString("dd.MM.yy");*/
                DateTime randomdate = GetRandomDate(timeNow, timeWeek);


             

                //using (FileStream s2 = new FileStream("log.txt", FileMode.OpenOrCreate, FileAccess.Write))
                using (var file = File.Exists("log.txt") ? File.Open("log.txt", FileMode.Append) : File.Open("log.txt", FileMode.CreateNew))
                {
                    using (StreamWriter sr = new StreamWriter(file))
                    {
                        sr.WriteLine(timeNow);
                        sr.WriteLine(timeWeek);
                        sr.WriteLine(randomdate);

                    }
                }
                //Console.WriteLine(timeNow);
                //Console.WriteLine(timeWeek);
                //Console.WriteLine(randomdate);
                 

                double inter = (randomdate - timeNow).TotalMilliseconds;

                Console.WriteLine(inter);



                SetTimer(inter, list);
                mre.Reset();

                Console.WriteLine("Fin");

                aTimer.Stop();
                aTimer.Dispose();
            }
            
        }



        private static  void SetTimer(double time, string quote)
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(time);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed +=  OnTimedEvent;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
            mre.WaitOne();


        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
           Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            string quote = GetQuote(quotecount);

            
            Console.WriteLine("{0}",quote);
            mre.Set();
            quotecount++;

            //aTimer.Elapsed -= OnTimedEvent;

        }



        public static DateTime GetRandomDate(DateTime from, DateTime to)
        {
            var range = to - from;

            var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }


        private static string GetQuote(int qnumber)
        {
            if (qnumber + 1 == qlist.Count())
            {
                listempty = true;
                return qlist[qnumber].qutoe;
             
            }
            else
            {
                return qlist[qnumber].qutoe;
            }
        }
    }
}
