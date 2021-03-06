#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CronJob/CronJob.csproj", "CronJob/"]
COPY ["Countries.Db/Countries.Db.csproj", "Countries.Db/"]
COPY ["Countries.Model/Countries.Model.csproj", "Countries.Model/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "CronJob/CronJob.csproj"
COPY . .
WORKDIR "/src/CronJob"
RUN dotnet build "CronJob.csproj" -c Release -o /app/build

FROM build AS publish
ARG Version
ARG Description
RUN dotnet publish "CronJob.csproj" -p:Version="$Version" -p:Description="$Description" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CronJob.dll"]