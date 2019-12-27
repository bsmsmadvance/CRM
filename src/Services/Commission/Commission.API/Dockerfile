FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5009

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Commission/Commission.API/Commission.API.csproj", "src/Services/Commission/Commission.API/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Commission/Commission.API/Commission.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Commission/Commission.API"
RUN dotnet build "Commission.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Commission.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Commission.API.dll"]