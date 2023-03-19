
# AcmeFlights Assignment

![alt text](/acmeflights-image.jpg)

This repository is the starting point for our .NET assignment. You are going to create a flights booking engine. API clients should be able to search available flights and book them. **The purpose of the assignment is to see if there is a match between our problem solving and coding style**

We like to use some modern best practices in this assignment and try to point you in a certain direction. But don't take it too strictly. If you are struggling with something, just let it go and shine at the parts you are more familiar with. We are not expecting everyone to know everything ;) The same is true the other way around. When you think there's a more clever solution, just do it and argue why it's better.

## Excercise "requirements"

- Implement the following features:
    - **Feature 1**: Search the available flights for a destination
        - You can search available flights to a specific destination
        - Does not include flights that are not available (has no rates)
        - For each found flight show:
          - Departure airport code
          - Arrival airport code
          - Departure datetime
          - Arrival datetime
          - Lowest Price
          
    - **Assumption made in developing Feature 1**: Search the available flights for a destination
	    - *Airport Code ( ex - "AMS") are available in the client, the client will pick one of the Airport codes.*
	    - *The filter property that will be used is the Airport Code.*
	    - *Flights that are not available are assumed as flights without any rating at all or, flights that doesn't have availability at any of the rates.* 
    - **Feature 2**: Placing an order
        - Must have endpoints to create an order
        - Must use the Ordering domain (`Domain/Aggregates/OrderAggregate/`)
        - Must be able to fill the order with the (just the necessary) details, while still in draft state
        - Respects the business logic
        
     - **Assumption made in developing Feature 2**: Placing an order
	    - *Orders and Order Items are equivalent of Booking Package & Booking Items. A Order Consist of Customer Id, Tax Rate, Total Price ( Customer has not been added as a Domain Entity)*
	    - *Under One Order there can be multiple Order Items . A Order Item is similar to a Flight which has a particular rate Id, flight Id, Unit and Unit Price.* 
	    - *Example Scenario - Customer  John , books a flight form AMS to DUB and the round trip. So For John's Order there are 2 flights. ( One from AMS to DUB and the other from DUB to AMS)*
	    - *All Orders initial status is Draft Status. When in the draft Status, as for the current implementation on the number of units ( seats) can be changed. But this can be extended into other properties as well.*
	     
    - **Feature 3**: Confirming an order
        - Must be able to confirm the order
        - When an order is confirmed, the any ordered rates should lower their availability by the quantity ordered
        - Notifies the customer about the confirmed order (fake the notification with a `Console.WriteLine`)
        - Its not possible to make changes to a confirmed order (guarded by domain)
- **Architecture requirements**: Apply the following practices throughout the project
    - Domain Driven Design
    - CQRS
    - Mediator pattern (Using [MediatR](https://github.com/jbogard/MediatR))
    - Persistence ignorance
    - SOLID
- **Other**
    - The project must be runnable on MacOS and Windows
    - If there are additional steps for us to take to run it, please write them down

> **Do not worry** if you are not familiar, or are struggling with one or more of the requirements. Just do the best you can. Not being able to fulfill all requirements does not mean you "failed".
> 
> This assignment is intended to be a conversation starter. We would still love to see your solution, even if you were not able to fulfill all requirements! We will discuss the assignment afterwards, so there's always the opportunity to explain the decisions made

## Prerequisities

- Docker Desktop
- .NET 6 SDK

## Getting started

- Start the Postgres database with `docker-compose up -d` (the application is already configured properly, but if you want to connect to the db directly you can see the credentials in the `docker-compose.yml` file)
- You can now run the API project and everything should work. Upon start the application will run the migrations and seed data to the database.

## References 

- https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/
- https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/
