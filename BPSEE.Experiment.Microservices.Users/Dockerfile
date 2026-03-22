# Build context must be the solution root:
#   docker build -f BPSEE.Experiment.Microservices.Users/Dockerfile -t bpsee-users .

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
COPY BPSEE.Experiment.Migrations.Users/BPSEE.Experiment.Migrations.Users.csproj                     BPSEE.Experiment.Migrations.Users/
COPY BPSEE.Experiment.Microservices.Users/BPSEE.Experiment.Microservices.Users.csproj               BPSEE.Experiment.Microservices.Users/

RUN dotnet restore BPSEE.Experiment.Microservices.Users/BPSEE.Experiment.Microservices.Users.csproj

COPY BPSEE.Experiment.Domain/               BPSEE.Experiment.Domain/
COPY BPSEE.Experiment.Application/          BPSEE.Experiment.Application/
COPY BPSEE.Experiment.Infrastructure/       BPSEE.Experiment.Infrastructure/
COPY BPSEE.Experiment.Migrations.Users/     BPSEE.Experiment.Migrations.Users/
COPY BPSEE.Experiment.Microservices.Users/  BPSEE.Experiment.Microservices.Users/

RUN dotnet publish BPSEE.Experiment.Microservices.Users/BPSEE.Experiment.Microservices.Users.csproj \
    -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BPSEE.Experiment.Microservices.Users.dll"]
