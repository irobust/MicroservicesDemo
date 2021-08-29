# Mocking Server
การจำลองหรือ Mocking Server จะเป็นส่วนสำคัญที่จะช่วยให้เราสามารถทำงานแยกกันแบบ 1 team 1 service ได้

การจำลอง service ขึ้นมามี 3 ลักษณะคือ
* การจำลอง Service โดยใช้ API Blueprint
* การจำลอง Service โดยใช้ OpenAPI
* การจำลอง Service โดยไม่ใช้ Contract

> ก่อนที่จะ run แต่ละคำสั่งให้เปลี่ยน directory มาที่ 01-Mocking-Service ก่อน

## การ Mock Service โดยใช้ Api blueprint (Drakov)
1. สร้างไฟล์ sample.apib (https://pastebin.com/U9dJzPKe)
2. Install VSCode Extension (API Blueprint Viewer)
3. npm i -g drakov
4. drakov -f **/*.apib
5. http://localhost:3000/order

## การ Mock Service โดยใช้ Api blueprint (Snowboard)
1. docker pull quay.io/bukalapak/snowboard
2. Change directory to /PATH TO sample.apib/
3. docker run -it -v ${PWD}:/doc -p 8087:8087 --rm quay.io/bukalapak/snowboard mock sample.apib
4. http://localhost:8087/order
5. New Terminal
6. docker run -it -v ${PWD}:/doc -p 8088:8088 --rm quay.io/bukalapak/snowboard http sample.apib

## การ Mock Service โดยใช้ JSON Server
### ติดตั้ง json-server
```
npm i -g json-server
```

### Fake product service
1. npm i faker
2. json-server product-service.js
3. http://localhost:3000

### Mocking services โดยใช้หลายๆ port
1. json-server product-service.js -p 3000
2. json-server delivery-service.js -p 3001

### Mocking services จาก JavaScript หลายๆไฟล์
```
json-server modules.js
```

## การ Mock Service โดยใช้ Swagger (Prism)
ดูรายละเอียดเพิ่มเติมได้ที่นี่
https://github.com/stoplightio/prism
