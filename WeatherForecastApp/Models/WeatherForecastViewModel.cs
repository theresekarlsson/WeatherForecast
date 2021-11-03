
using System.Collections.Generic;

namespace WeatherForecastApp.Models
{
    public class WeatherForecastViewModel
    {
        public string CurrentDate { get; set; }
        public string CurrentDayOfWeek { get; set; }
        public double CurrentTemp { get; set; }
        public double CurrentFeelsLike { get; set; }
        public List<DescriptionModel> WeatherDescription { get; set; }
        public double CurrentWindSpeed { get; set; }
        public List<DailyForecastModel> Daily { get; set; }
        public double CurrentPrecipitation { get; set; }
        public int CurrentHumidity { get; set; }
    }

    public class DescriptionModel
    {
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class DailyForecastModel
    {
        public string Date { get; set; }
        public string DayOfWeek { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public List<DescriptionModel> WeatherDescription { get; set; }
    }
}