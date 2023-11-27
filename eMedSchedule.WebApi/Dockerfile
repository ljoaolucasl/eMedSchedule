#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["eMedSchedule.WebApi/eMedSchedule.WebApi.csproj", "eMedSchedule.WebApi/"]
COPY ["eMedSchedule.Application/eMedSchedule.Application.csproj", "eMedSchedule.Application/"]
COPY ["eMedSchedule.Domain/eMedSchedule.Domain.csproj", "eMedSchedule.Domain/"]
COPY ["eMedSchedule.Infra.Orm/eMedSchedule.Infra.Orm.csproj", "eMedSchedule.Infra.Orm/"]
RUN dotnet restore "eMedSchedule.WebApi/eMedSchedule.WebApi.csproj"
COPY . .
WORKDIR "/src/eMedSchedule.WebApi"
RUN dotnet build "eMedSchedule.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eMedSchedule.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eMedSchedule.WebApi.dll"]