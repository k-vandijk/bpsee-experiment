# Build context must be the solution root:
#   docker build -f BPSEE.Experiment.Microservices.Products/Dockerfile -t bpsee-products .

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Directory.Build.props .
COPY Directory.Packages.props .

COPY BPSEE.Experiment.Domain/BPSEE.Experiment.Domain.csproj                                             BPSEE.Experiment.Domain/
COPY BPSEE.Experiment.Application/BPSEE.Experiment.Application.csproj                                   BPSEE.Experiment.Application/
COPY BPSEE.Experiment.Infrastructure/BPSEE.Experiment.Infrastructure.csproj                             BPSEE.Experiment.Infrastructure/
COPY BPSEE.Experiment.Migrations.Products/BPSEE.Experiment.Migrations.Products.csproj                   BPSEE.Experiment.Migrations.Products/
COPY BPSEE.Experiment.Microservices.Products/BPSEE.Experiment.Microservices.Products.csproj             BPSEE.Experiment.Microservices.Products/

RUN dotnet restore BPSEE.Experiment.Microservices.Products/BPSEE.Experiment.Microservices.Products.csproj

COPY BPSEE.Experiment.Domain/                  BPSEE.Experiment.Domain/
COPY BPSEE.Experiment.Application/             BPSEE.Experiment.Application/
COPY BPSEE.Experiment.Infrastructure/          BPSEE.Experiment.Infrastructure/
COPY BPSEE.Experiment.Migrations.Products/     BPSEE.Experiment.Migrations.Products/
COPY BPSEE.Experiment.Microservices.Products/  BPSEE.Experiment.Microservices.Products/

RUN dotnet publish BPSEE.Experiment.Microservices.Products/BPSEE.Experiment.Microservices.Products.csproj \
    -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BPSEE.Experiment.Microservices.Products.dll"]
