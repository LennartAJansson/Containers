#Assumes you have the github project buildversion running on your localhost on port 9000
#https://github.com/LennartAJansson/BuildVersion
#

foreach($name in @("workloadsapi","workloadsprojector"))
{
	"Current build: " + $name
	$buildVersion = curl.exe -s "http://buildversion.local:8081/api/Binaries/RevisionInc/$name"  | ConvertFrom-Json
	$semanticVersion = $buildVersion.buildVersion.semanticVersion
	if([string]::IsNullOrEmpty($semanticVersion)) 
	{
		$semanticVersion = "latest"
	}
	$semanticVersion

	docker build -f .\${name}\Dockerfile --force-rm -t ${name} .
	docker tag ${name}:latest $env:registryhost/${name}:$semanticVersion
	docker push $env:registryhost/${name}:$semanticVersion
}