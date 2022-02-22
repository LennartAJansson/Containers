$buildVersion = curl.exe "https://localhost:7176/api/Binaries/RevisionInc/workloadsapi"  | ConvertFrom-Json
$semanticVersion = $buildVersion.buildVersion.semanticVersion
$semanticVersion
docker build -f .\WorkloadsApi\Dockerfile --force-rm -t workloadsapi .
docker tag workloadsapi:latest $env:registryhost/workloadsapi:$semanticVersion
docker push $env:registryhost/workloadsapi:$semanticVersion

# Remember, ":latest" is not optimal, we need to cover versioning of the images somehow
