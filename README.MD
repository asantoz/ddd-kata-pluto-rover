# Pluto Rover Challenge

## Requirements
- .NET 5 SDK or Docker

***
## Design Approach

The kata was developed using a DDD(Domain-driven-design) approach following the principles of a enriched domain.  
In this challenge purpose was only developed the Application layer and the domain layer, with all necessary unit tests.   

Design decision notes:

- Presentation layer and repository layer was descoped by assuming the challenge is only a kata purpose
- Rover it's a aggregate domain and the "Pluto" map is loaded by a NavigationMap entity
- Rotation and move calculations logic belongs to Rover domain layer
- RoverService allows operate over rover state previous saved
***

## Testing

The challenge was developed using a TDD, therefore both layers of application and domain have unit tests exemplifing and granting all excepted behaviours.
On application service was used a Mock library in order to decouple the tests of repository implementation (following an dependency injection pattern) 

### How to run unit tests locally
* Jump to `src` folder
* Execute the following commands  
````
dotnet restore
dotnet test
````
***

### How to run unit tests on docke-compose
* On project root folder
* Execute the following commands  
````
docker-compose run tests
````
***

## Author

- André Santos - asantoz@gmail.com
