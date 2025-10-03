# ====== Build ======
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 

WORKDIR /src

COPY . .

RUN dotnet restore

RUN dotnet publish -c Release -o /app

# ====== Runtime ======
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY --from=build /app ./

RUN [ -f /app/appsettings.Development.json ] || cp /app/appsettings.Template.json /app/appsettings.Development.json

RUN echo "Usando $( [ -f /app/appsettings.Development.json ] && echo appsettings.Development.json || echo appsettings.Template.json )"

ENTRYPOINT ["dotnet", "ERP_TECKIO.dll"]

EXPOSE 8080
