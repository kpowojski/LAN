using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using System.IO;
using System.Threading;
using System.Net.Security;

namespace ProjektLAN
{
    class Capture
    {
        private Logs logs;
        private String from;
        private String to;
        public Capture(Logs logs, String from, String to)
        {
            this.logs = logs;
            this.from = from;
            this.to = to;
        }
        
        public void start()
        {
            //Console.WriteLine("Starting FiddlerCore ...");
            this.logs.addLog("Startign FiddlerCore ...");
            Fiddler.CONFIG.IgnoreServerCertErrors = false;

            //FiddlerApplication.OnValidateServerCertificate += new System.EventHandler<ValidateServerCertificateEventArgs>(CheckCert);


            Fiddler.FiddlerApplication.BeforeRequest += delegate(Fiddler.Session oS)
            {
                if (oS.HTTPMethodIs("CONNECT") && (oS.PathAndQuery.Contains(this.from)))
                {
                    
                    //Console.WriteLine("UDALOOOOOOOOOOOOOO SIEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                    //Console.WriteLine(oS.RequestHeaders.HTTPMethod);

                    //oS.hostname = "www.wp.pl";
                    //Console.WriteLine(oS.oRequest["host"]);

                    oS.PathAndQuery = this.to + ":80";


                    //Console.WriteLine(oS.RequestHeaders);
                    //Console.WriteLine("****** Full url" + oS.fullUrl);
                    //Console.WriteLine("****** path: " + oS.PathAndQuery);

                }
                else if (oS.hostname.Contains(this.from)) oS.hostname = this.to;

                if (oS.HTTPMethodIs("GET"))
                {
                    this.logs.addLog("GET", oS.url, "green");
                }
                else
                {
                    this.logs.addLog(oS.RequestHeaders.HTTPMethod, oS.url, "red");
                }
            };

            //Fiddler.FiddlerApplication.BeforeResponse += delegate(Fiddler.Session oS)
            //{
            //    Console.Write("************************");
            //    Console.WriteLine(oS.ResponseHeaders);
            //};
            
            Fiddler.FiddlerApplication.Startup(8877, true, true);

        }

        private void CheckCert(object sender, ValidateServerCertificateEventArgs e)
        {
            if (SslPolicyErrors.None == e.CertificatePolicyErrors)
            {
                return;
            }

            e.ValidityState = CertificateValidity.ForceValid;

        }

        public void stop()
        {
            if (FiddlerApplication.IsStarted())
                FiddlerApplication.Shutdown();

        }
    }
}
