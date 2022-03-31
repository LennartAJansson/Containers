#Assumes you have the github project buildversion running on your localhost on port 9000
#https://github.com/LennartAJansson/BuildVersion
#

foreach($name in @("workloadsapi", "workloadsprojector", "buildversion", "cronjob"))
{
	"http://buildversion.local:8081/api/Binaries/RevisionInc/$name"
	$buildVersion = curl.exe -s "http://buildversion.local:8081/api/Binaries/RevisionInc/$name" | ConvertFrom-Json
	$Description = "Local build"
	$semanticVersion = $buildVersion.buildVersion.semanticVersion
	if([string]::IsNullOrEmpty($semanticVersion)) 
	{
		$semanticVersion = "latest"
	}
	
	"Current build: ${name}:${semanticVersion}"
	"Version: ${semanticVersion}"
	"Description: ${Description}"
	"${env:registryhost}/${name}:${semanticVersion}"

	docker build -f .\${name}\Dockerfile --force-rm -t ${name} --build-arg Version="${semanticVersion}" --build-arg Description="${Description}" .
	docker tag ${name}:latest ${env:registryhost}/${name}:${semanticVersion}
	docker push ${env:registryhost}/${name}:${semanticVersion}
}