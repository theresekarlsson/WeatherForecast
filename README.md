# WeatherForecast
ASP.NET Core MVC Web Application showing current weather and 5 day forecast for a specific location (Karlstad, Sweden).

The application uses an API from openweathermap.org, the One Call API to be exact. 
There is no API key included in the project, however functionality that supports the use of a locally stored secrets.json file (Secret Manager tool) with an api key stored inside is included in comments. Still, this is recommended for development purposes only. 

As the code is now, the easiest way to access the API is to put your key in the apiKey variable in Services/ApiService.cs.


