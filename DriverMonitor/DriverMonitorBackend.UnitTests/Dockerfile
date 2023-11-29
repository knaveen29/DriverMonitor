FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0 AS build-env
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install --global dotnet-ef --version 6.*

# Use the .NET Core SDK image for building and running tests
FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0 AS build-env

# Set the working directory
WORKDIR /app

# Copy the solution file and restore dependencies
COPY DriverMonitor.sln .
COPY DriverMonitorBackend/DriverMonitorBackend.csproj DriverMonitorBackend/
RUN dotnet restore

# Copy the entire solution
COPY . ./

# Build and run tests
RUN dotnet test

# Build runtime image
FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0

# Set the working directory for the runtime image
WORKDIR /app

# Copy the built binaries from the build image
COPY --from=build-env /app/DriverMonitorBackend/bin/Release/net6.0/publish ./

# Run the unit tests
CMD ["dotnet", "test", "DriverMonitorBackend.UnitTests.dll"]