# Contract Testing
เราต้่องทำการทดสอบว่า Contract นั้นตรงกับ API หรือไม่

## Contract Test ด้วย Dredd
### Setup Gists API
เราจะใช้ Gists API เป็นตัวอย่างในการทดสอบซึ่งเป็นตัวอย่างที่มาจาก document ของ dredd เอง ดูตัวอย่างเพิ่มเติมได้่ที่ https://github.com/apiaryio/dredd-example.git

ทำการติดตั้ง dredd
```
npm i -g dredd
```

> เปลี่ยน Directory เข้าไปใน dredd-example ด้วยคำสั่ง (cd dredd-example)

Start mongoDB จาก docker container ด้วยคำสั่ง
```
docker run --name mongodb --rm -p 27017:27017 mongo:5
```
ติดตั้ง Package ที่จะใช้ในตัวอย่างที่เขียนด้วย express (Node JS)
```
npm install
```
Start express ขึ้นมาด้วยคำสั่ง
```
npm start
```

เปิดดู service ที่ start ขึ้นมาได้ที่ http://localhost:3000

### ทดสอบ API Blueprint เทียบกับ OpenAPI
```
dredd ./apiblueprint/api.apib http://localhost:3000
dredd ./openapi2/api.yml http://localhost:3000
```

> Hook จะใช้ในการเพิ่มหรือลบข้อมูลใน Database ก่อนที่จะทำการทดสอบใน endpoint(http method + uri) ต่างๆ

### ทดสอบ API Blueprint แบบมี hook
```
dredd ./apiblueprint/api.apib http://localhost:3000 -f ./apiblueprint/hook.js
```

### ทดสอบ Open API แบบมี hook
```
dredd ./openapi2/api.yml http://localhost:3000 -f ./openapi2/hooks.js
```

### Dredd configuration
กำหนดค่าต่างๆไว้ใน config file จะได้เรียกใช้งานด้วยคำสั่งสั้นๆ

เริ่มจากสร้าง config file ด้วยคำสั่ง
```
dredd init
```

ทดสอบโดยที่กำหนด configuration ไว้ใน file dredd.yml
```
dredd --config ./openapi2/dredd.yml
dredd --config ./apiblueprint/dredd.yml
```
