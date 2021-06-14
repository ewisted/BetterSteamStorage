using Autofac;
using Autofac.Extras.Quartz;
using BetterSteamStorage.WinService.Services.Implementation;
using BetterSteamStorage.WinService.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterSteamStorage.WinService.Jobs;
using Serilog;
using AutofacSerilogIntegration;
using BetterSteamStorage.WinService.Services;

namespace BetterSteamStorage.WinService
{
	public static class Bootstrapper
	{
		public static IContainer BuildContainer()
		{
            var baseUrl = new Uri(ConfigurationManager.ConnectionStrings["WebServerBaseUrl"].ConnectionString);
            var builder = new ContainerBuilder();

            // Logger Registrations

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            builder.RegisterLogger();

            // Service Registrations

            builder.RegisterType<ConfigService>()
               .WithParameter("baseUrl", baseUrl)
               .As<IConfigService>()
               .SingleInstance();

            builder.RegisterType<JobScheduler>()
                .As<IJobScheduler>()
                .SingleInstance();

            // Quartz Registrations

            var schedulerConfig = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "BetterSteamStorageScheduler" },
                { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" },
                { "quartz.threadPool.threadCount", "2" }
            };

            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = c => schedulerConfig
            });

            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(ConfigPollingJob).Assembly));
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(SteamLibraryMigrationJob).Assembly));

            var container = builder.Build();
            return container;
        }
	}
}
