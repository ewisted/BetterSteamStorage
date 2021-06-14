using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Autofac;

namespace BetterSteamStorage.WinService
{
    public class Program
    {
        public static void Main()
        {
            var container = Bootstrapper.BuildContainer();

            var rc = HostFactory.Run(c =>
            {
                c.UseAutofacContainer(container);

                c.Service<WinService>(s =>
                {
                    s.ConstructUsingAutofacContainer();
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());
                });

            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
