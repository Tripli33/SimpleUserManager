FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app 
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SimpleUserManager/SimpleUserManager.csproj", "SimpleUserManager/"]
RUN dotnet restore "SimpleUserManager/SimpleUserManager.csproj"
COPY . . 
WORKDIR "/src/SimpleUserManager"
RUN dotnet build "./SimpleUserManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleUserManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleUserManager.dll"]