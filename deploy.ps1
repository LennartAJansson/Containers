foreach($name in @("workloadsapi", "workloadsprojector", "buildversion", "cronjob"))
{
	$semanticVersion = "latest"
	"Current deploy: ${name}:${semanticVersion}"
	
	#kubectl delete -k ./deploy/${name}
	kubectl apply -k ./deploy/${name}

	curl.exe -X POST -g "http://prometheus.local:8081/api/v1/admin/tsdb/delete_series?match[]={app='${name}'}"
}
