FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["src/Credito.WebApi/Credito.WebApi.csproj", "src/Credito.WebApi/"]
RUN dotnet restore "src/Credito.WebApi/Credito.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Credito.WebApi"
RUN dotnet build "Credito.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Credito.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Credito.WebApi.dll"]
