#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Src/Clients/Simple.Api/Simple.Api.csproj", "Src/Clients/Simple.Api/"]
COPY ["Src/Infrastructure/Simple.Infrastructure.csproj", "Src/Infrastructure/Simple.Infrastructure/"]
RUN dotnet restore "Src/Clients/Simple.Api/Simple.Api.csproj"
COPY . .
WORKDIR "/src/Src/Clients/Simple.Api"
RUN dotnet build "Simple.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simple.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simple.Api.dll"]