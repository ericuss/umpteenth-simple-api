FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

WORKDIR /src

# Copy configuration and csproj and restore as distinct layers
COPY ["Src/_configuration", "Src/_configuration/"]

COPY ["Src/Clients/Simple.Api/Simple.Api.csproj", "Src/Clients/Simple.Api/"]
COPY ["Src/Infrastructure/Simple.Infrastructure/Simple.Infrastructure.csproj", "Src/Infrastructure/Simple.Infrastructure/"]
COPY ["Src/Domain/Simple.Domain.csproj", "Src/Domain/Simple.Domain/"]
COPY ["Src/Data/Simple.Data.csproj", "Src/Data/Simple.Data/"]
RUN dotnet restore "Src/Clients/Simple.Api/Simple.Api.csproj"

COPY ["Src/Tests/Simple.UnitTests/Simple.UnitTests.csproj", "Src/Tests/Simple.UnitTests/"]
RUN dotnet restore "Src/Tests/Simple.UnitTests/Simple.UnitTests.csproj"

# Copy everything else and build
COPY Src/ Src/
RUN dotnet build Src/Tests/Simple.UnitTests/Simple.UnitTests.csproj

FROM build-env as unittests
WORKDIR Src/Tests/Simple.UnitTests
