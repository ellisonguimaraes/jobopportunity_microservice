#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JobOpportunityMicroservice.API/JobOpportunityMicroservice.API.csproj", "JobOpportunityMicroservice.API/"]
COPY ["JobOpportunityMicroservice.Infra.CrossCutting.IoC/JobOpportunityMicroservice.Infra.CrossCutting.IoC.csproj", "JobOpportunityMicroservice.Infra.CrossCutting.IoC/"]
RUN dotnet restore "JobOpportunityMicroservice.API/JobOpportunityMicroservice.API.csproj"
COPY . .
WORKDIR "/src/JobOpportunityMicroservice.API"
RUN dotnet build "JobOpportunityMicroservice.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobOpportunityMicroservice.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobOpportunityMicroservice.API.dll"]
