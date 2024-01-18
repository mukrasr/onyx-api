#!/bin/bash

set -euo pipefail

export JWT_TOKEN=$(cd /app/Onyx.Api;dotnet user-jwts create --audience "http://localhost:5271" -o token)
dotnet test Onyx.Api.IntegrationTests