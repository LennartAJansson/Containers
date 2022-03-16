foreach($name in @("workloadsapi","workloadsprojector","cronjob"))
{
	$semanticVersion = "latest"
	"Current deploy: " + $name + ":" +$semantivVersion
	
	kubectl delete -k ./deploy/$name
	kubectl apply -k ./deploy/$name

	curl.exe -X POST -g "http://prometheus.local:8081/api/v1/admin/tsdb/delete_series?match[]={app='${name}'}"
}
