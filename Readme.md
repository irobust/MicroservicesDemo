# Building MicroServices
การสร้าง MicroServices ด้วย Greenfield Approach(ไม่เคยมีระบบเดิมมาก่อน) ดังนั้นเราจะเริ่มต้นด้วยขั้นตอนต่างๆ ดังนี้

## Steps to create MicroServices
1. Bounded Context
    - Context
    - Core Concept
    - Supporting Concept

ผลลัพธ์ที่ได้
![Artifact from bounded context process](imgs/bounded-context-example.png)

2. Supporting Concept -> Contract 
    - apiblueprint (Design first)
    - openAPI (Code first)
    - RAML

3. Mocking service
    - Mocking apiblueprint
        * drakov
        * snowboard
    - Mocking openAPI
        * prism
    - Mocking without Contract
        * json-server

4. Contract Test
    - dredd **
    - pact

5. Integration Test (API Testing)
    - Postman + newman
    - Soap UI

Before All -> Create connection to mongoDB
    Before each -> none
        Test case 1
    After each -> delete gists and star

    Before each -> none
        Test case 2
    After each -> delete gists and star
After All -> Close connection
