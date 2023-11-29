#!/bin/bash
set -eu -o pipefail

dotnet restore ./DriverMonitorBackend.UnitTests.csproj
dotnet test ./DriverMonitorBackend.UnitTests.csproj