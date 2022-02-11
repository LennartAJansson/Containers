docker build -f .\WorkloadsProjector\Dockerfile --force-rm -t workloadsprojector .
docker tag workloadsprojector:latest $env:registryhost/workloadsprojector:latest
docker push $env:registryhost/workloadsprojector:latest

# Remember, ":latest" is not optimal, we need to cover versioning of the images somehow