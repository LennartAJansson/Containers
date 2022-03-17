foreach($name in @("workloadsapi", "workloadsprojector", "buildversion", "cronjob"))
{
	$semanticVersion = "latest"
	"Current build: ${name}:${semanticVersion}"

	docker build -f .\${name}\Dockerfile --force-rm -t ${name} .
	docker tag ${name}:latest ${env:registryhost}/${name}:${semanticVersion}
	docker push $env:registryhost/${name}:$semanticVersion
}