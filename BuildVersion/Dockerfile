#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BuildVersion/BuildVersion.csproj", "BuildVersion/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "BuildVersion/BuildVersion.csproj"
COPY . .
WORKDIR "/src/BuildVersion"
RUN dotnet build "BuildVersion.csproj" -c Release -o /app/build

FROM build AS publish
ARG Version
ARG Description
RUN dotnet publish "BuildVersion.csproj" -p:Version="$Version" -p:Description="$Description" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuildVersion.dll"]