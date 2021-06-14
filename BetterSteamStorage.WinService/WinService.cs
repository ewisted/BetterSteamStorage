using BetterSteamStorage.WinService.Services.Abstraction;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSteamStorage.WinService
{
	public class WinService
	{
		private readonly ILogger _logger;
		private readonly IJobScheduler _jobScheduler;
		private readonly IConfigService _configService;

		public WinService(ILogger logger, IJobScheduler jobScheduler, IConfigService configService)
		{
			_logger = logger;
			_jobScheduler = jobScheduler;
			_configService = configService;
		}

		public bool Start()
		{
			return Initialize().ConfigureAwait(false).GetAwaiter().GetResult();
		}

		public bool Stop()
		{
			try
			{
				_jobScheduler.StopJobs().ConfigureAwait(false).GetAwaiter().GetResult();
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occured while attenpting to stop the service.");
				return false;
			}
		}

		private async Task<bool> Initialize()
		{
			try
			{
				var currentConfig = await _configService.RefreshCurrentConfigAsync();
				await _jobScheduler.StartJobs(currentConfig);
				return true;
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occured while attenpting to start the service.");
				return false;
			}
		}
	}
}
