# Difference between `IOptions`, `IOptionsSnapshot` and `IOptionsMonitor`

The `IOptions` interface works as a singleton object and therefore can be injected into all services with any lifetime. If you change the value of the `appsettings.json` file after running the program and use this interface to read the data, you will not see your changes, because this service is Singleton and is only validated at the time of running the program. If the program does not run again, it shows the same initial values.

The `IOptionsSnapshot` interface works as an object with scoped lifetime. That is, for each request, it re-reads the data from `appsettings.json` and provides it to us. This interface cannot be used in Singleton lifetime services.

The `IOptionsMonitor` interface works as a Singleton, but the difference with the `IOptions` interface is that if a change is made to the `appsettings.json` file, new changes can be received by the `OnChange` method. 