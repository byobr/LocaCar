#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ./LocaCar/LocaCar.csproj ./LocaCar/
COPY ./BLL/BLL.csproj ./BLL/
COPY ./Entidades/Entidades.csproj ./Entidades/
COPY ./Util/Util.csproj ./Util/
COPY ./Fronteiras/Fronteiras.csproj ./Fronteiras/
COPY ./Repositorios/Repositorios.csproj ./Repositorios/
RUN dotnet restore "./LocaCar/LocaCar.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./LocaCar/LocaCar.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./LocaCar/LocaCar.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "LocaCar.dll"]