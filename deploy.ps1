foreach($name in @("workloadsapi","workloadsprojector"))
{
	$semanticVersion = "latest"
	"Current deploy: " + $name + ":" +$semantivVersion
	
	kubectl delete -k ./deploy/$name
	kubectl apply -k ./deploy/$name
	kubectl set image -n ${name} deployment/${name} ${name}=$env:registryhost/${name}:${semanticversion}
	curl.exe -X POST -g "http://prometheus.local:8081/api/v1/admin/tsdb/delete_series?match[]={app='${name}'}"
}
