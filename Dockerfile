FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ExpenseTrackerApi.slnx .
COPY ExpenseTrackerApi/ExpenseTrackerApi.csproj                                 ExpenseTrackerApi/
COPY ExpenseTrackerApi.Presentation/ExpenseTrackerApi.Presentation.csproj       ExpenseTrackerApi.Presentation/
COPY Entities/Entities.csproj                                                   Entities/
COPY Contracts/Contracts.csproj                                                 Contracts/
COPY Repository/Repository.csproj                                               Repository/
COPY Service/Service.csproj                                                     Service/
COPY Service.Contracts/Service.Contracts.csproj                                 Service.Contracts/
COPY Shared/Shared.csproj                                                       Shared/

RUN dotnet restore

COPY . .
RUN dotnet publish ExpenseTrackerApi/ExpenseTrackerApi.csproj -c Release -o /app/publish \
    -p:EnableDefaultEmbeddedResourceItems=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ExpenseTrackerApi.dll"]