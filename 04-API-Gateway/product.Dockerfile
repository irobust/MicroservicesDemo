FROM williamyeh/json-server
WORKDIR /data
COPY product-service.js .
RUN ["npm", "install", "faker"]
ENTRYPOINT [ "json-server", "product-service.js", "-q" ]
EXPOSE 3000