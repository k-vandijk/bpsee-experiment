# run_tests.ps1
# Runs all four load levels (50, 100, 250, 500) three times each for the microservices architecture.
# Results are saved as HTML reports in the locust/reports/ folder.
#
# Prerequisites:
#   pip install locust
#
# Usage:
#   .\locust\run_tests.ps1 `
#       -UsersHost    https://<users>.azurecontainerapps.io `
#       -ProductsHost https://<products>.azurecontainerapps.io `
#       -OrdersHost   https://<orders>.azurecontainerapps.io

param(
    [string]$UsersHost    = "",
    [string]$ProductsHost = "",
    [string]$OrdersHost   = "",

    [int]$Repetitions = 3,
    [int]$SpawnRate   = 10   # users per second during ramp-up
)

if (-not $UsersHost -or -not $ProductsHost -or -not $OrdersHost) {
    Write-Host "Error: -UsersHost, -ProductsHost and -OrdersHost are all required." -ForegroundColor Red
    exit 1
}

$scriptDir  = $PSScriptRoot
$reportsDir = Join-Path $scriptDir "reports"
New-Item -ItemType Directory -Force -Path $reportsDir | Out-Null

$levels     = @(50, 100, 250, 500)
$locustfile = Join-Path $scriptDir "locustfile_microservices.py"

foreach ($users in $levels) {
    for ($run = 1; $run -le $Repetitions; $run++) {
        $csvPrefix  = Join-Path $reportsDir "microservices_${users}users_run${run}"
        $reportFile = "${csvPrefix}.html"

        Write-Host ""
        Write-Host "[microservices] $users users — run $run/$Repetitions" -ForegroundColor Cyan

        $env:TARGET_USERS    = "$users"
        $env:USERS_HOST    = $UsersHost
        $env:PRODUCTS_HOST = $ProductsHost
        $env:ORDERS_HOST   = $OrdersHost

        locust -f $locustfile `
               --headless `
               --users $users `
               --spawn-rate $SpawnRate `
               --run-time 8m `
               --html $reportFile `
               --csv $csvPrefix

        if ($LASTEXITCODE -eq 0) {
            Write-Host "  ✓ Report: $reportFile" -ForegroundColor Green
        } else {
            Write-Host "  ✗ Run failed (exit code $LASTEXITCODE)" -ForegroundColor Red
        }
    }
}

Write-Host ""
Write-Host "All runs complete. Reports in: $reportsDir" -ForegroundColor Cyan
