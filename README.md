#Student web API

##Environment setup##

1. Make sure to install .NET 7 SDK (https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
2. Install dotnet tools if not installed yet:

```
 dotnet tool install --global dotnet-ef --version 7.0.14

```

3. Make sure Docker is installed & running in background (Which hosts our API service, API Database and Auth Database for our service)
   > It's okay for no containers to be running right now within docker

##Build Application##

1. Clone the github repo into local VS code
2. Build the application to check for any errors:

   ```
    dotnet build
   ```

3. Run Docker Containers
   Within terminal, run:

   ```
    docker-compose up --build

   ```

4. Updating Database with Migrations
   Within terminal, run:

   ```
    dotnet ef database update --context "StudentAPIDbContext"
    dotnet ef database update --context "StudentAPIAuthDbContext"

   ```

5. Run Application once containers are running

   ```
   dotnet run

   ```
