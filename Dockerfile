# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

# Prevent 'Warning: apt-key output should not be parsed (stdout is not a terminal)'
#ENV APT_KEY_DONT_WARN_ON_DANGEROUS_USAGE=1

# install NodeJS 13.x
# see https://github.com/nodesource/distributions/blob/master/README.md#deb
#RUN apt-get update -yq 
#RUN apt-get install curl gnupg -yq 
#RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
#RUN apt-get install -y nodejs

# copy csproj and restore as distinct layers
COPY *.sln .
COPY IdentityGeneration/*.csproj ./IdentityGeneration/
COPY IdentityGeneration.WebApi/*.csproj ./IdentityGeneration.WebApi/
COPY IdentityGeneration.Tests/*.csproj ./IdentityGeneration.Tests/
RUN dotnet restore

# copy everything else and build app
COPY IdentityGeneration/. ./IdentityGeneration/
COPY IdentityGeneration.WebApi/. ./IdentityGeneration.WebApi/
WORKDIR /source/IdentityGeneration.WebApi
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app ./

# See: https://github.com/dotnet/announcements/issues/20
# Uncomment to enable globalization APIs (or delete)
#ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
#RUN apk add --no-cache icu-libs
#ENV LC_ALL en_US.UTF-8
#ENV LANG en_US.UTF-8

ENTRYPOINT ["dotnet", "IdentityGeneration.WebApi.dll"]