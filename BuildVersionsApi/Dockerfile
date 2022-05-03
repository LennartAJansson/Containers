#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BuildVersionsApi/BuildVersionsApi.csproj", "BuildVersionsApi/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Common.AspNet/Common.AspNet.csproj", "Common.AspNet/"]
RUN dotnet restore "BuildVersionsApi/BuildVersionsApi.csproj"
COPY . .
WORKDIR "/src/BuildVersionsApi"
RUN dotnet build "BuildVersionsApi.csproj" -c Release -o /app/build

FROM build AS publish
ARG Version
ARG Description
RUN dotnet publish "BuildVersionsApi.csproj" -p:Version="$Version" -p:Description="$Description" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuildVersionsApi.dll"]