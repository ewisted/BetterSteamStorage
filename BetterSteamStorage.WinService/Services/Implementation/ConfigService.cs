using BetterSteamStorage.Common.Models;
using BetterSteamStorage.WinService.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BetterSteamStorage.WinService.Services.Implementation
{
	public class ConfigService : IConfigService
	{
		private readonly HttpClient _client;
		private WinServiceConfig _currentConfig;

		public ConfigService(Uri baseUrl)
		{
			_client = new HttpClient();
			_client.BaseAddress = baseUrl;
		}

		public WinServiceConfig GetCurrentConfig()
		{
			return _currentConfig;
		}

		public async Task<WinServiceConfig> RefreshCurrentConfigAsync()
		{
			var response = await _client.GetAsync("/config/get");
			response.EnsureSuccessStatusCode();

			var json = await response.Content.ReadAsStreamAsync();
			_currentConfig = await JsonSerializer.DeserializeAsync<WinServiceConfig>(json);
			return _currentConfig;
		}
	}
}
