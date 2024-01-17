# Onyx Api

The tests against this api can be run either via docker compose or directly.

## Docker compose

This is the recommended method. The pre-requisite to use this method docker. Run as follows:

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
dotnet test source/Onyx.Api.IntegrationTests
```

The rendered logs should provide the test results, after which the command should exit with code 0.

### Running unit tests

Open this folder in a terminal and run the unit tests using the following command:

```bash
dotnet test source/Onyx.Api.UnitTests
```

The rendered logs should provide the test results, after which the command should exit with code 0.
