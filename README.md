# PetsAlone
A web-based system to help owners raise awareness of their missing pets.

### Prerequisites
Please make sure you have installed [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

### Getting Started
Follow the steps below to get the project up and running:
1. Clone [this repository](https://github.com/sathish-nagula/PetsAlone) in IDE like Visual Studio
2. Build the solution
3. Run unit tests
4. Run (F5)

## Background

### User stories
1) As anonymous users, the users will be able to see all missing pets added in the system, so that they can help to reunite a missing pet with its owner.  
2) Authenticated users will be able to add (create) a new missing pet on the system so that they can raise awareness of a missing pet.

### Acceptance criteria

1.  The home page of the site will show all the missing pets, with the most recently added pets listed first.
2.  A user will be able to filter the pets that have gone missing by animal type (dog, cat, ferret etc).
3.  A new page, only accessible by an authenticated user, which will be able to create a new missing pet.

### Notes
- The system currently interacts with auto-genetated values in place of a ‘real’ database, however, this will be extedned to use in-memory/real database. 
- For an authenticated user you can log in with the following credentials:  
Username: elmyraduff  
Password: MontanaMax!!
