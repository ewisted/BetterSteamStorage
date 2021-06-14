using BetterSteamStorage.WinService.Services.Abstraction;
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSteamStorage.WinService.Jobs
{
	public class ConfigPollingJob : IJob
	{
		private readonly ILogger _logger;
		private readonly IConfigService _configService;

		public ConfigPollingJob(ILogger logger, IConfigService configService)
		{
			_logger = logger;
			_configService = configService;
		}

		public async Task Execute(IJobExecutionContext context)
		{
			_logger.Information("Starting config polling job for {id}", context.JobDetail.Key.Name);
			await _configService.RefreshCurrentConfigAsync();
			_logger.Information("Config polling job successfully refreshed config for {id}", context.JobDetail.Key.Name);
		}
	}
}
