#Assumes you have the github project buildversion running on your localhost on port 9000
#https://github.com/LennartAJansson/BuildVersion
#

foreach($name in @("workloadsapi", "workloadsprojector", "buildversion", "cronjob"))
{
	"http://buildversion.local:8081/api/Binaries/RevisionInc/$name"
	$branch = git rev-parse --abbrev-ref HEAD
	#$commit = git log -1 --pretty=format:"%H - %an : %s"
	$commit = git log -1 --pretty=format:"%H"
	$description = "${branch}: ${commit}"
	$buildVersion = curl.exe -s "http://buildversion.local:8081/api/Binaries/RevisionInc/$name" | ConvertFrom-Json
	$semanticVersion = $buildVersion.buildVersion.semanticVersion
	
	if([string]::IsNullOrEmpty($semanticVersion)) 
	{
		$semanticVersion = "latest"
	}
	else
	{
		git tag $semanticVersion
	}

	if([string]::IsNullOrEmpty($description))
	{
		$description = "Local build"
	}
	
	"Current build: ${name}:${semanticVersion}"
	"Version: ${semanticVersion}"
	"Description: ${description}"
	"${env:registryhost}/${name}:${semanticVersion}"

	docker build -f .\${name}\Dockerfile --force-rm -t ${name} --build-arg Version="${semanticVersion}" --build-arg Description="${description}" .
	docker tag ${name}:latest ${env:registryhost}/${name}:${semanticVersion}
	docker push ${env:registryhost}/${name}:${semanticVersion}
}