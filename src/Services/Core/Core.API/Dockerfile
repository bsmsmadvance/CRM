FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5003

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY src/Services/Core/Core.API/Core.API.csproj src/Services/Core/Core.API/
COPY src/Database/Database.Models/Database.Models.csproj src/Database/Database.Models/
COPY lib/PagingExtensions/PagingExtensions.csproj lib/PagingExtensions/
COPY lib/DateTimeExtensions/DateTimeExtensions.csproj lib/DateTimeExtensions/
COPY src/Services/Base.DTOs/Base.DTOs.csproj src/Services/Base.DTOs/
RUN dotnet restore src/Services/Core/Core.API/Core.API.csproj
COPY . .
WORKDIR /src/src/Services/Core/Core.API
RUN dotnet build Core.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Core.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Core.API.dll"]
