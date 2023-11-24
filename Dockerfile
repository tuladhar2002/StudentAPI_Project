FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /webApp

EXPOSE 80
EXPOSE 443

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final-env
WORKDIR /webApp
COPY --from=build-env /webApp/out .
ENTRYPOINT [ "dotnet", "StudentAPI.dll" ]
