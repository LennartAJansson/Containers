if($env:Path -notmatch "chocolatey")
{
	iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
	Set-Alias choco "C:\ProgramData\chocolatey\bin\choco.exe"
}
else
{
	choco upgrade chocolatey
}

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
