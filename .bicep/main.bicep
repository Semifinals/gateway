targetScope = 'subscription'

// Parameters
@description('The environment to build in')
@allowed([
  'dev'
  'prod'
])
param environment string

@description('The location to deploy to')
param location string

// Names
var product = 'gateway'
var suffix = '-sf-${product}-${environment}'

var resourceGroup = 'rg${suffix}'
var apiManagement = 'apim${suffix}'

// Create resource group
resource azResourceGroup 'Microsoft.Resources/resourceGroups@2023-07-01' = {
  name: resourceGroup
  location: location
}

// Create APIM
module azApiManagement 'modules/azApiManagement.bicep' = {
  scope: azResourceGroup
  name: 'apiManagement'
  params: {
    name: apiManagement
    location: location
  }
}

// Outputs
output resourceGroupName string = azResourceGroup.name
output apiManagementName string = azApiManagement.outputs.name
