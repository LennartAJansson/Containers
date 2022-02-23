#This is how you clean the registry from all repositories
$registry = curl.exe -s http://localhost:5000/v2/_catalog | ConvertFrom-Json
foreach($repositoryName in $registry.repositories)
{
	$repositoryTags = curl.exe -s http://localhost:5000/v2/$repositoryName/tags/list | ConvertFrom-Json

	foreach($tag in $repositoryTags.tags)
	{
		$repositoryName + " - " + $tag
		$response = Invoke-WebRequest -Headers @{'Accept' = 'application/vnd.docker.distribution.manifest.v2+json'} -Uri http://localhost:5000/v2/$repositoryName/manifests/$tag -Method HEAD
		$digest = $response.Headers["Docker-Content-Digest"]
		"Trying to delete " + $digest
		curl.exe -v -s -H "Accept: application/vnd.docker.distribution.manifest.v2+json" -X DELETE "http://localhost:5000/v2/$repositoryName/manifests/$digest"
	}
}

docker exec registry bin/registry garbage-collect /etc/docker/registry/config.yml

#Only way to delete repository folders is to do following:
#docker exec registry sudo rm -rf /var/lib/registry/docker/v2/repositories/workloadsapi
#docker exec registry sudo rm -rf /var/lib/registry/docker/v2/repositories/workloadsprojector