using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSteamStorage.WinService.Services.Implementation
{
	public class SteamManager
	{
		private readonly ILogger _logger;

		public SteamManager(ILogger logger)
		{
			_logger = logger;
		}

		public async Task MoveSteamItem(int appId, string newPath)
		{
			// Steam gets library item locations here-
			// $"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {appId}"
		}
	}
}
