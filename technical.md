# ParkBee technical questions

1. How would you improve the API to make it production-ready (bug fixes, security, performance, etc.)?

   API can be improved in the following ways:-
   <br/>
   1) Thorough unit testing and integration testing of all possible scenarios of any acceptance criteria including negative test case scenarios.
   2) Testing the API application by using automation testing tool.
   3) Using specific tools to improve coding practices(Possible null exception handling, reduce redundant codes).
   4) Implement an error handling system to gracefully handle any unexpected errors and prevent crashes.
   5) Logging exceptions which will help to find the root cause.
   6) Implement authentication and authorization mechanisms to ensure that only authorized users can access the API.
   7)  Optimize the API code and database queries to improve response times and reduce latency.
   8)  Implement caching mechanisms to store frequently accessed data and reduce the load on the server.
   9)  Use load balancing and scaling techniques to handle increasing traffic and ensure high availability.
   10)  Set up monitoring service like azure appinsight to monitor API's performance, exceptions and traces.
   11)  Set up a CI/CD pipeline to automate testing, building, and deployment processes, ensuring that updates and changes can be rolled out reliably and efficiently.
   12)  Store application keys like connection string in environment specific appsettings file or azure key vault securely.

  2. How would you make the API idempotent?

     API can be made idempotent by following ways:-
     1. Use Appropriate HTTP Methods.Ensure that the API endpoints use the appropriate HTTP methods as per RESTful best practices. In particular, use the "GET" method for retrieving data and "POST", "PUT", "DELETE" methods for creating, updating, and deleting resources respectively.
     2. Design the API endpoints in a way that multiple identical requests can be made without causing different outcomes. For instance, if a "PUT" request is used to update a resource, ensure that subsequent identical "PUT" requests result in the same state as the first one.
     3. Utilize unique identifiers for resources to prevent duplicate resource creation.
     4. Ensure that the API provides proper feedback through responses, including HTTP status codes, to indicate the success or failure of requests.
     5. If the API involves database operations, use database transactions to maintain consistency and ensure that operations are atomic, meaning they either fully succeed or are fully rolled back.

3. How would you approach the API authentication?

   In .NET Core, we can approach API authentication and authorization using JWT (JSON Web Tokens) by leveraging the built-in authentication and authorization middleware provided by the framework and integrating with the JWT authentication scheme. In our API endpoints, we can validate and extract claims from the incoming JWT token to make authorization decisions based on the user's identity and permissions.

4. What type of storage would you use for this service in production?

   We can use Relational Database Management System like SQL Server, which will hold the relationship each entities and can be used efficiently for data fetch or manipulation.

5. How would you deploy this API to production? Which infrastructure would you need for that (databases, messaging, etc)?

   We can deploy the appilation to production through CI/CD pileline. Here the application may go through some initial environment stages like QA, Staging etc. Ensuring that the configuration settings, such as connection strings, API keys, and environment-specific configurations, are properly configured for the production environment. Infrastructure wise we need a application server, a database server. If we opt for cloud based deployment, we need an App service to host our API, and a SQL database service to host our database. We can store production connection string, environment specific keys on Azure key vault, azure service bus to configure asynchronous messaging. We can scale up or scale out in azure in the production based on high load.

6. How would you optimize the API endpoints to guarantee low latency under the high load?

   We can focus on the following points:

    1. Utilize asynchronous programming techniques such as async/await to handle concurrent requests efficiently.
    2. We can use tool like AppInsight in Azure to monitor the API performance.
    3. Implement caching for frequently accessed data to reduce database and external service calls.
    4. Optimize database queries utilizing efficient indexing, proper query optimization. We can reduce redundant queries by using database user defined functions.
    5. To optimize payload size, we can use Data Transfer Objects (DTOs) to send only the required data between client and server.
