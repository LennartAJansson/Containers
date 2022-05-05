#Assumes you have the project buildversionsapi running on your localhost on port 9000
#
$alive = curl.exe -s "http://buildversionsapi.local:8081/Ping" -H "accept:text/plain"
if($alive -ne "pong!")
{
	"You need to do an initial deploy of BuildVersionsApi"
	"Please run InitBuildVersion.ps1"
	return
}

foreach($name in @("buildversionsapi", "workloadsapi", "workloadsprojector", "cronjob", "countriesapi", "buildversions", "workloads", "countries"))
{
	$branch = git rev-parse --abbrev-ref HEAD
	$commit = git log -1 --pretty=format:"%H"
	$description = "${branch}: ${commit}"
	$buildVersion = $null
	$buildVersion = curl.exe -s "http://buildversionsapi.local:8081/api/Binaries/RevisionInc/$name" | ConvertFrom-Json
	$semanticVersion = $buildVersion.buildVersion.semanticVersion
	
	if([string]::IsNullOrEmpty($semanticVersion) -or [string]::IsNullOrEmpty($description)) 
	{
		"Could not connect to git repo or buildversionsapi"
		"Please check that you are in the correct folder and that"
		"BuildVersionsApi is working correctly in your Kubernetes"
		return
	}
	
	"Current build: ${name}:${semanticVersion}"
	"Version: ${semanticVersion}"
	"Description: ${description}"
	"${env:registryhost}/${name}:${semanticVersion}"

	docker build -f .\${name}\Dockerfile --force-rm -t ${name} --build-arg Version="${semanticVersion}" --build-arg Description="${description}" .
	docker tag ${name}:latest ${env:registryhost}/${name}:${semanticVersion}
	docker push ${env:registryhost}/${name}:${semanticVersion}
}