FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 60683
EXPOSE 44378

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["api-hangfire/api.hangfire.csproj", "api-hangfire/"]
RUN dotnet restore "api-hangfire/api.hangfire.csproj"
COPY . .
WORKDIR "/src/api-hangfire"
RUN dotnet build "api.hangfire.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "api.hangfire.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "api.hangfire.dll"]