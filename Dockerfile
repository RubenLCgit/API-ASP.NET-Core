FROM mcr.microsoft.com/dotnet/sdk:7.0 AS buildApp
WORKDIR /src
COPY . . 
RUN dotnet publish "PetPalApp.Presentation/PetPalApp.Presentation.csproj" -c Release -o /PetPalApp

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /PetPalApp
COPY --from=buildApp /PetPalApp ./
EXPOSE 7216
VOLUME /PetPalApp/SharedForlder
ENTRYPOINT ["dotnet", "PetPalApp.Presentation.dll"]