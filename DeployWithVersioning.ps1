#Assumes you have the github project buildversion running on your localhost on port 9000
#https://github.com/LennartAJansson/BuildVersion
#

foreach($name in @("workloadsapi", "workloadsprojector", "buildversion", "cronjob"))
{
	#kubectl delete -k ./deploy/${name}
	$buildVersion = $null
	$buildVersion = curl.exe -s "http://buildversion.local:8081/api/Binaries/GetByName/$name"  | ConvertFrom-Json
	$semanticVersion = ""
	$semanticVersion = ${buildVersion.buildVersion.semanticVersion}
	if([string]::IsNullOrEmpty($semanticVersion)) 
	{
		$semanticVersion = "latest"
	}

	"Current deploy: ${name}:${semanticVersion}"
	"${env:registryhost}/${name}:${semanticVersion}"

	cd ./deploy/${name}
	kustomize edit set image "${env:registryhost}/${name}:${semanticVersion}"
	cd ../..
	kubectl apply -k ./deploy/${name}

#	#kubectl set image -n ${name} deployment/${name} ${name}="${env:registryhost}/${name}:${semanticVersion}"
#	#curl.exe -X POST -g "http://prometheus.local:8081/api/v1/admin/tsdb/delete_series?match[]={app='${name}'}"
}


#container_last_seen{pod="nats-0"}
#http_request_duration_seconds_sum{method="GET",controller="Query"}
#workloadsapi_controllers_executiontime{path="/api/Query/GetAssignments"}
#workloadsapi_controllers_executiontime{path="/api/Query/GetPeople"}
#workloadsapi_controllers_executiontime{path="/api/Query/GetWorkloads"}


