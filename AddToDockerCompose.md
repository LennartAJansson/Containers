Docker-Compose.yml:
-------------------
```
version: '3.4'

services:
  workloadsapi:
    image: ${DOCKER_REGISTRY-}workloadsapi
    build:
      context: .
      dockerfile: WorkloadsApi/Dockerfile

  workloadsprojector:
    image: ${DOCKER_REGISTRY-}workloadsprojector
    build:
      context: .
      dockerfile: WorkloadsProjector/Dockerfile

  nats:
    container_name: nats
    image: nats:latest
    command: -js -m 8222
```  	
Docker-Compose.override.yml:
----------------------------
```
version: '3.4'

services:
  workloadsapi:
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:443;http://+:80
    ports:
      - "80"
      - "443"
    volumes:
      - ${APPDATA}/ASP.NET/Https:/root/.aspnet/https:ro

  workloadsprojector:
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - DOTNET_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:443;http://+:80
    ports:
      - "80"
      - "443"
    volumes:
      - ${APPDATA}/ASP.NET/Https:/root/.aspnet/https:ro

  nats:
    ports:
      - "4222:4222"
      - "6222:6222"
      - "8222:8222"
```  