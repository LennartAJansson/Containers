$alive = curl -s "http://buildversion.local:8081/Ping" -H "accept: text/plain"
if($alive -eq "pong!")
{
	. (".\DeployWithVersioning.ps1")
	return
}

foreach($name in @("workloadsapi", "workloadsprojector", "buildversion", "cronjob"))
{
	$semanticVersion = "latest"
	"Current deploy: ${name}:${semanticVersion}"
	
	kubectl apply -k ./deploy/${name}

	curl.exe -X POST -g "http://prometheus.local:8081/api/v1/admin/tsdb/delete_series?match[]={app='${name}'}"
}
