## Install tools
To install all the tools needed for this example solution please execute the script ContainerTools.ps1 in this solutionfolder. It will install Chocolatey to help you installl various small commandline tools in a simple generic way.  

Since we are going to use Nats as a message broker in our sample you will most likely have use for their command line tool. Download it from https://github.com/nats-io/natscli/releases/  

## Fixing your powershell profile
In your powershell profile add an alias for Nats command line tool, in my system it looks like this:  
``` Powershell
Set-Alias nats "c:\apps\natsjetstream\nats.exe"
```  
You could also make sure that you have full access to your Visual Studio environment everytime you run powershell by adding following lines to your profile.ps1:
``` Powershell
$vsPath = &"C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -property installationpath
Import-Module (Join-Path $vsPath "Common7\Tools\Microsoft.VisualStudio.DevShell.dll")
Enter-VsDevShell -VsInstallPath $vsPath -SkipAutomaticLocation
```  
There's a sample profile.ps1 included in this solution.  

## Windows hosts aliases
Because we are going to do a lot of redirections through hostnames you should preferably add a shortcut on your desktop to make the access to your hosts system file as easy as possible. Add a shortcut on your desktop containing the command:  
```"C:\Windows\System32\notepad.exe C:\Windows\System32\drivers\etc\hosts"```  
Right-click your shortcut and make sure under Advanced that you always run this as administrator, otherwise you're not allowed to save any changes you do.  

## Solution content
1. Initial: This part
2. Base: Contains the base applications, one api receiving client requests and one background service maintaining the persistancy. The applications follow CQRS and Mediator pattern and requires MySql for persistance in combination with Nats for Messaging.  
3. Docker: How to run multiple applications inside a docker environment by the use of docker-compose.
4. Kubernetes: Deployment configurations to run the workload example applications in a Kubernetes cluster together with Nats and MySql also in the cluster.
5. BuildVersion: A more complex scenario where we use a web api to keep track of image versioning to avoid using latest as image version all the time. This part contains a web api using MySql and also updated build and deploy scripts for workloads example applications. This api runs in Kubernetes and all deployment configurations are included.
6. Prometheus, Grafana:
7. Various docs: