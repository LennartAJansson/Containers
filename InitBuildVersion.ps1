$name = "buildversion"
$semanticVersion = "latest"

"Current build: " + $name
docker build -f .\${name}\Dockerfile --force-rm -t ${name} .
docker tag ${name}:latest $env:registryhost/${name}:$semanticVersion
docker push $env:registryhost/${name}:$semanticVersion

"Current deploy: " + $name
kubectl delete -k ./deploy/$name
kubectl apply -k ./deploy/$name
kubectl set image -n ${name} deployment/${name} ${name}=$env:registryhost/${name}:${semanticversion}
