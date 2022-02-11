docker build -f .\WorkloadsApi\Dockerfile --force-rm -t workloadsapi .
docker tag workloadsapi:latest $env:registryhost/workloadsapi:latest
docker push $env:registryhost/workloadsapi:latest

# Remember, ":latest" is not optimal, we need to cover versioning of the images somehow