FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app/

COPY ["Notes.slnx", "."]
COPY ["src/Notes.Api/Notes.Api.csproj", "src/Notes.Api/"]
COPY ["src/Notes.Application/Notes.Application.csproj", "src/Notes.Application/"]
COPY ["src/Notes.Domain/Notes.Domain.csproj", "src/Notes.Domain/"]
COPY ["src/Notes.Infrastructure/Notes.Infrastructure.csproj", "src/Notes.Infrastructure/"]
COPY ["src/Notes.Shared/Notes.Shared.csproj", "src/Notes.Shared/"]
COPY ["Directory.Packages.props", "."]

RUN dotnet restore src/Notes.Api/Notes.Api.csproj

COPY . .

RUN dotnet publish src/Notes.Api/Notes.Api.csproj -c Release -o ./publish/

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish/ .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Notes.Api.dll"]