FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY src/CleanArchTemplate.Domain/CleanArchTemplate.Domain.csproj ./src/CleanArchTemplate.Domain/
COPY src/CleanArchTemplate.Application/CleanArchTemplate.Application.csproj ./src/CleanArchTemplate.Application/
COPY src/CleanArchTemplate.Infrastructure/CleanArchTemplate.Infrastructure.csproj ./src/CleanArchTemplate.Infrastructure/
COPY src/CleanArchTemplate.Api/CleanArchTemplate.Api.csproj ./src/CleanArchTemplate.Api/
RUN dotnet restore ./src/CleanArchTemplate.Api/CleanArchTemplate.Api.csproj

COPY . ./
RUN dotnet publish ./src/CleanArchTemplate.Api -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
EXPOSE 5432
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "CleanArchTemplate.Api.dll"]
