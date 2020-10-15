# Build Image
FROM mcr.microsoft.com/dotnet/sdk:5.0.100-rc.2-alpine3.12-amd64 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0.0-rc.2-alpine3.12-amd64
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BoundfoxStudios.Computermuseum.WebApi.dll"]
