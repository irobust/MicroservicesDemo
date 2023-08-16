FROM kong:latest
CMD ["sh", "-c", "kong migrations bootstrap && ./docker-entrypoint.sh kong docker-start"]
USER kong