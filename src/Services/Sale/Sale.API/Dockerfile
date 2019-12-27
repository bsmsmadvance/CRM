FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5006

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Sale/Sale.API/Sale.API.csproj", "src/Services/Sale/Sale.API/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Sale/Sale.API/Sale.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Sale/Sale.API"
RUN dotnet build "Sale.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Sale.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Sale.API.dll"]