# script directory 
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

$infraProj = Join-Path $scriptDir "\Buhoborec.Infrastructure\Buhoborec.Infrastructure.csproj"
$migrationsPath = Join-Path $scriptDir "\Buhoborec.Infrastructure\Migrations"

#Write-Host $migrationsPath
#Write-Host $infraProj

# check if migration exists
if (-not (Test-Path $migrationsPath)) {
    $needsMigration = $true
} else {
    # get all .cs files in migrations folder
    $files = Get-ChildItem $migrationsPath -Recurse -Filter *.cs -ErrorAction SilentlyContinue
    $needsMigration = ($files.Count -eq 0)
}

# WebUI tool-manifest
$webUIPath = Join-Path $scriptDir ".\Buhoborec.WebUI"
$webUIFolderName = "Buhoborec.WebUI"

if (-not ((Get-Location).Path -like "*\$webUIFolderName*")) {
    Set-Location $webUIPath
}

dotnet tool restore

# migration name
$timestamp = Get-Date -Format "yyyyMMdd_HHmmss"

if ($needsMigration) {
    # create migration
    dotnet tool run dotnet-ef migrations add "InitialCreate_$timestamp" -p $infraProj -s .
}
else
{
    Write-Host "Migrations exist"
}

# apply all migrations, but i do it in the proj
# dotnet tool run dotnet-ef database update -p $infraPath -s .
