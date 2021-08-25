# Pluto Rover Challenge

## Requirements
- .NET 5 SDK or Docker

***
## Design Approach

The kata was developed using a DDD(Domain-driven-design) approach following the principles of an enriched domain.  
In this challenge, the purpose was only developed the Application layer and the Domain layer, with all necessary unit tests.   

Design decision notes:

- Presentation layer and repository layer was descoped by assuming the challenge is only a kata purpose
- Rover it's an aggregate domain and the "Pluto" navigation map is loaded by a NavigationMap entity
- Rotation and move calculations logic belongs to the Rover domain layer
- RoverService allows operating over rover state previous saved
***

## Testing

The challenge was developed using a TDD, therefore both layers of application and domain have unit tests exemplifying and granting all excepted behaviors.
On the Application service, was used a Mock library in order to decouple the tests of repository implementation (following the dependency injection pattern).

### How to run unit tests locally
* Jump to `src` folder
* Execute the following commands  
````
dotnet restore
dotnet test
````
***

### How to run unit tests on docker-compose
* On project root folder
* Execute the following command  
````
docker-compose run tests
````
***

## Author

- @asantoz
