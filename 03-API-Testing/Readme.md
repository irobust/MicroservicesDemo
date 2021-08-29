# API Testing(Integration Test)
การทำ Integration Test มีส่วนสำคัญมากในการทำ Microservices เนื่องจากเราไม่สามารถจบการทำงานที่ service เดียวได้อีกต่อไป เราจึงต้องทำ Integration Test อย่างหนัก เพื่อให้มั่นใจได้ว่าการเปลี่่ยนแปลงที่เกิดขึ้นกับ API ของเราจะไม่ทำให้ใครเสียหาย

> Testing ผ่านหน้า UI เราจะใช้ **Postman** แต่ถ้าจะ Test ผ่านทาง commandline เราจะใช้ **newman**

## API Testing ด้วย Postman
ดูขั้นตอนการเขียน script เพื่อทำ API Testing เพิ่มเติมได้ที่ https://github.com/DannyDainton/All-Things-Postman

### Postman Variables
ใน postman มีตัวแปรอยู่ 4 รูปแบบคือ
1. Environment (ใช้เมื่อต้องการเปลี่ยน environment เช่น BASE_URL ใน developemnt กับ prooduction จะไม่เหมือนกัน)
2. Globals (share ข้าม collection ได้)
3. Collection Variables (ใช้เฉพาะใน collection เท่านั้น)
4. Variables (ใช้สำหรับ share ตัวแปรระหว่างหน้า pre-request script กับ request bdy)

เราสามารถ load JavaScript เข้ามาใช้งานใน postman ได้ด้วยคำสั่ง
```
const moment = require('moment');
```

อ่านค่าและกำหนดค่าของตัวแปรใน Global scope ได้ด้วยคำสั่ง
```
const id = pm.globals.get("id");
pm.globals.set("id", id + 1);
```

random สมาชิก 1 ตัวออกมาจาก array ที่กำหนดด้วย _.sample (postman จะโหลด javascript library ที่ชื่ีอว่า lodash มาให้เราอยู่แล้ว)
```
const gistsData = [{
 "description": "Gist description...1",
 "content": "String content...1",
 "createdAt": moment().format('YYYY-MM-DD')
},{
 "description": "Gist description...2",
 "content": "String content..2.",
 "createdAt": moment().format('YYYY-MM-DD')
},{
 "description": "Gist description...3",
 "content": "String content...3",
 "createdAt": moment().format('YYYY-MM-DD')
}]
 
const selectedData = _.sample(gistsData);
```

จะเก็บค่าไว้ในตัวแปรทีละตัวแบบนี้
```
pm.variables.set("_id", selectedData._id);
pm.variables.set("description", selectedData.description);
pm.variables.set("content", selectedData.content);
pm.variables.set("createdAt", selectedData.createdAt);
```
หรือเก็บไว้เป็น Object เลยแบบนี้ก็ได้
```
pm.variables.set('selected_data', selectedData)
```
แต่เราจะนำไปใช้ได้แค่ {{slected_data}} เท่านั้น ไม่สามารถจะใช้ {{selected_data.content}} ได้


### Postman Test-case
ทดสอบว่า http status ที่กลับมาเป็น 200 หรือไม่
```
pm.test("Get all gists successfully", function () {
   pm.response.to.have.status(200);
});
```
 
ทดสอบว่า response ที่ได้กลับมามีคำว่า _link อยู่หรือไม่ (อยู่ใน header หรือ body ก็ได)
```
pm.test("Gists matches string", function () {
   pm.expect(pm.response.text()).to.include("_links");
});
```
 
JSON Object ที่ return กลับมา**ทุุกตัว**ต้องมี attribute content (content ไม่เท่ากับ undefined)
```
pm.test("Every gists must have content", function(){
   const gists = pm.response.json()._embedded.gists;
   pm.expect(gists.every((gist) => {
       return gist.content != undefined;
   })).to.be.true
});
```

JSON Object ที่ return กลับมาต้องมี**อย่างน้อย 1 ตัว**ที่มี attribute content (content ไม่เท่ากับ undefined)
```
pm.test("At least one git has content", function(){
   const gists = pm.response.json()._embedded.gists;
   pm.expect(gists.any((gist) => {
       return gist.content != undefined;
   })).to.be.true
});
```

ทดสอบว่า gist ที่มีค่า id สูงที่สุด มี id เท่ากับตัวแปร id ที่อยู่ใน globals หรือไม่
```
pm.test("Check Latest gist matches current id", function(){
   const gists = pm.response.json()._embedded.gists;
   pm.expect(gists.any((gist) => {
       return gist.id == pm.globals.get("id");
   })).to.be.true
});
```
### API Testing ด้วย Newman
ก่อนจะใช้งาน postman ต้อง Export Collection กับ Enviroment ออกมาจาก Postman ก่อน

#### ติดตั้ง Newman ผ่านทาง Npm(Node JS)
ติดตั้ง newman ด้วยคำสั่ง
```
npm install -g newman 
```

run newman ด้วยคำสั่ง
```
newman run [ชื่อ Collection] -e [ชื่อ Environment] --global-var id=1 -n 3
```

### ติดตั้ง Newman ผ่านทาง Docker container
> ต้องแก้ไฟล์ Collection โดยเปลี่ยน variable.value จาก localhost ไปเป็น host.docker.internal

#### สำหรับผู้ที่ใช้งาน Docker ผ่านทาง Hyper-V ต้องทำการ Share file ก่อน
โดยมีขั้นตอนดังนี้
1. เลือก Setting ของ Docker
2. Share Drive C: (ถ้า Save collections ไว้ที่ Drive C)
   ถ้า Username ของเครื่องไม่มี Password ต้องไป set password ก่อน
3. Apply and Restart
4. วางไฟล์ Collection และ Environment ไว้ใน Folder C:\NewmanDemo

run newman ผ่านทาง docker container ด้วยคำสั่งนี้
```
docker pull postman/newman_alpine33
docker run -v ${PWD}/gist.collection.json:/etc/newman/gist.collection.json
	 --rm -t postman/newman_alpine33 
	run [ชื่อไฟล์ Collection ที่ Export ออกมา].json 
		   -e [ชื่อไฟล์ Environment Variable].json
```

