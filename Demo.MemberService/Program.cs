namespace Demo.MemberService
{
    using MassTransit.Log4NetIntegration.Logging;
    using Topshelf;
    using Topshelf.Logging;


    class Program
    {
        static int Main()
        {
            Log4NetLogWriterFactory.Use("log4net.config");
            Log4NetLogger.Use();

            return (int)HostFactory.Run(x =>
                {
                    x.RunAsNetworkService();

                    x.Service<MemberService>();
                });
        }
    }
}