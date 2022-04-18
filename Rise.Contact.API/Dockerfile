FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

RUN apt-get update && \
  apt-get install -yq --allow-unauthenticated \
  libc6-dev \
  libgdiplus \
  libx11-dev && \
  rm -rf /var/lib/apt/lists/*

WORKDIR /source

COPY *.csproj .
COPY . .
RUN dotnet restore Rise.Contact.API.csproj

WORKDIR /source
RUN dotnet publish -c Release -o /app Rise.Contact.API.csproj

FROM mcr.microsoft.com/dotnet/aspnet:6.0

RUN apt-get update && \
  apt-get install -yq --allow-unauthenticated \
  libc6-dev \
  libgdiplus \
  libx11-dev && \
  rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build-env /app ./
ENV ASPNETCORE_ENVIRONMENT=Production 
ENV ASPNETCORE_URLS=http://+:80 
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Rise.Contact.API.dll"]