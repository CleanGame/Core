FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CleanGame.UI/CleanGame.UI.csproj", "CleanGame.UI/"]
RUN dotnet restore "CleanGame.UI/CleanGame.UI.csproj"
COPY . .
WORKDIR "/src/CleanGame.UI"
RUN dotnet build "CleanGame.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanGame.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanGame.UI.dll"]
