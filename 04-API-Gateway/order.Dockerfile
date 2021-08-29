FROM quay.io/bukalapak/snowboard
WORKDIR /doc
COPY order.apib .
CMD ["mock", "order.apib"]
EXPOSE 8087
