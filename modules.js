var products = require('./product-service.js')();
var deliveries = require('./delivery-service.js')();
module.exports = () => {
    return {
        products: products,
        deliveries: deliveries
    }
}

