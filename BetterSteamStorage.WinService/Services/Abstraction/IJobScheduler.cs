using BetterSteamStorage.Common.Models;
using System.Threading.Tasks;

namespace BetterSteamStorage.WinService.Services.Abstraction
{
	public interface IJobScheduler
	{
		Task StartJobs(WinServiceConfig config);
		Task StopJobs();
	}
}
