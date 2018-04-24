# UwpAspNetCore
Demo to host an ASP.NET Core site in an IOT Core UWP app.  This actually works!

## Notes

I'll clean these notes up once everything is working, for now this is just a scratchpad for me to keep notes on as I work through things

1. You must have the internetClientServer and privateNetworkClientServer capabilities defined in your package manifest
2. Calling AddMvc resulted in an error because UWP applications get compiled to Windows Runtime components and internally AddMvc resulted in Assembly.Load("UwpAspNetCore") being called which would throw an exception
3. The workaround was to initialize and DI the ApplicationPartManager before calling AddMvc, this works because Mvc then pulls it the assembly from the ServiceCollection instead
4. Next up is that you cannot have a Controller in the UWP app because controllers must be public classes and UWP will not compile a public class inheriting from Controller because it does not end up deriving from System.Object
5. To get around this issue you can put the controller in a class library, I had success with both .NETStandard2.0 and UWP class libraries
6. You do have to tell the ApplicationPartManager about your class library, I did it with manager.ApplicationParts.Add(new AssemblyPart(Assembly.Load("UwpAspNetLib"))); and it worked
7. I cannot remember why, but I also had to add UseSetting("preventHostingStartup", "true") on the WebHostBuilder to get around another issue, I think that was related to Mvc locating the controllers
8. You must also have a custom ViewsFeatureProvider for your Razor views to be found
