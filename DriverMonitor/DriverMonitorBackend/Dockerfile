FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0 AS build-env
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install --global dotnet-ef --version 6.*

COPY ./entrypoint.sh /
RUN sed -i 's/\r//' /entrypoint.sh
RUN chmod +x /entrypoint.sh

WORKDIR /app

CMD /entrypoint.sh