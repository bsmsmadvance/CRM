FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 4008

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Project/Project.Background/Project.Background.csproj", "src/Services/Project/Project.Background/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Project/Project.Background/Project.Background.csproj"
COPY . .
WORKDIR "/src/src/Services/Project/Project.Background"
RUN dotnet build "Project.Background.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Project.Background.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Project.Background.dll"]