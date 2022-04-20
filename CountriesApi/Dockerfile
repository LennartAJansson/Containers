#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CountriesApi/CountriesApi.csproj", "CountriesApi/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Common.AspNet/Common.AspNet.csproj", "Common.AspNet/"]
COPY ["Countries.Model/Countries.Model.csproj", "Countries.Model/"]
RUN dotnet restore "CountriesApi/CountriesApi.csproj"
COPY . .
WORKDIR "/src/CountriesApi"
RUN dotnet build "CountriesApi.csproj" -c Release -o /app/build

FROM build AS publish
ARG Version
ARG Description
RUN dotnet publish "CountriesApi.csproj" -p:Version="$Version" -p:Description="$Description" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountriesApi.dll"]