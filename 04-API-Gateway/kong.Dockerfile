FROM kong:latest
COPY ./wait-for.sh wait-for.sh
USER root
RUN chmod +x wait-for.sh
# CMD ["./wait-for.sh", "kong-database:9042", "&& kong migrations bootstrap"]
USER kong