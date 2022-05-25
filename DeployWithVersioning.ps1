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
		#THE FOLDER "./secrets" SHOULD BE LOCATED OUTSIDE OF THE VERSION CONTROL!!!
		"Creating secrets"
		kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./secrets |
			C:/Apps/kubeseal/kubeseal -n "${name}" --controller-namespace kube-system --format yaml > "secret.yaml"	
	}
	
	cd ../..
	kubectl apply -k ./deploy/${name}
	
	if([string]::IsNullOrEmpty($env:AGENT_NAME))
	{
		if(Test-Path -Path deploy/${name}/secret.yaml)
		{
			#ALWAYS UNDO THE CHANGE OF ./secret.yaml SO IT NEVER GETS INTO THE VERSION CONTROL
			git checkout deploy/${name}/secret.yaml
		}
		git checkout deploy/${name}/kustomization.yaml
	}
	#DELETE DATA FROM PROMETHEUS
	#curl.exe -X POST -g "http://prometheus.local:8081/api/v1/admin/tsdb/delete_series?match[]={app='${name}'}"
}

#EXAMPLE OF PROMETHEUS QUERIES:
#container_last_seen{pod="nats-0"}
#http_request_duration_seconds_sum{method="GET",controller="Query"}
#workloadsapi_controllers_executiontime{path="/api/Query/GetAssignments"}
#workloadsapi_controllers_executiontime{path="/api/Query/GetPeople"}
#workloadsapi_controllers_executiontime{path="/api/Query/GetWorkloads"}
#http_request_duration_seconds_sum{method=~"GET|POST|PUT|DELETE",controller=~"Query|Command"}
#workloadsapi_controllers_executiontime{path=~"/api/Query/GetAssignments|/api/Query/GetPeople|/api/Query/GetWorkloads|/api/Command/CreatePerson|/api/Command/CreateAssignment|/api/Command/CreateWorkload|/api/Command/UpdateWorkload"}
#container_memory_usage_bytes{container=~"cronjob|buildversion|workloadsapi|workloadsprojector|countriesapi"}