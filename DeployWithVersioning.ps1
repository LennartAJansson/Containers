#Assumes you have the project buildversion running on your localhost on port 9000
#
$alive = curl.exe -s "http://buildversion.local:8081/Ping" -H "accept: text/plain"
if($alive -ne "pong!")
{
	"You need to do an initial deploy of BuildVersion API"
	"Please run InitBuildVersion.ps1"
	return
}

foreach($name in @("buildversion", "workloadsapi", "workloadsprojector", "cronjob", "countriesapi"))
{
	$buildVersion = $null
	$buildVersion = curl.exe -s "http://buildversion.local:8081/api/Binaries/GetByName/$name" | ConvertFrom-Json
	$semanticVersion = $buildVersion.buildVersion.semanticVersion

	if([string]::IsNullOrEmpty($semanticVersion)) 
	{
		"Could not connect to buildversion api"
		"Please check that BuildVersion API is working correctly in your Kubernetes"
		return
	}

	"Current deploy: ${name}:${semanticVersion}"
	"${env:registryhost}/${name}:${semanticVersion}"

	cd ./deploy/${name}
	kustomize edit set image "${env:registryhost}/${name}:${semanticVersion}"
	cd ../..
	kubectl apply -k ./deploy/${name}
	
	if([string]::IsNullOrEmpty($env:AGENT_NAME))
	{
		git checkout deploy/${name}/kustomization.yaml
	}

#	#kubectl set image -n ${name} deployment/${name} ${name}="${env:registryhost}/${name}:${semanticVersion}"
#	#curl.exe -X POST -g "http://prometheus.local:8081/api/v1/admin/tsdb/delete_series?match[]={app='${name}'}"
}


#container_last_seen{pod="nats-0"}
#http_request_duration_seconds_sum{method="GET",controller="Query"}
#workloadsapi_controllers_executiontime{path="/api/Query/GetAssignments"}
#workloadsapi_controllers_executiontime{path="/api/Query/GetPeople"}
#workloadsapi_controllers_executiontime{path="/api/Query/GetWorkloads"}


