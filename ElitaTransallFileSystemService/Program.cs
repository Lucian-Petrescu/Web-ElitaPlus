
namespace ElitaTransallFileSystemService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;

            ServicesToRun = new System.ServiceProcess.ServiceBase[]
                            {
                                new ElitaTransallService()
                            };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }
    }
}
