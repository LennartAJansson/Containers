$buildVersion = curl.exe -s "http://localhost:9000/api/Binaries/RevisionInc/workloadsapi"  | ConvertFrom-Json
$buildVersion

$semanticVersion = $buildVersion.buildVersion.semanticVersion
if([string]::IsNullOrEmpty($semanticVersion)) 
{
	$semanticVersion = "latest"
}

$semanticVersion

docker build -f .\WorkloadsApi\Dockerfile --force-rm -t workloadsapi .
docker tag workloadsapi:latest $env:registryhost/workloadsapi:$semanticVersion
docker push $env:registryhost/workloadsapi:$semanticVersion

# Remember, ":latest" is never optimal, we need to cover versioning of the images somehow
