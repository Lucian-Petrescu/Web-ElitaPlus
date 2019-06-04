param(
    $FilePath = "C:\code\Workspaces\AS.Elita-Elita\Source\Elita+\Common",
    $FileName = "GenericConstants",
    $FileExtention = "vb",
    $Version = "19.1.18.0"   
)

Write-Host "File path:" $FilePath
Write-Host "File name:" $FileName
Write-Host "File exension:" $FileExtention

$File = $FileName + '.' + $FileExtention
Write-Host "Full file name:" $File

$Path = $FilePath + '\' + $File
Write-Host "Full path:" $Path

Get-Content -path $Path -Raw

#Get the raw content of the file and replace the version. Save results.
((Get-Content -path $Path -Raw) -replace '(\d+\.\d+\.\d+\.\d+)', $Version) | Set-Content -path $Path
#((Get-Content -path $Path -Raw) -replace '19.1.16.0', $Version) | Set-Content -path $Path
 
Get-Content -path $Path -Raw
