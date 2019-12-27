FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 4002

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/Services/Customer/Customer.Background/Customer.Background.csproj", "src/Services/Customer/Customer.Background/"]
COPY ["src/Database/Database.Models/Database.Models.csproj", "src/Database/Database.Models/"]
RUN dotnet restore "src/Services/Customer/Customer.Background/Customer.Background.csproj"
COPY . .
WORKDIR "/src/src/Services/Customer/Customer.Background"
RUN dotnet build "Customer.Background.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Customer.Background.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Customer.Background.dll"]