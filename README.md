# UwpAspNetCore
An attempt to host an ASP.NET Core site in a UWP app

## Notes

I'll clean these notes up once everything is working, for now this is just a scratchpad for me to keep notes on as I work through things

1. You must have the internetClientServer and privateNetworkClientServer capabilities defined in your package manifest
2. Calling AddMvc resulted in an error because UWP applications get compiled to Windows Runtime components and internally AddMvc resulted in Assembly.Load("UwpAspNetCore") being called which would throw an exception
3. The workaround was to initialize and DI the ApplicationPartManager before calling AddMvc, this works because Mvc then pulls it the assembly from the ServiceCollection instead
4. Next up is that you cannot have a Controller in the UWP app because controllers must be public classes and UWP will not compile a public class inheriting from Controller because it does not end up deriving from System.Object
5. To get around this issue you can put the controller in a class library, I had success with both .NETStandard2.0 and UWP class libraries
6. You do have to tell the ApplicationPartManager about your class library, I did it with manager.ApplicationParts.Add(new AssemblyPart(Assembly.Load("UwpAspNetLib"))); and it worked
7. I cannot remember why, but I also had to add UseSetting("preventHostingStartup", "true") on the WebHostBuilder to get around another issue, I think that was related to Mvc locating the controllers
8. This is where I'm stuck, at this point requests make it to Kestrel, the request is handed off to Mvc routing which locates the controller and invokes the controller method, but Razor blows up when returning the view.  I think this is down to the fact that Mvc views must be compiled so I've been playing with precompilation on build and have it generating the *.Views.dll file, but the view is not located at runtime...
