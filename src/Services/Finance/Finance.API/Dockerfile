FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
RUN apt-get update
RUN apt-get install -y apt-utils
RUN apt-get install -y libgdiplus
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
WORKDIR /app
EXPOSE 5007

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Finance/Finance.API/Finance.API.csproj", "src/Services/Finance/Finance.API/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Finance/Finance.API/Finance.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Finance/Finance.API"
RUN dotnet build "Finance.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Finance.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Finance.API.dll"]