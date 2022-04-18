$alive = curl.exe -s "http://buildversion.local:8081/Ping" -H "accept: text/plain"
if($alive -eq "pong!")
{
	. (".\BuildWithVersioning.ps1")
	return
}

foreach($name in @("workloadsapi", "workloadsprojector", "buildversion", "cronjob", "countriesapi"))
{
	$semanticVersion = "latest"
	"Current build: ${name}:${semanticVersion}"

	docker build -f .\${name}\Dockerfile --force-rm -t ${name} .
	docker tag ${name}:latest ${env:registryhost}/${name}:${semanticVersion}
	docker push $env:registryhost/${name}:$semanticVersion
}