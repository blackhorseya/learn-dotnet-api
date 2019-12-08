FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS builder
WORKDIR /app
EXPOSE 80

COPY ./*.sln .
COPY ./src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*} && mv $file src/${file%.*}; done
RUN dotnet restore ./src/Doggy.Learning.WebService

COPY ./src ./src
RUN dotnet publish ./src/Doggy.Learning.WebService -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-alpine AS runtime
WORKDIR /app
COPY --from=builder /app/out ./
ENTRYPOINT ["dotnet", "Doggy.Learning.WebService.dll"]
