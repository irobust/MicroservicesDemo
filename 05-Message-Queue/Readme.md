# Asynchronous Messaging with RabbitMQ
## Set up RabbitMQ with Docker
### ติดตั้ง RabbitMQ ผ่านทาง Docker container ด้วยคำสั่ง
```
docker pull rabbitmq:3.8.22-management-alpine
docker run -d --hostname my-rabbit --name some-rabbit -p 5671:5671 -p 5672:5672 -p 15671:15671 -p 15672:15672 rabbitmq:3.8.22-management-alpine
```

ถ้าอยากกำหนด username และ password เองให้เพิ่ม option 2 ตัวนี้
```
-e RABBITMQ_DEFAULT_USER=user 
-e RABBITMQ_DEFAULT_PASS=password
```
### RabbitMQ ports
- 5672 used by AMQP 0-9-1
- 5671 used by AMQP 0-9-1 with TLS
- 15672 API, management UI และ rabbitmqadmin 
- 15692 Prometheus metrics(ต้องติดตั้ง Prometheus plugins)

### เปิด Management Console ได้ที่ http://localhost:15672
Username: guest, Password: guest (default username and password)
* docker exec some-rabbit rabbitmqctl list_queues
* docker exec some-rabbit rabbitmq-plugins list
* docker exec some-rabbit rabbitmq-diagnostics status

### Customize Image(Install plugin rabbit_tracing สำหรับการเก็บ Log)
```
FROM rabbitmq:3.8.22-management-alpine
RUN rabbitmq-plugins enable --offline rabbitmq_tracing
```
build image ที่ต้องการด้วยคำสั่ง
```
docker build -t rabbitmq:3.8.22-tracing .
docker run -d --hostname my-rabbit --name some-rabbit -p 5671:5671 -p 5672:5672 -p 15671:15671 -p 15672:15672 rabbitmq:3.8.22-tracing
```

### ตัวอย่าง Code สำหรับการใช้งาน 
1. Download source code at https://github.com/squaremo/amqp.node
2. เข้าไปที่ Folder examples/tutorials
3. Run npm install (npm install amqplib)
4. เลือกตัวอย่าง code ที่ต้องการดูได้เลย

ดูตัวอย่างการใช้งานเพิ่มเติมได้ที่
https://www.rabbitmq.com/getstarted.html (Tutorials)