FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /App
COPY . ./
RUN dotnet restore "BadgerClan.Api.csproj"
RUN dotnet publish "BadgerClan.Api.csproj" -c Release -o publish

# Build the image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /App
COPY --from=build /App/publish .

EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_URLS="http://*:5000;http://*:5001"

ENTRYPOINT ["dotnet", "BadgerClan.Api.dll"]
