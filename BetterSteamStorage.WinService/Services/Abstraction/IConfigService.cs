using BetterSteamStorage.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSteamStorage.WinService.Services.Abstraction
{
	public interface IConfigService
	{
		WinServiceConfig GetCurrentConfig();
		Task<WinServiceConfig> RefreshCurrentConfigAsync();
	}
}
