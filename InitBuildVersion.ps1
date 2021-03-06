$name = "buildversionsapi"
$description = "Initial version"
$version = "0.0.0.1"
$semanticVersion = "0.0.0.1"

"Current build: " + $name
docker build -f .\${name}\Dockerfile --force-rm -t ${name} --build-arg Version="${version}" --build-arg Description="${description}" .
docker tag ${name}:latest ${env:registryhost}/${name}:${semanticVersion}
docker push ${env:registryhost}/${name}:${semanticVersion}


"Current deploy: " + $name
kubectl apply -k ./deploy/$name
kubectl set image -n ${name} deployment/${name} ${name}=$env:registryhost/${name}:${semanticversion}

if([string]::IsNullOrEmpty($env:AGENT_NAME))
{
	git checkout deploy/${name}/kustomization.yaml
}