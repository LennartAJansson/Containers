#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WorkloadsApi/WorkloadsApi.csproj", "WorkloadsApi/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Common.AspNet/Common.AspNet.csproj", "Common.AspNet/"]
COPY ["NATS.Extensions.DependencyInjection/NATS.Extensions.DependencyInjection.csproj", "NATS.Extensions.DependencyInjection/"]
COPY ["Workloads.Contract/Workloads.Contract.csproj", "Workloads.Contract/"]
COPY ["WorkloadsApi.Mediators/WorkloadsApi.Mediators.csproj", "WorkloadsApi.Mediators/"]
COPY ["Workloads.Model/Workloads.Model.csproj", "Workloads.Model/"]
RUN dotnet restore "WorkloadsApi/WorkloadsApi.csproj"
COPY . .
WORKDIR "/src/WorkloadsApi"
RUN dotnet build "WorkloadsApi.csproj" -c Release -o /app/build

FROM build AS publish
ARG Version
ARG Description
RUN dotnet publish "WorkloadsApi.csproj" -p:Version="$Version" -p:Description="$Description" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkloadsApi.dll"]