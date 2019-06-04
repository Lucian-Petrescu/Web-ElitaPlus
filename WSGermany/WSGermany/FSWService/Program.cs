using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace WSGService1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

#if (!DEBUG)
            ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            //
          ServicesToRun = new ServiceBase[] { new Service1() };

          ServiceBase.Run(ServicesToRun);
          //  System.Diagnostics.Debugger.Launch();
#else
            Service1 serv = new Service1();
            serv.StartService();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#endif
        }
    }
}