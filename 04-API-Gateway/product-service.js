module.exports = () => {
    const data = { products: [] }
    for (let i = 0; i < 10; i++) {
      data.products.push({
          productCode: `P000${i}`,
          productName: `Sample product ${i}`,
          price: 100,
          rating: 1,
          comments: [
              { message: 'Test Comment 1' },
              { message: 'Test Comment 2' },
              { message: 'Test Comment 3' }
          ]
      })
    }
    return data
  }