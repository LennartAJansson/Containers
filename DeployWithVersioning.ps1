#Assumes you have the project buildversionsapi running on your localhost on port 9000
#
$alive = curl.exe -s "http://buildversionsapi.local:8081/Ping" -H "accept: text/plain"
if($alive -ne "pong!")
{
	"You need to do an initial deploy of BuildVersionsApi"
	"Please run InitBuildVersion.ps1"
	return
}

foreach($name in @("buildversionsapi", "workloadsapi", "workloadsprojector", "cronjob", "countriesapi", "buildversions", "workloads", "countries"))
{
	$buildVersion = $null
	$buildVersion = curl.exe -s "http://buildversionsapi.local:8081/api/Binaries/GetBinaryByName/${name}" | ConvertFrom-Json
	$semanticVersion = $buildVersion.buildVersion.semanticVersion

	if([string]::IsNullOrEmpty($semanticVersion)) 
	{
		"Could not connect to buildversionsapi"
		"Please check that BuildVersionsApi is working correctly in your Kubernetes"
		return
	}

	"Current deploy: ${name}:${semanticVersion}"
	"${env:registryhost}/${name}:${semanticVersion}"

	cd ./deploy/${name}
	kustomize edit set image "${env:registryhost}/${name}:${semanticVersion}"
	if(Test-Path -Path ./secrets/*)
	{
		"Creating secrets"
		kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets |
			C:/Apps/kubeseal/kubeseal -n "${name}" --controller-namespace kube-system --format yaml > "secret.yaml"	}
	cd ../..
	kubectl apply -k ./deploy/${name}
	
	if([string]::IsNullOrEmpty($env:AGENT_NAME))
	{
		git checkout deploy/${name}/secret.yaml
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


