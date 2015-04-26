using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using System.IO;

namespace ProjektLAN
{
    class Capture
    {
        private Logs logs;
        public Capture(Logs logs)
        {
            this.logs = logs;

        }
        
        public void start()
        {
            //Console.WriteLine("Starting FiddlerCore ...");
            this.logs.addLog("Startign FiddlerCore ...");
            Fiddler.CONFIG.IgnoreServerCertErrors = false;
            Fiddler.FiddlerApplication.BeforeRequest += delegate(Fiddler.Session oS)
            {
                Console.WriteLine(oS.GetRedirectTargetURL());
                this.logs.addLog(oS.url);
            };
            Fiddler.FiddlerApplication.Startup(8877, true, true);
        }

        public void stop()
        {
            if (FiddlerApplication.IsStarted())
                FiddlerApplication.Shutdown();

        }
    }
}
