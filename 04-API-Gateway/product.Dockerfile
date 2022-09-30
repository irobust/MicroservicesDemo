FROM clue/json-server
WORKDIR /data
COPY product-service.js .
RUN /bin/bash -c "npm i @faker-js/faker"
ENTRYPOINT [ "json-server", "--host", "0.0.0.0", "product-service.js"]
EXPOSE 3000