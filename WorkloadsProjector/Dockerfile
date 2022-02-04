#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WorkloadsProjector/WorkloadsProjector.csproj", "WorkloadsProjector/"]
COPY ["Workloads.Model/Workloads.Model.csproj", "Workloads.Model/"]
COPY ["WorkloadsProjector.Mediators/WorkloadsProjector.Mediators.csproj", "WorkloadsProjector.Mediators/"]
COPY ["Workloads.Contract/Workloads.Contract.csproj", "Workloads.Contract/"]
COPY ["NATS.Extensions.DependencyInjection/NATS.Extensions.DependencyInjection.csproj", "NATS.Extensions.DependencyInjection/"]
RUN dotnet restore "WorkloadsProjector/WorkloadsProjector.csproj"
COPY . .
WORKDIR "/src/WorkloadsProjector"
RUN dotnet build "WorkloadsProjector.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkloadsProjector.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkloadsProjector.dll"]