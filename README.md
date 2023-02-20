# MetalHive
MetalHive API
This is the documentation for the MetalHive API, which provides a way to manage production facilities and contracts. The API is built with ASP.NET Core and uses an SQL Server database hosted on Azure. The API is secured with an API key that needs to be provided with each request.

API Key
The API key is required for each request to the MetalHive API. The key is sent as a header named X-ApiKey with each request. If the API key is missing or invalid, the request will be rejected with an HTTP 401 error.