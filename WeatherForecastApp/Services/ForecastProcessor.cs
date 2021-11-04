using System;
using System.Collections.Generic;
using System.Globalization;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Services
{
    public class ForecastProcessor
    {
        internal static object HandleResponse(WeatherResponseModel forecastResponse)
        {
            var weatherforecasts = new WeatherForecastViewModel
            {
                CurrentDate = GetDate(forecastResponse.Current.Dt),
                CurrentDayOfWeek = "Idag",  // To get actual day of the week, replace with:  GetDay(forecastResponse.Current.Dt)
                CurrentTemp = GetTemp(forecastResponse.Current.Temp),
                CurrentFeelsLike = GetTemp(forecastResponse.Current.Feels_Like),
                CurrentWindSpeed = GetWindSpeed(forecastResponse.Current.Wind_Speed),
                CurrentPrecipitation = GetPrecipitation(forecastResponse.Hourly[0].Pop),
                CurrentHumidity = forecastResponse.Current.Humidity,
                WeatherDescription = new List<DescriptionModel>(),
                Daily = new List<DailyForecastModel>()
            };

            foreach (var item in forecastResponse.Current.Weather)
            {
                var weatherDescription = new DescriptionModel
                {
                    Description = item.Description,
                    Icon = GetIconUrl(item.Icon)
                };
                weatherforecasts.WeatherDescription.Add(weatherDescription);
            }
             // -2 because showing weather for today + 5 days forecast (API response has daily forecast for 7 days)
            for (int i = 0; i < forecastResponse.Daily.Count-2; i++)
            {
                if (GetDate(forecastResponse.Daily[i].Dt) != weatherforecasts.CurrentDate) 
                {
                    var dailyForecastModel = new DailyForecastModel
                    {
                        Date = GetDate(forecastResponse.Daily[i].Dt),
                        DayOfWeek = GetDay(forecastResponse.Daily[i].Dt),
                        TempMin = GetTemp(forecastResponse.Daily[i].Temp.Min),
                        TempMax = GetTemp(forecastResponse.Daily[i].Temp.Max),
                        WeatherDescription = new List<DescriptionModel>()
                    };

                    foreach (var item in forecastResponse.Daily[i].Weather)
                    {
                        var weatherDescription = new DescriptionModel
                        {
                            Icon = GetIconUrl(item.Icon)
                        };
                        dailyForecastModel.WeatherDescription.Add(weatherDescription);
                    }
                    weatherforecasts.Daily.Add(dailyForecastModel);
                }       
            }
            return weatherforecasts;
        }

        private static double GetPrecipitation(double pop)
        {
            if (pop == 0)
                return pop;
            else
            return pop * 100;
        }

        internal static string GetDay(int unixTimeStamp)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).LocalDateTime;

            string dayOfWeek = dateTimeOffset.ToString("dddd");
            TextInfo textInfo = new CultureInfo("se-sv", false).TextInfo;

            return textInfo.ToTitleCase(dayOfWeek);
        }

        internal static string GetDate(int unixTimeStamp)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).LocalDateTime;
            return dateTimeOffset.DateTime.Date.ToString("dd MMMM").TrimStart(new char[] { '0' });
        }

        internal static string GetIconUrl (string iconCode)
        {
            return $"https://openweathermap.org/img/wn/{iconCode}@2x.png";
        }

        internal static double GetWindSpeed(double wind_Speed)
        {
            return Math.Round(wind_Speed, MidpointRounding.AwayFromZero);
        }

        internal static double GetTemp(double temp)
        {
            return Math.Round(temp, 1, MidpointRounding.AwayFromZero);
        }

        // called if attempt to retrieve data from API fails.
        internal static WeatherForecastViewModel GetDefaultViewModel()
        {
            var weatherforecasts = new WeatherForecastViewModel
            {
                CurrentDate = "No data available",
                CurrentDayOfWeek = "",
                CurrentTemp = 0,
                CurrentFeelsLike = 0,
                CurrentWindSpeed = 0,
                CurrentPrecipitation = 0,
                CurrentHumidity = 0,
                WeatherDescription = new List<DescriptionModel>(),
                Daily = new List<DailyForecastModel>()
            };

            var weatherDescription = new DescriptionModel
            {
                Description = "",
                Icon = ""
            };
            weatherforecasts.WeatherDescription.Add(weatherDescription);

            for (int i = 0; i < 5; i++)
            {
                var dailyForecastModel = new DailyForecastModel
                {
                    Date = "",
                    DayOfWeek = "",
                    TempMin = 0,
                    TempMax = 0,
                    WeatherDescription = new List<DescriptionModel>()
                };

                var dailyWeatherDescription = new DescriptionModel
                {
                    Icon = ""
                };
                dailyForecastModel.WeatherDescription.Add(dailyWeatherDescription);
                weatherforecasts.Daily.Add(dailyForecastModel);
            }
            return weatherforecasts;
        }
    }
}
