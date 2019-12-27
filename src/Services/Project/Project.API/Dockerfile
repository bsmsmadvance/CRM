FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
RUN apt-get update
RUN apt-get install -y apt-utils
RUN apt-get install -y libgdiplus
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
WORKDIR /app
EXPOSE 5001

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Project/Project.API/Project.API.csproj", "src/Services/Project/Project.API/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Project/Project.API/Project.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Project/Project.API"
RUN dotnet build "Project.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Project.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Project.API.dll"]