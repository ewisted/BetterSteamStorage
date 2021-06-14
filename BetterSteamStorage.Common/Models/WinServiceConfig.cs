using System;
using System.Collections.Generic;
using System.Text;

namespace BetterSteamStorage.Common.Models
{
	public class WinServiceConfig
	{
		public string ConfigPollingChronExpression { get; set; }
		public string SteamLibraryPollingChronExpression { get; set; }
	}
}
