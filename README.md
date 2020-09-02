# AglDeveloperTest

Phil's solution to the AGL Developer Test at http://agl-developer-test.azurewebsites.net/

Some notes:

I approached this as primarily a data transformation exercise.
There are two external interfaces: data input from the API call and data output, which I will write to the Console.
The conversion of the data from input to output is the core logic, which I can verify with unit tests.
I have also added an integration test that proves the data can be fetched and processed successfully from the API call.

This is a Console app - the simplest kind of .net project.
I haven't used any Dependency Injection (DI) because I don't think it's warranted for such a simple app whose run time is so brief.
Otherwise, I might have used the new "worker service" project template, since this would have provided me with DI capabilities out of the box.

For now, having all classes at the top level of the project keep things simple and discoverable.
With more time (or if this mini app grew any larger), I would add more structure with namespaces nested within the main project.

I am well aware of the problems with using HttpClient directly (in PeopleJsonReader).
See https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
However in the case of a simple console app which makes one API call and then exits, it is a non-issue.
For longer running processes, the HttpClientFactory should be used, which again would lead us towards DI and the "worker service" approach.
