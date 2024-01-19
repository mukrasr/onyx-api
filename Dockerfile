FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY source/Onyx.Api Onyx.Api
RUN dotnet restore Onyx.Api

# Unit test runner
FROM build AS unittestrunner
WORKDIR /src

COPY source/Onyx.Api.UnitTests Onyx.Api.UnitTests
COPY source/Onyx.Api Onyx.Api
RUN dotnet restore Onyx.Api.UnitTests && \
    dotnet build --no-restore Onyx.Api.UnitTests
CMD ["dotnet", "test", "Onyx.Api.UnitTests"]

# Integration test runner
FROM build AS integrationtestrunner
WORKDIR /src

COPY source/Onyx.Api.IntegrationTests Onyx.Api.IntegrationTests
COPY scripts/run-integration-tests.sh .
RUN dotnet restore Onyx.Api.IntegrationTests && \
    dotnet build --no-restore Onyx.Api.IntegrationTests && \
    chmod +x run-integration-tests.sh
CMD ["./run-integration-tests.sh"]

# Publish
FROM build AS publish
RUN dotnet publish --no-restore Onyx.Api -c Release -o /app/publish

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

RUN apt-get update && \
    apt-get install -y curl
COPY --from=publish /app/publish .
CMD ["dotnet", "Onyx.Api.dll"]