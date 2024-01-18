# Onyx Api

The tests that validate this api can be run either via docker compose or directly.

## Docker compose

This is the recommended method. The pre-requisite to use this method docker. Firstly you need to create a JWT token for the api and store it in a .env file as follows

```bash
echo JWT_TOKEN=$(cd source/Onyx.Api;dotnet user-jwts create --audience "http://localhost:5271" -o token) > .env
```

Once this is done, you can now run the tests

```bash
docker compose up --build integrationtests unittests
```

The rendered logs should provide the test results for both the integration and unit tests, after which the command should exit with code 0.

## Directly

The pre-requisite to use this method is dotnet 8. Run the intergration and unit tests as follows:

### Running integration tests

Open this folder in two terminals and start the api in the first one as follows:

```bash
export ASPNETCORE_ENVIRONMENT=Development
export ASPNETCORE_URLS=http://localhost:5271
dotnet run --no-launch-profile --project source/Onyx.Api
```

Once the api is up and running, kick off the integration tests in the second terminal as follows:

```bash
export API_PATH=http://localhost:5271
export JWT_TOKEN=$(cd source/Onyx.Api;dotnet user-jwts create --audience "http://localhost:5271" -o token)
dotnet test source/Onyx.Api.IntegrationTests
```

The rendered logs should provide the test results, after which the command should exit with code 0.

### Running unit tests

Open this folder in a terminal and run the unit tests using the following command:

```bash
dotnet test source/Onyx.Api.UnitTests
```

The rendered logs should provide the test results, after which the command should exit with code 0.
