FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5004

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Promotion/Promotion.API/Promotion.API.csproj", "src/Services/Promotion/Promotion.API/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Promotion/Promotion.API/Promotion.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Promotion/Promotion.API"
RUN dotnet build "Promotion.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Promotion.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Promotion.API.dll"]