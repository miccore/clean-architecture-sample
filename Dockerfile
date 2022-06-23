FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

COPY ./src/Miccore.CleanArchitecture.Sample.Api/bin/Release/net6.0/publish/. .
RUN mkdir wwwroot
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "User.Microservice.dll"]
