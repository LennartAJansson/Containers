# This is stuff that goes into all powershell hosts, console and / or ISE

# Alias for nats cli tool
# Downloaded from https://github.com/nats-io/natscli/releases/
Set-Alias nats "c:\apps\natsjetstream\nats.exe"

function GoToFolder{
    Set-Location $args
    Get-ChildItem -Force
}
function reposfunc{
    GoToFolder("D:\ReposAuto")
}
Set-Alias repos reposfunc

function Time-Command($block) {
    $sw = [Diagnostics.Stopwatch]::StartNew()
    & $block
    $sw.Stop()
    $sw.Elapsed
}
Set-Alias time Time-Command

#https://intellitect.com/enter-vsdevshell-powershell/
$vsPath = &"C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe" -latest -prerelease -property installationpath
Import-Module (Join-Path $vsPath "Common7\Tools\Microsoft.VisualStudio.DevShell.dll")
Enter-VsDevShell -VsInstallPath $vsPath -SkipAutomaticLocation
