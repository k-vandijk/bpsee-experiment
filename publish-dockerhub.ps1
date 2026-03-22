# publish-dockerhub.ps1
# Builds the three microservice images and pushes them to Docker Hub.
#
# Prerequisites:
#   - Docker Desktop running
#   - docker login (eenmalig in je terminal, of wordt gevraagd door het script)
#
# Usage:
#   .\publish-dockerhub.ps1 -DockerHubUser "jouwgebruiker"
#   .\publish-dockerhub.ps1 -DockerHubUser "jouwgebruiker" -Tag "v1.0"

param(
    [Parameter(Mandatory)] [string] $DockerHubUser,
    [string] $Tag = "latest"
)

$ErrorActionPreference = "Stop"
$root = $PSScriptRoot

$services = @(
    @{ Name = "users";    Dockerfile = "BPSEE.Experiment.Microservices.Users/Dockerfile" },
    @{ Name = "products"; Dockerfile = "BPSEE.Experiment.Microservices.Products/Dockerfile" },
    @{ Name = "orders";   Dockerfile = "BPSEE.Experiment.Microservices.Orders/Dockerfile" }
)

Write-Host "`nLogging in to Docker Hub..." -ForegroundColor Yellow
docker login --username $DockerHubUser
if ($LASTEXITCODE -ne 0) { exit 1 }

foreach ($svc in $services) {
    $image = "$DockerHubUser/kvandijk-bpsee-microservices-$($svc.Name):$Tag"

    Write-Host "`nBuilding $image..." -ForegroundColor Cyan
    docker build -f "$root/$($svc.Dockerfile)" -t $image $root
    if ($LASTEXITCODE -ne 0) { Write-Host "Build failed for $($svc.Name)" -ForegroundColor Red; exit 1 }

    Write-Host "Pushing $image..." -ForegroundColor Cyan
    docker push $image
    if ($LASTEXITCODE -ne 0) { Write-Host "Push failed for $($svc.Name)" -ForegroundColor Red; exit 1 }

    Write-Host "  ✓ hub.docker.com/$DockerHubUser/kvandijk-bpsee-microservices-$($svc.Name):$Tag" -ForegroundColor Green
}

Write-Host "`nAll images published!" -ForegroundColor Cyan
