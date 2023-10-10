FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /source
COPY . .
#COPY ./GeoApi.Presentation/GeoLite2-Country.mmdb /app

RUN dotnet restore "./GeoApi.Presentation/GeoApi.Presentation.csproj" --disable-parallel
RUN dotnet publish "./GeoApi.Presentation/GeoApi.Presentation.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY ./GeoApi.Presentation/GeoLite2-Country.mmdb /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "GeoApi.Presentation.dll"]