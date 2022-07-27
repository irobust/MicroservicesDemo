FROM clue/json-server
WORKDIR /data
COPY product-service.js .
RUN "npm install faker"
ENTRYPOINT [ "json-server", "--host", "0.0.0.0", "product-service.js"]
EXPOSE 3000