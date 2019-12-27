FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5008

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Accounting/Accounting.API/Accounting.API.csproj", "src/Services/Accounting/Accounting.API/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Accounting/Accounting.API/Accounting.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Accounting/Accounting.API"
RUN dotnet build "Accounting.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Accounting.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Accounting.API.dll"]