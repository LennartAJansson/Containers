foreach($name in @("workloadsapi","workloadsprojector", "cronjob"))
{
	$semanticVersion = "latest"
	"Current build: " + $name + ":" +$semantivVersion

	docker build -f .\${name}\Dockerfile --force-rm -t ${name} .
	docker tag ${name}:latest $env:registryhost/${name}:$semanticVersion
	docker push $env:registryhost/${name}:$semanticVersion
}