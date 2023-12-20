# ApiUtilities
### A lightweight common api wrapper utilities nuget package.

APIUtilities is a comprehensive library designed to accelerate the development of API wrapper NuGet packages. It provides a common layer for base services, common testing classes, and utilities to streamline the creation of API wrappers for various services.

# Features
Base Services: Abstract classes and interfaces for common API functionalities.
Common Testing Classes: Helper classes for testing API wrappers, making it easier to write unit tests.
Utilities: Additional utilities to simplify the implementation of API wrapper functionality.
# Getting Started
## Installation
Install the APIUtilities NuGet package using the following command:

bash
Copy code
dotnet add package APIUtilities
Usage
Base Services: Inherit from the provided base classes and interfaces to create API-specific services.
```
// Example of using BaseService
public class MyApiService : BaseService
{
    // Implement your API-specific logic here
}
```
Common Testing Classes: Utilize the provided testing classes for writing unit tests for your API wrapper.
```
// Example of using CommonTestHelper
public class MyApiTests
{
    [Fact]
    public void TestApiFunctionality()
    {
        var api = new MyApiService();
        var testHelper = new CommonTestHelper(api);

        // Write your unit tests using the test helper
    }
}
```
Utilities: Explore additional utilities to enhance your API wrapper development.
```
// Example of using common utilities
var result = APIUtilityHelper.ExecuteApiRequest(apiRequest);
```
# Contributing
Contributions are welcome! Please follow the contribution guidelines when submitting pull requests.

# License
This project is licensed under the MIT License.
