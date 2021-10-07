FROM mcr.microsoft.com/dotnet/sdk:5.0

MAINTAINER Yazan Kassam, yazankassam.codavia@gmail.com

WORKDIR /app

ENV ASPNETCORE_URLS="http://notifications_ms"

ENV ASPNETCORE_ENVIRONMENT=Development

ENV ConnectionStrings:DefaultConnection="Server=iread_mysql;Database=notifications_ms_db;Uid=codavia;Pwd=C0d@v!@; convert zero datetime=True"

ENV ConsulConfig:Host="http://consul:8500"

EXPOSE 5017

COPY ./publish .

ENTRYPOINT ["dotnet","iread_notifications_ms.dll"]


