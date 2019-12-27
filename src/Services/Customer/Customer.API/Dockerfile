FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5002

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Customer/Customer.API/Customer.API.csproj", "src/Services/Customer/Customer.API/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Customer/Customer.API/Customer.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Customer/Customer.API"
RUN dotnet build "Customer.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Customer.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Customer.API.dll"]