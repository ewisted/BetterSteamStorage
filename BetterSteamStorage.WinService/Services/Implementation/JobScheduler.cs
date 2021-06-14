using BetterSteamStorage.Common.Models;
using BetterSteamStorage.WinService.Jobs;
using BetterSteamStorage.WinService.Services.Abstraction;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSteamStorage.WinService.Services
{
    public class JobScheduler : IJobScheduler
    {
        private readonly IScheduler _scheduler;

        public JobScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task StartJobs(WinServiceConfig config)
        {
            ScheduleJobs(config);
            await _scheduler.Start();
        }

        private void ScheduleJobs(WinServiceConfig config)
        {
            ScheduleJobWithCronSchedule<ConfigPollingJob>(config.ConfigPollingChronExpression);
            ScheduleJobWithCronSchedule<SteamLibraryMigrationJob>(config.SteamLibraryPollingChronExpression);
        }

        private void ScheduleJobWithCronSchedule<T>(string cronShedule) where T : IJob
        {
            var jobName = typeof(T).Name;
            var job = JobBuilder
                .Create<T>()
                .WithIdentity(jobName, $"{jobName}-Group")
                .Build();

            var cronTrigger = TriggerBuilder
                .Create()
                .WithIdentity($"{jobName}-Trigger")
                .StartNow()
                .WithCronSchedule(cronShedule)
                .ForJob(job)
                .Build();

            _scheduler.ScheduleJob(cronTrigger);
        }

        public async Task StopJobs()
        {
            await _scheduler.Shutdown();
        }
    }
}
