# jwt-api
jwt-api- A simple JSON Web Token API example using Containerized dotnetcore 3.1

### Dependencies to run locally
- Dotnet Core 3.1+
- Docker 19.0.3.1

### appsettings.Deploy.json
The Dockerfile has a dependency on a file called appsettings.Deploy.json and won't build without it. I created this file but left it out of source control to keep my deployed secrets, keys, and test user accounts out of source control. you will need to copy the existing appsettings file into a appsettings.Deploy.json before trying to build the docker image.

### Running Locally
**Note**: If using Docker Desktop for Windows, make sure you are pointing to Linux Containers.
```
git clone https://github.com/beesona/jwt-api.git
cd jwt-api
dotnet restore
dotnet run
```
- Navigate to https://localhost:5001 to access the page.
- Any src changes will need to stop the express server (CTRL+C) and rerun ```dotnet run```

### Deploying Containers and Running
**Note**: If using Docker Desktop for Windows, make sure you are pointing to Linux Containers.
```
cd <project-root-dir>
dotnet restore
dotnet publish -c Release
docker-compose up
```
- Navigate to localhost:5000 to access the page. (**NOTE**: The docker-compose.yml points the dotnetcore app to 5000 instead of 5001)
- Any changes will require the following (ran from the project root)
```
docker-compose down
dotnet publish -c Release
docker-compose build
docker-compose up
```

### Using the application
There is one endpoint (currently) available for validating a user.
1. /api/user/authenticate
  - use this endpoint to get a JWT by passing in a valid user name and PW in the body of a POST request
  - The body must be JSON, refer to the AuthenticateModel.cs for the format
  - Sample users are stored in the application.json file.
  - Sharing the secret between this app and other APIs and utilizing Microsoft.AspNetCore.Authentication.JwtBearer will allow apps to share a JWT.
