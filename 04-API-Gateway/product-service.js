module.exports = () => {
    var faker = require('faker');
    const data = { products: [] }
    for (let i = 0; i < 10; i++) {
      data.products.push({
          productCode: faker.commerce.product(),
          productName: faker.commerce.productName(),
          price: faker.commerce.price(),
          rating: faker.datatype.number({min: 1, max: 5}),
          comments: [
              { message: 'Test Comment 1' },
              { message: 'Test Comment 2' },
              { message: 'Test Comment 3' }
          ]
      })
    }
    return data
  }