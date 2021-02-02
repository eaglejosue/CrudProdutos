#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/CadastroProduto.Web/CadastroProduto.Web.csproj", "src/CadastroProduto.Web/"]
COPY ["src/CadastroProduto.Application/CadastroProduto.Application.csproj", "src/CadastroProduto.Application/"]
COPY ["src/CadastroProduto.Domain/CadastroProduto.Domain.csproj", "src/CadastroProduto.Domain/"]
COPY ["src/CadastroProduto.Data/CadastroProduto.Data.csproj", "src/CadastroProduto.Data/"]
COPY ["src/CadastroProduto.IoC/CadastroProduto.IoC.csproj", "src/CadastroProduto.IoC/"]
RUN dotnet restore "src/CadastroProduto.Web/CadastroProduto.Web.csproj"
COPY . .
WORKDIR "/src/src/CadastroProduto.Web"
RUN dotnet build "CadastroProduto.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CadastroProduto.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CadastroProduto.Web.dll"]