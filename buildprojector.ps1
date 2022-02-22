$buildVersion = curl.exe "https://localhost:7176/api/Binaries/RevisionInc/workloadsprojector"  | ConvertFrom-Json
$semanticVersion = $buildVersion.buildVersion.semanticVersion
$semanticVersion
docker build -f .\WorkloadsProjector\Dockerfile --force-rm -t workloadsprojector .
docker tag workloadsprojector:latest $env:registryhost/workloadsprojector:$semanticVersion
docker push $env:registryhost/workloadsprojector:$semanticVersion

# Remember, ":latest" is not optimal, we need to cover versioning of the images somehow