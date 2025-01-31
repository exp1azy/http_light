# HttpLight

**HttpLight** is a lightweight and efficient HTTP client for .NET, designed to simplify HTTP requests with built-in response conversion and retry logic. 
Unlike `HttpClient`, it reduces boilerplate code and enhances reliability with automatic retries and customizable delays.
Supporting both synchronous and asynchronous methods, HttpLight ensures flexibility while maintaining performance. 
It provides seamless handling for **GET, POST, PUT, DELETE,** and multipart form data requests, making API integration effortless.
With its **minimalistic design, low memory consumption, and high efficiency, HttpLight** is the perfect choice for developers looking for a simple yet powerful HTTP client.

## Documentation

Additional:
1. `Dispose()` - Releases unmanaged resources and disposes the internal `HttpClient`.
2. `SetDefaultHeaders()` - Sets default headers for the HTTP client.
3. `RemoveDefaultHeaders()` - Removes all default headers from the HTTP client.
4. `SetTimeout()` - Sets the timeout for the HTTP client.
5. `RemoveTimeout()` - Removes the timeout from the HTTP client.
6. `SetBearerToken()` - Sets the bearer token for the HTTP client.
7. `RemoveAuthorizationHeader()` - Removes the bearer token from the HTTP client.
8. `Send()` - Sends a synchronous HTTP request using the specified `HttpRequestMessage` and returns the response deserialized to the specified type.
9. `SendAsync()` - Sends aa asynchronous HTTP request using the specified `HttpRequestMessage` and returns the response deserialized to the specified type.

GET methods:
1. `Get()` - Synchronously executes a GET request.
2. `GetAsync()` - Asynchronously performs a GET request.
3. `GetWithRetry()` - Synchronously performs the specified number of attempts to send a GET request with the specified delay between each attempt.
4. `GetWithRetryAsync()` - Asynchronously performs the specified number of attempts to send a GET request with the specified delay between each attempt.

POST methods:
1. `Post()` - Synchronously executes a POST request.
2. `PostAsync()` - Asynchronously executes a POST request.
3. `PostWithRetry()` - Synchronously performs the specified number of attempts to send a POST request with the specified delay between each attempt.
4. `PostWithRetryAsync()` - Asynchronously performs the specified number of attempts to send a GET request with the specified delay between each attempt.
5. `PostFormData()` - Sends a synchronous HTTP POST request with multipart/form-data content and returns the response deserialized to the specified type.
6. `PostFormDataAsync()` - Sends an asynchronous HTTP POST request with multipart/form-data content and returns the response deserialized to the specified type.

PUT methods:
1. `Put()` - Synchronously executes a PUT request.
2. `PutAsync()` - Asynchronously executes a PUT request.
3. `PutWithRetry()` - Synchronously performs the specified number of attempts to send a PUT request with the specified delay between each attempt.
4. `PutWithRetryAsync()` - Asynchronously performs the specified number of attempts to send a PUT request with the specified delay between each attempt.
5. `PutFormData()` - Sends a synchronous HTTP PUT request with multipart/form-data content and returns the response deserialized to the specified type.
6. `PutFormDataAsync()` - Sends an asynchronous HTTP PUT request with multipart/form-data content and returns the response deserialized to the specified type.

PATCH methods:
1. `Patch()` - Synchronously executes a PATCH request.
2. `PatchAsync()` - Asynchronously executes a PATCH request.
3. `PatchWithRetry()` - Synchronously performs the specified number of attempts to send a PATCH request with the specified delay between each attempt.
4. `PatchWithRetryAsync()` - Asynchronously performs the specified number of attempts to send a PUT request with the specified delay between each attempt.
5. `PatchFormData()` - Sends a synchronous HTTP PATCH request with multipart/form-data content and returns the response deserialized to the specified type.
6. `PatchFormDataAsync()` - Sends an asynchronous HTTP PATCH request with multipart/form-data content and returns the response deserialized to the specified type.

DELETE methods:
1. `Delete()` - Sends a synchronous HTTP DELETE request to the specified URL and returns the response deserialized to the specified type.
2. `DeleteAsync()` - Sends an asynchronous HTTP DELETE request to the specified URL and returns the response deserialized to the specified type.
3. `DeleteWithRetry()` - Sends a synchronous HTTP DELETE request to the specified URL with retry logic, and returns the response deserialized to the specified type.
4. `DeleteWithRetryAsync()` - Sends an asynchronous HTTP DELETE request to the specified URL with retry logic, and returns the response deserialized to the specified type.

## Usage

To use the library functionality, you need to initialize the `HttpLight` object:
```csharp
var httpLight = new HttpLight();
```

Below is an example of sending a GET request:
```csharp
var response = httpLight.Get<OutputModel>("https://jsonplaceholder.typicode.com/posts/1");
Console.WriteLine(response.Body);
```

Below is an example of sending a POST request:
```csharp
var request = new OutputModel
{
    UserId = 1,
    Title = "title",
    Body = "body"
};

var json = JsonConvert.SerializeObject(request);
var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
var response = httpLight.Post<OutputModel>("https://jsonplaceholder.typicode.com/posts", content);

Console.WriteLine(response.Title);
```

## Installation

To install the package, use the following command:
```bash
dotnet add package HttpLight
```
or via NuGet Package Manager:
```bash
Install-Package HttpLight
```
