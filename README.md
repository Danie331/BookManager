### Description
Maintain an inventory of books exposed via a RESTful API and swagger UI

### Back-end setup
- Clone and build the solution
- Ensure that the API project is set as the startup project
- If the default launch profile gives a certificate error, switch to iis-express

### Data source
- Flat file located in the web root by default
- This repo currently ships with a sample books.json file containing random test data. To start from a clean slate, please delete this file and it will be automatically recreated.

### Tests
- NUnit test project located under UnitTests\Tests can be run directly from the project
- Currently only supports validation unit tests for model data
- E2E service tests to be added later

### Features
- Swagger UI enabled for testing purposes
- MediatR used for message dispatch via CQRS
- FluentValidation to validate incoming DTOs
- DI enabled by default framework classes
- Separate configuration project added for confgurablity of data sources should this be diversified in future

### End
