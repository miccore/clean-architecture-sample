FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine
WORKDIR /app

COPY ./src/Miccore.CleanArchitecture.Sample.Api/bin/Release/net6.0/publish/. .
RUN mkdir wwwroot
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "Miccore.CleanArchitecture.Sample.Api.dll"]
