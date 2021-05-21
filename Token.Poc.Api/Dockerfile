#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Token.Poc.Api/Token.Poc.Api.csproj", "Token.Poc.Api/"]
RUN dotnet restore "Token.Poc.Api/Token.Poc.Api.csproj"
COPY . .
WORKDIR "/src/Token.Poc.Api"
RUN dotnet build "Token.Poc.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Token.Poc.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Token.Poc.Api.dll"]