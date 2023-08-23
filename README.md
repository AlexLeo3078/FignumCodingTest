## FignumCodingTest

## Technical specification
This project requires .NET 7 SDK. The solution `FignumCodingTest.sln` contains two projects `FignumCodingTest` and `TestFignumCodingTest.Test`.
The frontend we will use to test is Swagger UI.


## Alex Leo Notes
No authentication or authorization has been implemented - potentially we could use Azure API Management AAD.
We might need to implement some sort of caching in order to improve performance  - store request into database.
As a proof of concept, I have added some logging - output to console. Moving forward a persistent logging must be implemented - perhaps using Serilog, Log4Net or any other alternatives.