using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProjektLAN
{
    class Proxy
    {
        private Logs logs;
        public int options;
        public String blockedPage;
        public String redirectPage;
        
        public Proxy(Logs logs)
        {
            this.logs = logs;
        }

        public void start()
        {
            List<Fiddler.Session> oAllSession = new List<Fiddler.Session>();
            Fiddler.FiddlerApplication.BeforeRequest += delegate(Fiddler.Session oS)
            {
                oS.bBufferResponse = false;
                oS["X-AutoAuth"] = "(default)";
                Monitor.Enter(oAllSession);
                oAllSession.Add(oS);
                Monitor.Exit(oAllSession);

                switch (this.options)
                {

                    case 0: //sniffing
                        
                        break;

                    case 1: //block net
                        
                        oS.oRequest.FailSession(404, "Blocked", "404 not found");
                        break;
                    case 2: //redirect flow from one page to another
                        //oS.utilCreateResponseAndBypassServer();
                        if (oS.HTTPMethodIs("CONNECT") && (oS.PathAndQuery.Contains(this.blockedPage)))
                        {
                            oS.PathAndQuery = this.redirectPage + ":80";
                        }
                        else if (oS.hostname.Contains(this.blockedPage)) oS.hostname = this.redirectPage;
                        break;
                    case 3: // 401 HTTP error 
                        try
                        {
                            oS.utilCreateResponseAndBypassServer();
                            oS.oResponse.headers.HTTPResponseCode = 401;
                            oS.oResponse.headers.HTTPResponseStatus = "401 Auth Required ";
                            oS.oResponse["WWW-Authenticate"] = "Basic realm=\"Auth Required\"";
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("invalid");
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine("wyjatek");
                        }
                        break;
                    default:
                        break;
                }
                this.logs.addLog(oS.RequestMethod, oS.fullUrl, "black");
            };

            


            Fiddler.FiddlerApplication.Startup(8877, true, true);

        }



        public void stop()
        {
            Console.WriteLine("stopped");
            Fiddler.FiddlerApplication.Shutdown();
        }

    
    }
}
