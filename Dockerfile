FROM mcr.microsoft.com/dotnet/sdk:5.0

MAINTAINER Yazan Kassam, yazankassam.codavia@gmail.com

WORKDIR /app

ENV ASPNETCORE_URLS="http://notifications_ms"

ENV ASPNETCORE_ENVIRONMENT=Development

ENV ConnectionStrings:DefaultConnection="Server=iread_mysql;Database=notifications_ms_db;Uid=codavia;Pwd=cod@v!@; convert zero datetime=True"

ENV ConsulConfig:Host="http://consul:8500"

ENV URL:applicationUrl="http://217.182.250.236:5019"

EXPOSE 5019

COPY ./publish .

ENTRYPOINT ["dotnet","iread_notifications_ms.dll"]