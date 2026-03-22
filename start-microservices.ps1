# start-microservices.ps1
# Builds the solution once, then starts all three microservices each in their own terminal window.

$root = $PSScriptRoot

$services = @(
    @{ Name = "Users";    Project = "BPSEE.Experiment.Microservices.Users";    Port = 5257 },
    @{ Name = "Products"; Project = "BPSEE.Experiment.Microservices.Products"; Port = 5229 },
    @{ Name = "Orders";   Project = "BPSEE.Experiment.Microservices.Orders";   Port = 5264 }
)

Write-Host ""
Write-Host "Building solution..." -ForegroundColor Yellow
dotnet build "$root\BPSEE.Experiment.slnx" --configuration Debug -v minimal
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed. Fix errors and try again." -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Starting BPSEE microservices..." -ForegroundColor Cyan
Write-Host ""

foreach ($svc in $services) {
    $projectPath = Join-Path $root $svc.Project
    $title       = "BPSEE - $($svc.Name) (http://localhost:$($svc.Port))"

    Start-Process powershell -ArgumentList `
        "-NoExit", `
        "-Command", `
        "`$host.UI.RawUI.WindowTitle = '$title'; Write-Host 'Starting $($svc.Name) on http://localhost:$($svc.Port)' -ForegroundColor Green; dotnet run --project '$projectPath' --launch-profile http --no-build"

    Write-Host "  ✓ $($svc.Name)  →  http://localhost:$($svc.Port)/api/$($svc.Name.ToLower())" -ForegroundColor Green
}

Write-Host ""
Write-Host "All services starting. Close individual windows to stop them." -ForegroundColor Cyan
Write-Host ""
