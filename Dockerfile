FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY PetPalApp.Domain/*.csproj PetPalApp.Domain/
COPY PetPalApp.Business/*.csproj PetPalApp.Business/
COPY PetPalApp.Data/*.csproj PetPalApp.Data/
COPY PetPal.API/*.csproj PetPalAPI/
RUN dotnet restore PetPalAPI/PetPalApp.API.csproj

COPY . .
RUN dotnet publish PetPalAPI.sln -c Release -o API/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/API/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "PetPalApp.API.dll"]