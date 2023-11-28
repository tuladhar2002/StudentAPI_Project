**Environment setup**

1. Make sure to install .NET 7 SDK (https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
2. Install dotnet tools if not installed yet (dotnet tool install --global dotnet-ef)
3. Make sure Docker is running in background (Which is hosting our service, Api Database and Auth Database)

**Build Application**

1. Clone the github repo into local VS code
2. Build the application to check for any errors:

   ```
       dotnet build
   ```

3. Updating Database with Migrations
   Within terminal, run:

   ```
    dotnet ef database update --context "StudentAPIDbContext"
    dotnet ef database update --context "StudentAPIAuthDbContext"

   ```

4. Run Docker Containers
   Within terminal, run:

   ```
    docker-compose up --build

   ```

5. Run Application once containers are running
   ```
   dotnet run
   
   ```
