#!/bin/bash

set -e
dotnet restore

until dotnet ef database update -p DriverMonitorBackend.csproj; do
>&2 echo "DB is starting up"
sleep 1
done

>&2 echo "DB is up - executing command"
dotnet watch -p DriverMonitorBackend.csproj run