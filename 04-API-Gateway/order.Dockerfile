FROM burakince/drakov
WORKDIR /blueprints
COPY order.apib .
ENTRYPOINT ["drakov", "-f", "order.apib", "-p", "8087"]
EXPOSE 8087
