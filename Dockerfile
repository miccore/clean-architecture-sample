FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

COPY ./User.Microservice/bin/Release/net6.0/publish/. .
RUN mkdir wwwroot
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "User.Microservice.dll"]
