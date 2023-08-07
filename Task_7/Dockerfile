FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Task_7/Task_7.csproj", "Task_7/"]
RUN dotnet restore "Task_7/Task_7.csproj"
COPY . .
WORKDIR "/src/Task_7"
RUN dotnet build "Task_7.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Task_7.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task_7.dll"]
