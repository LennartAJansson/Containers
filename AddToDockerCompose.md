Docker-Compose.yml:
-------------------
```
services:
  nats:
    container_name: nats
    image: nats:latest
    command: -js -m 8222
```  	
Docker-Compose.override.yml:
----------------------------
```
services:
  workloadsapi:
    environment:
      - NATS__Servers__0=nats://nats:4222
      - NATS__Url=nats://nats:4222
    volumes:
      - C:/Data/Docker:/data/persisted/workloadsapi:rw

  workloadsprojector:
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - DOTNET_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - NATS__Servers__0=nats://nats:4222
      - NATS__Url=nats://nats:4222
    ports:
      - "80"
      - "443"
    volumes:
      - ${APPDATA}/ASP.NET/Https:/root/.aspnet/https:ro
      - C:/Data/Docker:/data/persisted/workloadsapi:rw

  nats:
    ports:
      - "4222:4222"
      - "6222:6222"
      - "8222:8222"
```  