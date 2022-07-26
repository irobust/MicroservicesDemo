FROM burakince/drakov
WORKDIR /blueprints
COPY order.apib .
CMD ["-f", "order.apib", "-p", "8087"]
EXPOSE 8087
