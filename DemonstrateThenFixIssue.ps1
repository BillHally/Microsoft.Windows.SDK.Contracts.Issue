cd $PSScriptRoot

Write-Host -Foreground Green "Get Paket..."
dotnet tool restore

Write-Host -Foreground Green "Run Paket restore..."
dotnet paket restore

Write-Host -Foreground Green "Build..."
msbuild /v:minimal

Write-Host -Foreground Green "Run the broken version..."
ShowIssue\bin\Debug\netcoreapp3.0\ShowIssue.exe

Write-Host -Foreground Green "Fix it..."
&"$PSScriptRoot\ConvertAssemblyReferences\bin\Debug\netcoreapp3.0\ConvertAssemblyReferences.exe" `
	ShowIssue\bin\Debug\netcoreapp3.0\ShowIssue.dll `
	Windows.Foundation.UniversalApiContract `
	Windows.Foundation.FoundationContract `
	Windows

Write-Host -Foreground Green "Run the fixed version..."
ShowIssue\bin\Debug\netcoreapp3.0\ShowIssue.exe
