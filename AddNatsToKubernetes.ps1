if ($env:registryhost -gt "")
{
	$registryhost = $env:registryhost
}
else
{
	$port = (docker port registry).Split(':')[1]
	$registryhost = "registry:$($port)"
}

# Pull, retag and push NATS/Jetstream to your repository
docker pull nats:latest

docker tag nats:latest $registryhost/nats:latest

docker push $registryhost/nats:latest

kubectl apply -k ./deploy/nats

"Add following to C:\Windows\System32\drivers\etc\hosts: "
"127.0.0.1 nats"
"127.0.0.1 nats.local"
""
"You should now be able to surf to:"
" http://nats.local:8222/"