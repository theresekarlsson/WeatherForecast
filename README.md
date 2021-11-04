# WeatherForecast
ASP.NET Core MVC Web Application showing current weather and 5 day forecast for a specific location (Karlstad, Sweden).

The application uses an API from openweathermap.org, the One Call API to be exact. 
There is no API key included in the project, however functionality that supports the use of a locally stored secrets.json file (Secret Manager tool) with an api key stored inside is included in comments. If you wish to store and retrieve your key using the Secret Manager tool, the following project methods and files are affected and need some comments to be uncommented : ConfigureServices() in Startup.cs and BuildApiUrl() in Services/ApiService.cs.

As the code is now, the easiest way to access the API is to put your key in the apiKey variable in Services/ApiService.cs.



Information about the Secret Manager tool and user secrets: https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows


