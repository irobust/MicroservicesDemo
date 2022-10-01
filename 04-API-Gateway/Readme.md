# API Gateway
## Preparation
```
docker pull kong
docker pull cassandra:3.11
docker pull pantsel/konga:latest
docker pull williamyeh/json-server
```
## Docker Container
Create network
```
docker network create kong-net
docker network ls
```

Create cassandra container
```
docker run -d --name kong-database --network=kong-net -p 9042:9042 cassandra:3.11
```

Generate database schema
```
docker run --rm --network=kong-net -e "KONG_DATABASE=cassandra" -e "KONG_CASSANDRA_CONTACT_POINTS=kong-database" kong:latest kong migrations bootstrap
```

Start Kong
```
docker run -d --name kong --network=kong-net -e "KONG_DATABASE=cassandra" -e "KONG_CASSANDRA_CONTACT_POINTS=kong-database" -e "KONG_ADMIN_LISTEN=0.0.0.0:8001" -p 8000:8000 -p 8001:8001 -p 8443:8443 -p 8444:8444 kong:latest
```

Create Fake Product Service with Json-Server
1. Share file api.js เข้าไปใน container(สำหรับ docker ที่ run ผ่าน Hyper-V)
2. ติดตั้ง Faker
``` 
npm install faker
```
3. Mock Product Service ด้วย Json-server
```
docker run --rm --name product-service --network kong-net  -p 3000:3000 -v ${PWD}:/data williamyeh/json-server product-service.js
```

Create Fake Order Service with Snowboard
```
docker run --name=order-service --network kong-net -it -v ${PWD}:/doc -p 8087:8087 --rm quay.io/bukalapak/snowboard mock order.apib
```


## Docker Compose
Start docker compose ด้วยคำสั่ง
```
docker-compose up -d
```

### Docker Compose Services
1. Kong Database (Cassandra)
2. Kong - http://localhost:8001/status
3. Konga (GUI for manage Kong) - http://localhost:1337
4. Product Service (Json-server) - http://localhost:8087/orders
5. Order Services (apiblueprint) - http://localhost:3000/products

## Kong Ports
* 8000 -> Gateway
* 8001 -> Admin port
* 8443 -> Https gateway
* 8444 -> Https admin port

check kong status
```
curl http://localhost:8001
curl http://localhost:8001/status
```

