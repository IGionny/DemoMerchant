# DemoMerchant

![.NET workflow](https://github.com/IGionny/DemoMerchant/actions/workflows/dotnet.yml/badge.svg)

A minimal .NET9 solution to handle a common merchant domain with a design restriction: every business service must be
isolated: no direct references between services.

## How to start
1. Clone the repository
2. Open the solution with an IDE (Visual Studio, Rider, etc.)
3. Build the solution
4. Run the WebApi project
5. Open a browser and navigate to https://localhost:7127/swagger/index.html
6. You can test the API using the Swagger UI
7. You can run the tests using the Test Explorer in Visual Studio or Rider
8. You can use Postman or any other tool to test the API

OR you can execute the following commands in the terminal:

```bash
dotnet build
dotnet run --project src/DemoMerchant.WebApi/DemoMerchant.WebApi.csproj
```


## Project Structure
The solution is divided into projects: 
- Sdk: contains the domain definition, EF context 
- Services: contains the business services to interact with the domain entities
- WebApi: contains the Asp.NET Core Web API project to expose API-REST endpoints to interact with the entities
- Tests: contains the unit tests for the domain and the API

## Domain
The domain is composed by:

- Customer: Represents a customer entity. It has a name, email and a list of addresses.
- Address: Represents an address entity.
- Product: Represents a product entity. It has a name and a category.
- ProductCategory: Represents a product category entity.
- Order: Represents an order entity. It has a customer, a list of order items and a total amount.

Value objects:
- OrderItem is a value object that represents an item in an order. It has a product, a quantity and a price.
- Address is NOT a value object in this project


## Database
For the sake of simplification, the project uses Sqllite as database for the application and in-memory database for integration tests.
The database is generated and seeded with some data at startup.
The files of Sqllite are store under ~/App_Data/Data folder.

## API-REST
The WebApi project exposes API-REST endpoints to interact with the domain entities.
I'm using the new OpenApi library of .NET 9 to generate the json document and Swagger for the presentation.

You can see the Swagger UI at the following URL: https://localhost:7127/swagger/index.html \
(change the port accordingly with your configuration)

Note: there is no authentication mechanism in this demo.
