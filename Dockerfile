FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY . .
WORKDIR ./src/Miccore.CleanArchitecture.Sample.Api
RUN dotnet restore
RUN dotnet publish --no-restore -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app
 
#Copy in files from the build
COPY --from=build /out .
RUN mkdir wwwroot
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "Miccore.CleanArchitecture.Sample.Api.dll"]
