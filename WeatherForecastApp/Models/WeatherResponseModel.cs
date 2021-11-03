using System.Collections.Generic;

namespace WeatherForecastApp.Models
{
    public class WeatherResponseModel
    {
        public Current Current { get; set; }
        public List<Daily> Daily { get; set; }
        public List<Hourly> Hourly { get; set; }
    }

    public class Hourly
    {
        public double Pop { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Current
    {
        public int Dt { get; set; }
        public double Temp { get; set; }
        public double Feels_Like { get; set; }
        public double Wind_Speed { get; set; }
        public int Humidity { get; set; }
        public List<Weather> Weather { get; set; }
    }

    public class Temp
    {
        public double Min { get; set; }
        public double Max { get; set; }
    }

    public class Daily
    {
        public int Dt { get; set; }
        public Temp Temp { get; set; }
        public List<Weather> Weather { get; set; }
    }
}
