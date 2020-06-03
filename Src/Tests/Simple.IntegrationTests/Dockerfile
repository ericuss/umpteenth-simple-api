FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443


# Copy csproj and restore as distinct layers

WORKDIR /src

COPY ["Src/_configuration", "Src/_configuration/"]

COPY ["Src/Clients/Simple.Api/Simple.Api.csproj", "Src/Clients/Simple.Api/"]
COPY ["Src/Infrastructure/Simple.Infrastructure/Simple.Infrastructure.csproj", "Src/Infrastructure/Simple.Infrastructure/"]
COPY ["Src/Domain/Simple.Domain.csproj", "Src/Domain/Simple.Domain/"]
COPY ["Src/Data/Simple.Data.csproj", "Src/Data/Simple.Data/"]
RUN dotnet restore "Src/Clients/Simple.Api/Simple.Api.csproj"

COPY ["Src/Tests/Simple.IntegrationTests/Simple.IntegrationTests.csproj", "Src/Tests/Simple.IntegrationTests/"]
RUN dotnet restore "Src/Tests/Simple.IntegrationTests/Simple.IntegrationTests.csproj"

# Copy everything else and build
COPY Src/ Src/
RUN dotnet build Src/Tests/Simple.IntegrationTests/Simple.IntegrationTests.csproj

FROM build-env as integrationtests
WORKDIR Src/Tests/Simple.IntegrationTests
