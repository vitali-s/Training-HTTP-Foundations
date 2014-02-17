Training-HTTP-Foundations
=========================

Homework:

There is an example of ASP.NET Web API services, it consists of 3 projects:
 - HttpFoundations.Web - empty web application for Web API services;
 - HttpFoundations - web application functionality: bootstrapping, controllers, models;
 - HttpFoundations.Tests - integration tests for Web API services.

Application should be extended to meet following acceptance criteria:
 - Company information (CompanyController controller, Get method) should be compressed using GZip or Deflate data compression algorithms depending on client support. Appropriate behavior should be verified by Fiddler and integration tests for following cases:
 	- Client do not support compression;
 	- Client support only GZip compression;
 	- Client support only Deflate compression;
 	- Client support both GZip and Deflate compression;
 	- Create table with results of traffic size comparison for each method (without compression, GZip, Deflate).
 
 - Offices information (CompanyController controller, GetOffices method) should be cached for 20 seconds. Functionality should be verified using Fiddler and integration tests for following cases:
 	- For two requests in 20 seconds. The first request should content, the second "Not Modified" status;
 	- For two requests in more than 20 seconds. The content should be returned in both cases.

 - Employees information (CompanyController controller, GetEmployees method) should be cached depending on content, if content is not changed, it should not be transferred to the client. In current implementation, the content is changed every 30 seconds. Verify appropriate functionality using Fiddler and integration tests for following cases:
 	- For two requests in 30 seconds. The first request should return content, the second "Not Modified" status;
 	- For two requests in more than 30 seconds. The content should be returned in both cases.

Notes:
 - Example is developed using .NET 4.5, Visual Studio 2013 and Web API 2.1.
 - Example of simple integration test is provided at: HttpFoundations.Tests\Controllers\CompanyControllerTests.cs
 - Functionality should be implemented using action filters System.Web.Http.Filters.ActionFilterAttribute
 - Fiddler could be downloaded at: http://www.telerik.com/download/fiddler
