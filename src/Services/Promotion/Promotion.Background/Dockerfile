FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 4006

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Promotion/Promotion.Background/Promotion.Background.csproj", "src/Services/Promotion/Promotion.Background/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Promotion/Promotion.Background/Promotion.Background.csproj"
COPY . .
WORKDIR "/src/src/Services/Promotion/Promotion.Background"
RUN dotnet build "Promotion.Background.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Promotion.Background.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Promotion.Background.dll"]