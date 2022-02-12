docker pull nats:latest
docker tag nats:latest $env:registryhost/nats:latest
docker push $env:registryhost/nats:latest
