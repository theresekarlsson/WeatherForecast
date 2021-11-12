using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecastApp.Config;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Services
{
    public interface IApiService
    {
        Task<WeatherForecastViewModel> GetForecastAsync();
    }

    public class ApiService : IApiService
    {
        private readonly HttpClient _client;
        private readonly AppConfiguration _appConfig;

        public ApiService(HttpClient client, AppConfiguration appConfig)
        {
            _appConfig = appConfig;
            _client = client;
        }

        public ApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<WeatherForecastViewModel> GetForecastAsync()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string url = BuildApiUrl();

            try { 
                var response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonOptions = new JsonSerializerOptions { IgnoreNullValues = false, PropertyNameCaseInsensitive = true };
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var weatherResponse = await JsonSerializer.DeserializeAsync<WeatherResponseModel>(responseStream, jsonOptions);

                WeatherForecastViewModel processedForecast = (WeatherForecastViewModel)ForecastProcessor.HandleResponse(weatherResponse);
            
                return processedForecast;
            }
            catch (HttpRequestException)
            {
                return new WeatherForecastViewModel();
            }
        }

        private string BuildApiUrl()
        {
            string apiResource = "onecall";
            string units = "metric";
            string lat = "59.376060";
            string lon = "13.505050";
            string language = "sv";
            string apiKey = _appConfig.ApiKey;

            return $"https://api.openweathermap.org/data/2.5/{apiResource}?units={units}&lat={lat}&lon={lon}&lang={language}&appid={apiKey}";
        }
    }
}
