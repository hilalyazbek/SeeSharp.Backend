FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7777

ENV ASPNETCORE_URLS=http://+:7777

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/SeeSharp.Api/SeeSharp.Api.csproj", "src/SeeSharp.Api/"]
RUN dotnet restore "src/SeeSharp.Api/SeeSharp.Api.csproj"
COPY . .
WORKDIR "/src/src/SeeSharp.Api"
RUN dotnet build "SeeSharp.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "SeeSharp.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SeeSharp.Api.dll"]
