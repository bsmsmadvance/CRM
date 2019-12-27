FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 4005

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Identity/Identity.Background/Identity.Background.csproj", "src/Services/Identity/Identity.Background/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Identity/Identity.Background/Identity.Background.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/Identity.Background"
RUN dotnet build "Identity.Background.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Identity.Background.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.Background.dll"]