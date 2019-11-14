FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS builder
WORKDIR /app
EXPOSE 80

COPY ./*.sln .
COPY ./src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*} && mv $file src/${file%.*}; done
RUN dotnet restore ./src/Doggy.Learning.WebService

COPY ./src ./src
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS runtime
WORKDIR /app
COPY --from=builder /app/out ./
ENTRYPOINT ["dotnet", "Doggy.Learning.WebService.dll"]
