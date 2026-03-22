# Commands

## Publish microservices images to Docker Hub

```bash
.\publish-dockerhub.ps1 -DockerHubUser "kvdijk" -Tag "v0.1"
```

## Run locust tests

### Prerequisites

```bash
pip install locust
```

### Usage

```bash
.\locust\run_tests.ps1 -Architecture monolithic -BaseUrl https://as-kvandijk-bpsee-experiment-monolithic-aqapgzdsbhbbcae7.westeurope-01.azurewebsites.net
```

```bash
.\locust\run_tests.ps1 -Architecture microservices `
    -UsersHost    https://<users>.azurecontainerapps.io `
    -ProductsHost https://<products>.azurecontainerapps.io `
    -OrdersHost   https://<orders>.azurecontainerapps.io
```
