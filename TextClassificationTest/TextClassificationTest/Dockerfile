FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TextClassificationTest/TextClassificationTest.csproj", "TextClassificationTest/"]
RUN dotnet restore "TextClassificationTest/TextClassificationTest.csproj"
COPY . .
WORKDIR "/src/TextClassificationTest"
RUN dotnet build "TextClassificationTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TextClassificationTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TextClassificationTest.dll"]
