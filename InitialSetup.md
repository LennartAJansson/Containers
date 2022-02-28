To install all the tools needed for this example solution please execute the script ContainerTools.ps1 in this solutionfolder. It will install Chocolatey to help you installl various small commandline tools in a simple generic way.  

Since we are going to use Nats as a message broker in our sample you will most likely have use for their command line tool. Download it from https://github.com/nats-io/natscli/releases/  

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

Since we are going to do a lot of redirections through hostnames you should preferably add a shortcut on your desktop to make the access to your hosts system file as easy as possible. Add a shortcut on your desktop containing the command:  
```"C:\Windows\System32\notepad.exe C:\Windows\System32\drivers\etc\hosts"```  
Right-click your shortcut and make sure under Advanced that you always run this as administrator, otherwise you're not allowed to save any changes you do.  
