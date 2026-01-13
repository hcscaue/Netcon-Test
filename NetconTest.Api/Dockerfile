FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["NetconTest.Api/NetconTest.Api.csproj", "NetconTest.Api/"]
COPY ["NetconTest.Application/NetconTest.Application.csproj", "NetconTest.Application/"]
COPY ["NetconTest.Domain/NetconTest.Domain.csproj", "NetconTest.Domain/"]
COPY ["NetconTest.Infra/NetconTest.Infra.csproj", "NetconTest.Infra/"]

RUN dotnet restore "NetconTest.Api/NetconTest.Api.csproj"

COPY . .

WORKDIR "/src/NetconTest.Api"
RUN dotnet build "NetconTest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetconTest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY NetconTest.Infra/Data/dataset.json ./Data/

ENTRYPOINT ["dotnet", "NetconTest.Api.dll"]