module.exports = () => {
    var faker = require('faker');
    const data = { deliveries: [] }
    for (let i = 0; i < 10; i++) {
      data.deliveries.push({
          orderID: faker.datatype.number(),
          driverID: faker.datatype.number(),
      })
    }
    return data
  }