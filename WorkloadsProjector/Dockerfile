#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WorkloadsProjector/WorkloadsProjector.csproj", "WorkloadsProjector/"]
COPY ["Workloads.Model/Workloads.Model.csproj", "Workloads.Model/"]
COPY ["WorkloadsProjector.Mediators/WorkloadsProjector.Mediators.csproj", "WorkloadsProjector.Mediators/"]
COPY ["Workloads.Db/Workloads.Db.csproj", "Workloads.Db/"]
COPY ["Workloads.Contract/Workloads.Contract.csproj", "Workloads.Contract/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Common.AspNet/Common.AspNet.csproj", "Common.AspNet/"]
COPY ["NATS.Extensions.DependencyInjection/NATS.Extensions.DependencyInjection.csproj", "NATS.Extensions.DependencyInjection/"]
RUN dotnet restore "WorkloadsProjector/WorkloadsProjector.csproj"
COPY . .
WORKDIR "/src/WorkloadsProjector"
RUN dotnet build "WorkloadsProjector.csproj" -c Release -o /app/build

FROM build AS publish
ARG Version
ARG Description
RUN dotnet publish "WorkloadsProjector.csproj" -p:Version="$Version" -p:Description="$Description" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkloadsProjector.dll"]