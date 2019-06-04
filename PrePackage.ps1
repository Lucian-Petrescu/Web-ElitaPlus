Remove-Item $ENV:BUILD_STAGINGDIRECTORY\ElitaInternalWS\Web.config
Remove-Item $ENV:BUILD_STAGINGDIRECTORY\ElitaInternalWS\packages.config
Rename-Item $ENV:BUILD_STAGINGDIRECTORY\ElitaInternalWS\Rel.Web.Config Web.Config


Remove-Item $ENV:BUILD_STAGINGDIRECTORY\ElitaPlusWebApp\Web.config
Remove-Item $ENV:BUILD_STAGINGDIRECTORY\ElitaPlusWebApp\packages.config
Rename-Item $ENV:BUILD_STAGINGDIRECTORY\ElitaPlusWebApp\Rel.Web.Config Web.Config


Copy-Item $ENV:BUILD_SOURCESDIRECTORY\ElitaTransallFileSystemService\deploy$ENV:ReleasePath.config $ENV:BUILD_SOURCESDIRECTORY\ElitaTransallFileSystemService\Bin\Release\deploy.config
Copy-Item $ENV:BUILD_SOURCESDIRECTORY\ElitaTransallFileSystemService\Settings.xml $ENV:BUILD_SOURCESDIRECTORY\ElitaTransallFileSystemService\Bin\Release\Settings.xml

Copy-Item $ENV:BUILD_SOURCESDIRECTORY\BuildUtils\ElitaConfigEncrypt.exe $ENV:BUILD_SOURCESDIRECTORY\ElitaHarvesterService\bin\Release\ElitaConfigEncrypt.exe
