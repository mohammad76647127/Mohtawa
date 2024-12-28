# Use a lightweight base image for .NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["Mohtawa.Services.API.dll", "./"]
RUN dotnet restore

# Copy the rest of the application files
COPY . .
WORKDIR /src

# Build the application in Release mode
RUN dotnet build -c Release -o /app/build

# Publish the application to a runtime-friendly format
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Use the runtime base image for the final stage
FROM base AS final
WORKDIR /app

# Create a non-root user and switch to it
RUN addgroup --system appgroup && adduser --system appuser --ingroup appgroup
USER appuser

COPY --from=publish /app/publish .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Mohtawa.Services.API.dll"]