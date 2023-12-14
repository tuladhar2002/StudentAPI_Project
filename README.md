# Student WEB API

## Environment setup

1. Make sure to install .NET 7 SDK (https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
2. Install dotnet tools if not installed yet:

```
 dotnet tool install --global dotnet-ef --version 7.0.14

```

3. Make sure Docker is installed & running in background (Which hosts our API service, API Database and Auth Database for our service)
   > It's okay for no containers to be running right now within docker

## Build Application

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

# Setup For API services

Once the application can build succesfully and application is running, we can access it through chrome at port 5288:(http://localhost:5288/swagger/index.html).
For testing in POSTMAN, simply copy the JSON StudentAPI collection file in the root directory of this project and paste it in postman.

## Register and Login

The Student API web service can handle client Login and Registry. The service is built around ASP.NET Core JWT Authentication flow[Know More](https://dev.to/fabriziobagala/jwt-authentication-in-aspnet-13ma), hence a client would need to Register and login in order to get authorized for all the Web API functionalities.

### Register

For registering client needs to put in 3 informations:

1.  Username (Any form of email address)
2.  Password (Minimum 6 characters with Uppercase, Lowercase & Numeric characters )
3.  Role (Either Admin or Student)

After Executing, Should get a 200 Response back with comment Succesfully Registerd.

### Login

Once registered, User can Login to get the JWT token.

For testing through Swagger, Copy and paste the token within the Authorize Header in format "Bearer <paste JWT Token we got >".

For testing through POSTMAN,

1. Click on the StudentAPI_Test Collection in Postman
2. Click Authorization (right besides Overview panel)
3. Select Type API Key
4. For Key type in -> Authorization
5. For Value type in -> Bearer //Copied Jwt Token
6. Add to -> Header

Then save it.

Now we should be set for sending requests within Swagger or Postman, whichever preffered.

> Note: The token expires in 30 mins, hence will need to login again to get new JWT token

## Student API Functionalities

We can apply basic CRUD functionalities now that we are logged in within the Student API service, however it depends on the Role assigned:
Admin -> Should be able to work with all the CRUD functionalities as is authorized.
Student -> Can only implement GetAllStudents and GetStudentById functionalities as they are not authorized for all the tasks.

### GetAllStudents

Route to get all Students: http://localhost:5288/api/Student

Route to get or filter Student by Name: http://localhost:5288/api/Student?filterOn=Name&filterQuery=Name

> (Replace "Name" (Name you want to search) for filterQuery=Name property in URL)

Route to sort all students by Name in Ascending or Descending: http://localhost:5288/api/Student?sortBy=Name&isAscending=true

> (put "true" or "false" in isAscending=true to sort accordingly)

### CreateStudent

Route: POST request http://localhost:5288/api/Student

Initially there is nothing in the database so we can start storing in Students.

name -> name of Student <br>
email -> valid email address<br>
nationality -> any country<br>
isEnabled -> true or false (Based on whether you want to enable this student or disable)<br>
ClassId -> Choose a Guid Class Id based on the table below<br>

| Id                                   | Name                               |
| ------------------------------------ | ---------------------------------- |
| 0d06acb7-5ad4-458c-b26e-a3b390994335 | Object Oriented Programming        |
| 0d06acb7-5ad4-458c-b26e-a3b390994447 | Calculus                           |
| 0d06acb7-5ad4-458c-b26e-a3b390995678 | Data Structure and Algorythm       |
| 237b0357-dfb0-49e9-90a9-0a25933c439e | Introduction to programming        |
| 5c47bab3-293b-4b64-8860-bdeb1516ed43 | Web Programming and Implementation |
| 69e61e7e-6b80-4cde-b016-3a253e9a0c45 | Java Programming                   |

RankingId -> Choose a Guid Ranking Id based on table below

| Id                                   | Name            |
| ------------------------------------ | --------------- |
| 044677d4-ec73-4c97-a007-86530e9c0769 | Needs more work |
| b9de3c6d-55e8-4857-9e8e-87a8c35f5ae0 | Brilliant       |
| df1228e3-b8e6-4bd5-95a3-5b54e19b3a88 | Average         |

Should respond with 201 status code with the created object Student.

> If you put in invalid or unknown Guid, ModelValidation should handle it and explain no such Id exists.

### GetStudentById

Route: Get Request http://localhost:5288/api/Student/{ID}

> Replace {ID} with actual Student ID you want to get

### GetStudentById

Route: PUT request http://localhost:5288/api/Student/{ID}

> Replace {ID} with actual Student ID you want to update

pass in body in raw JSON format similar to CreateStudent

### DeleteStudent

Route: Delete request http://localhost:5288/api/Student/{ID}

> Replace {ID} with actual Student ID you want to delete

### EnableDisableStudent

Route: PATCH request http://localhost:5288/api/Student/{ID}?enableStudent=true

> Replace {ID} with actual Student ID you want to enable or disable
> change enableStudent property to true or false based on if you want to enable or disable student.
