#This is how you clean the registry from all repositories
$registry = curl.exe -s http://localhost:5000/v2/_catalog | ConvertFrom-Json
foreach($repositoryName in $registry.repositories)
{
	$repositoryTags = curl.exe -s http://localhost:5000/v2/$repositoryName/tags/list | ConvertFrom-Json

	foreach($tag in $repositoryTags.tags)
	{
		$repositoryName + " - " + $tag
		$response = Invoke-WebRequest -Uri http://localhost:5000/v2/$repositoryName/manifests/$tag -Method HEAD
		$digest = $response.Headers["Docker-Content-Digest"]
		"Trying to delete " + $digest
		curl.exe -v -s -H "Accept: application/vnd.docker.distribution.manifest.v2+json" "http://localhost:5000/v2/$repositoryName/manifests/$digest"
	}
}

#docker exec registry bin/registry garbage-collect /etc/docker/registry/config.yml
