# DemoMerchant
A simple .NET solution to handle a common merchant domain.

## Project Structure
The solution is divided into projects: 
- Sdk: contains the domain definition, EF context 
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

