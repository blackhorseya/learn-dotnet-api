FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS builder
WORKDIR /app
EXPOSE 80

COPY ./*.sln .
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*} && mv $file ${file%.*}; done
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine AS runtime
WORKDIR /app
COPY --from=builder /app/out ./
ENTRYPOINT ["dotnet", "Doggy.Learning.WebService.dll"]
