#Get base sdk Image from Microsoft
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 As build-env
WORKDIR /app

#Copy the CSPROJ file and restore any dependancies (via NUGET)
COPY *.csproj ./
RUN dotnet restore

#Copy Project file and build our release 
COPY . ./
RUN dotnet publish -c release -o out

#Generate runtime image 
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ZipPay.dll"]
