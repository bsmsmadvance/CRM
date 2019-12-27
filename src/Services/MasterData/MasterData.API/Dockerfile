FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/MasterData/MasterData.API/MasterData.API.csproj", "src/Services/MasterData/MasterData.API/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/MasterData/MasterData.API/MasterData.API.csproj"
COPY . .
WORKDIR "/src/src/Services/MasterData/MasterData.API"
RUN dotnet build "MasterData.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MasterData.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MasterData.API.dll"]