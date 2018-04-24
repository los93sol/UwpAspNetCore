# UwpAspNetCore
Working demo of an ASP.NET Core site in an IOT Core UWP app.

## Notes
1. You must be using Visual Studio 2017 15.7.0 Preview 4.0 or newer
3. You must have the IOT Core Background Application template installed: https://docs.microsoft.com/en-us/windows/iot-core/develop-your-app/backgroundapplications
2. The project must be deployed to a Windows IOT Core device, I'm using a Raspberry Pi 3

## TODO
1. Figure out how to get the entire contents of UwpAspNetLib to copy to the AppX folder without the Post-Build Events that are currently implemented.  Ex: UwpAspNetLib.Views.dll, appsettings.json, appsettings.development.json, wwwroot directory
2. Implement SignalR Core sample
3. Upgrade to Bootstrap 4.1.0
4. Figure out how to load appsettings from json files
5. Get HTTPS working
