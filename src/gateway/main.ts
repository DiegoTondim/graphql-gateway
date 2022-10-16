const gateway = require('gql-gateway')

const port = 5200

const endpointsList = [
   { name: 'productsApi', url: 'http://localhost:5197/swagger/v1/swagger.json' },
   { name: 'flightsApi', url: 'http://localhost:5100/swagger/v1/swagger.json' }
]

gateway({ endpointsList })
  .then(server => server.listen(port))
  .then(console.log(`Service is now running at port: ${port}`))
  .catch(err => console.log(err))