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
.\locust\run_tests.ps1 -Architecture microservices `
    -UsersHost    https://ca-exp-microservices-users.wittysand-6561c76d.westeurope.azurecontainerapps.io `
    -ProductsHost https://ca-exp-microservices-products.wittysand-6561c76d.westeurope.azurecontainerapps.io `
    -OrdersHost   https://ca-exp-microservices-orders.wittysand-6561c76d.westeurope.azurecontainerapps.io
```
