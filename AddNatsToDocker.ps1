docker pull nats:latest
docker run -d --name nats -p 4222:4222 -p 6222:6222 -p 8222:8222 nats -js -m 8222