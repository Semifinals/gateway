// Parameters
param name string
param location string

// Create API Management
resource azApiManagement 'Microsoft.ApiManagement/service@2023-03-01-preview' = {
  name: name
  location: location
  sku: {
    capacity: 0
    name: 'Consumption'
  }
  properties: {
    publisherName: 'Semifinals'
    publisherEmail: 'admin@semifinals.gg'
  }
}

// Outputs
output name string = azApiManagement.name
