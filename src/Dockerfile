FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# create dependencies layer
COPY *.sln .
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore

# move all code content 
# project run will be discard since this project only needs to run tests
COPY . ./
WORKDIR /source