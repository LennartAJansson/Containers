# K3d is a tool to generate a K3s environment in Docker.
k3d cluster create k3s --volume C:\Data\K8s:/tmp/shared@server:0 --kubeconfig-update-default --kubeconfig-switch-context --registry-create registry:5000 -p 8081:80@loadbalancer -p 4222:4222@server:0 -p 8222:8222@server:0 --api-port=16443 --wait --timeout=60s

# Check what host port your registry publish and save it in environment:
# From Powershell you can access it as $env:REGISTRYHOST
# From CMD-files you can access it as %REGISTRYHOST%
# Following the k3d command above the value should be: "registry:5000"

$port = (docker port registry).Split(':')[1]
$registryhost = "registry:$($port)"
SETX /M REGISTRYHOST $registryhost
$env:registryhost=$registryhost

"Add following to C:\Windows\System32\drivers\etc\hosts: "
"127.0.0.1 registry"
"127.0.0.1 registry.local"
""

# Verify that you have connection to your registry
curl.exe http://$registryhost/v2/_catalog

"To remove everything regarding cluster, loadbalancer and registry:"
"k3d cluster delete k3slocal"
