if($env:Path -notmatch "chocolatey")
{
	iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
	Set-Alias choco "C:\ProgramData\chocolatey\bin\choco.exe"

	choco feature enable -n allowGlobalConfirmation

	# Good to have, console terminal
	choco install -force conemu

	# Copy or add the content of this file to your powershell profile to add support for Visual Studio and other tools:
	# profile.ps1

	# Add Docker desktop for windows
	choco install -force docker-desktop

	# Add any other tools that will be needed
	choco install -force k3d
	choco install -force curl
	choco install -force kubernetes-cli
	choco install -force kubernetes-helm
	choco install -force kustomize
	choco install -force k9s
}
else
{
	choco upgrade chocolatey
	choco upgrade -force conemu
	choco upgrade -force docker-desktop
	choco upgrade -force k3d
	choco upgrade -force curl
	choco upgrade -force kubernetes-cli
	choco upgrade -force kubernetes-helm
	choco upgrade -force kustomize
	choco upgrade -force k9s
}


#openshift Redhat Kubernetes
#k8s fullscale kubernetes server
#k3s minimal k8s
#k3d docker hosting of k3s
#k9s tool to work against all kubernets variants