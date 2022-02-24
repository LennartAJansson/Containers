$buildVersion = curl.exe -s "http://localhost:9000/api/Binaries/RevisionInc/workloadsprojector"  | ConvertFrom-Json
#$buildVersion

$semanticVersion = $buildVersion.buildVersion.semanticVersion
if([string]::IsNullOrEmpty($semanticVersion)) 
{
	$semanticVersion = "latest"
}

$semanticVersion

docker build -f .\WorkloadsProjector\Dockerfile --force-rm -t workloadsprojector .
docker tag workloadsprojector:latest $env:registryhost/workloadsprojector:$semanticVersion
docker push $env:registryhost/workloadsprojector:$semanticVersion

# Remember, ":latest" is never optimal, we need to cover versioning of the images somehow