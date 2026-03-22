# Build context must be the solution root:
#   docker build -f BPSEE.Experiment.Microservices.Orders/Dockerfile -t bpsee-orders .

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Directory.Build.props .
COPY Directory.Packages.props .

COPY BPSEE.Experiment.Domain/BPSEE.Experiment.Domain.csproj                                         BPSEE.Experiment.Domain/
COPY BPSEE.Experiment.Application/BPSEE.Experiment.Application.csproj                               BPSEE.Experiment.Application/
COPY BPSEE.Experiment.Infrastructure/BPSEE.Experiment.Infrastructure.csproj                         BPSEE.Experiment.Infrastructure/
COPY BPSEE.Experiment.Migrations.Orders/BPSEE.Experiment.Migrations.Orders.csproj                   BPSEE.Experiment.Migrations.Orders/
COPY BPSEE.Experiment.Microservices.Orders/BPSEE.Experiment.Microservices.Orders.csproj             BPSEE.Experiment.Microservices.Orders/

RUN dotnet restore BPSEE.Experiment.Microservices.Orders/BPSEE.Experiment.Microservices.Orders.csproj

COPY BPSEE.Experiment.Domain/                BPSEE.Experiment.Domain/
COPY BPSEE.Experiment.Application/           BPSEE.Experiment.Application/
COPY BPSEE.Experiment.Infrastructure/        BPSEE.Experiment.Infrastructure/
COPY BPSEE.Experiment.Migrations.Orders/     BPSEE.Experiment.Migrations.Orders/
COPY BPSEE.Experiment.Microservices.Orders/  BPSEE.Experiment.Microservices.Orders/

RUN dotnet publish BPSEE.Experiment.Microservices.Orders/BPSEE.Experiment.Microservices.Orders.csproj \
    -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BPSEE.Experiment.Microservices.Orders.dll"]
