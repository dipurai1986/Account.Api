#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API/Account.API/Account.API.csproj", "API/Account.API/"]
COPY ["core/Account.Application/Account.Application.csproj", "core/Account.Application/"]
COPY ["Account.Domain/Account.Domain.csproj", "Account.Domain/"]
COPY ["Account.Infrastructure/Account.Infrastructure.csproj", "Account.Infrastructure/"]
RUN dotnet restore "API/Account.API/Account.API.csproj"
COPY . .
WORKDIR "/src/API/Account.API"
RUN dotnet build "Account.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Account.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.API.dll"]