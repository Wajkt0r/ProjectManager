FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProjectManager.MVC/ProjectManager.MVC.csproj", "ProjectManager.MVC/"]
COPY ["ProjectManager.Application/ProjectManager.Application.csproj", "ProjectManager.Application/"]
COPY ["ProjectManager.Domain/ProjectManager.Domain.csproj", "ProjectManager.Domain/"]
COPY ["ProjectManager.Infrastructure/ProjectManager.Infrastructure.csproj", "ProjectManager.Infrastructure/"]
RUN dotnet restore "ProjectManager.MVC/ProjectManager.MVC.csproj"
COPY . .
WORKDIR "/src/ProjectManager.MVC"
RUN dotnet build "ProjectManager.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjectManager.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/certificates
COPY ProjectManager.MVC/Certificates/aspnetapp.pfx /app/certificates

ENTRYPOINT ["dotnet", "ProjectManager.MVC.dll"]