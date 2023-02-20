# MetalHive API

## Introduction
This is the documentation for the MetalHive API, which provides a way to manage production facilities and contracts. The API is built with ASP.NET Core and uses an SQL Server database hosted on Azure. The API is secured with an API key that needs to be provided with each request.

## Technology Stack
* C#
* .NET 5
* Entity Framework Core
* SQL Server
* Azure

## Prerequisites
To run this application, you will need:
* Visual Studio 2019 or later
* .NET 5 SDK
* SQL Server Management Studio (SSMS) or Azure SQL

## Usage
1. Clone the repository to your local machine.

```
git clone https://github.com/your-username/metalhive-api.git
```

2. Navigate to the project directory and restore packages.

```
cd metalhive-api
dotnet restore
```
3. Update the ***appsettings.json*** file with your connection string.

```
{
  "ConnectionStrings": {
    "AzureDB": "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"
  }
}
```
3. Run the application.

```
dotnet run
```


## API endpoints

| Endpoint | Method | Description |
| -------- | -------- | -------- |
| */api/contracts*  | ***GET*** | Returns a list of all contracts. |
| */api/contracts*  | ***POST*** | Creates a new contract. |

### Create contract

This endpoint creates a new contract between a client and a production facility.

#### Request

```
POST /api/contracts
Content-Type: application/json

{
    "equipmentId": 1,
    "productionFacilityId": 1,
    "clientName": "Lucy"
}

```

| Parameter | Type | Description |
| -------- | -------- | -------- |
| *equipmentId*  | ***int*** | The ID of the equipment type.. |
| *productionFacilityId*  | ***int*** | The ID of the production facility. |
| *clientName*  | ***string*** | The name of the client. |

#### Response

Returns the created contract.

`201 Created`: the contract was successfully created. The response body will contain a JSON object representing the created contract.

Example:

```
HTTP/1.1 201 Created
Content-Type: application/json

{
    "id": 1,
    "equipmentId": 1,
    "productionFacilityId": 1,
    "clientName": "Lucy"
}

```

`400 Bad Request`: the request body is invalid or the specified equipment or production facility does not exist. The response body will contain a JSON object with an error code and description.

#### Error codes:

| ErrorCode  | Description |
| -------- |  -------- |
| `6001`   | This error occurs when the specified equipment type does not exist in the system. |
| `6002`|   This error occurs when the specified production facility does not exist in the system. |
| `6003`|   This error occurs when the specified production facility is already assigned to another contract.|
| `6004`|   This error occurs when there is not enough available production space for the requested equipment|


Example:

```
{
    "errorCode": 6001,
    "errorDescription": "The specified equipment type does not exist."
}


```

### Get contracts

This endpoint returns a list of all contracts.

#### Request

```
GET /api/contracts

```

#### Response

Returns a list of all contracts.

Example:

```
HTTP/1.1 200 OK
Content-Type: application/json

[
    {
        "id": 1,
        "equipmentId": 1,
        "productionFacilityId": 1,
        "clientName": "Lucy"
    },
    {
        "id": 2,
        "equipmentId": 2,
        "productionFacilityId": 2,
        "clientName": "Maksym"
    }
]

```
## Dependencies

* *Microsoft.EntityFrameworkCore*
* *Microsoft.EntityFrameworkCore.SqlServer*
* *Microsoft.EntityFrameworkCore.Tools*
* *Swashbuckle.AspNetCore*

## License

This project is licensed under the [MIT License](https://opensource.org/license/mit/).
