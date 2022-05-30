$unityPath = $args[0]
$projectDir = "..\ProjectArena\ProjectArenaGame"
Write-Host "Starting build all"
$process = Start-Process -FilePath "$unityPath" -Wait -PassThru `
    -ArgumentList "-batchmode", "-quit", "-releaseCodeOptimization", `
    "-logFile", "$projectDir\Logs\BuildAll.log", `
    "-projectPath", "$projectDir", `
    "-executeMethod", "Assets.Scripts.Editor.BuildScripts.BuildAll"
    
if ($process.ExitCode -ne 0) {
    Write-Error "Build has failed" "$process.ExitCode"
}
else {
    Write-Host "Build succeeded"
}