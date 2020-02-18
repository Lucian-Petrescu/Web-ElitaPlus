param(
    $BranchName = "Dev",
    $ReleaseEnvironments = "AS-Elita America Test",
    $EnvironmentPath = 1,
    $HarvesterReleaseEnvironments = "Batch1 Model,AS-Elita Europe Model Batch,AS-Elita Japan Model Batch",
    $TargetBranch = "GsspDev",
    $TargetEnvironments = "Shared1 Dev ATL,Shared1 Dev MSP",
    $TargetEnvironmentPath = 1,
    $BatchTargetEnvironments = "Batch1 Dev ATL,Batch1 Dev MSP"
)

Write-Host "Passed in values"
Write-Host "-----------------------------------------"
Write-Host "BranchName": $BranchName
Write-Host "ReleaseEnvironments:" $ReleaseEnvironments
Write-Host "EnvironmentPath:" $EnvironmentPath
Write-Host "HarvesterReleaseEnvironments:" $HarvesterReleaseEnvironments
Write-Host "TargetBranch:" $TargetBranch
Write-Host "TargetEnvironments:" $TargetEnvironments
Write-Host "TargetEnvironmentPath:" $TargetEnvironmentPath
Write-Host "BatchTargetEnvironments:" $BatchTargetEnvironments
Write-Host "-----------------------------------------"

if($BranchName -eq "$TargetBranch")
{
    $ReleaseEnvironments = $TargetEnvironments
    $EnvironmentPath = $TargetEnvironmentPath
    $HarvesterReleaseEnvironments = $BatchTargetEnvironments

    Write-Host "Custom branch detected"
    Write-Host "Will use target values"
    Write-Host "-----------------------------------------"    
    Write-Host ("##vso[task.setvariable variable=ReleaseEnvironments;]$ReleaseEnvironments")    
    Write-Host "Targeted Environments: " $ReleaseEnvironments

    Write-Host ("##vso[task.setvariable variable=EnvironmentPath;]$EnvironmentPath")
    Write-Host "Targeted Path: " $EnvironmentPath

    Write-Host ("##vso[task.setvariable variable=HarvesterReleaseEnvironments;]$BatchTargetEnvironments")
    Write-Host "Targeted Batch Environments: " $HarvesterReleaseEnvironments
    Write-Host "-----------------------------------------"
}
else
{
    Write-Host "Using default values"
    Write-Host "-----------------------------------------"
    Write-Host "BranchName": $BranchName
    Write-Host "EnvironmentPath:" $EnvironmentPath
    Write-Host "HarvesterReleaseEnvironments:" $HarvesterReleaseEnvironments
    Write-Host "ReleaseEnvironments:" $ReleaseEnvironments
    Write-Host "BatchTargetEnvironments:" $BatchTargetEnvironments
    Write-Host "-----------------------------------------"
}