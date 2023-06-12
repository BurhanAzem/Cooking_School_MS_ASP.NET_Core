# Cooking_School_MS_ASP.NET_Core
![Architecture ](https://github.com/BurhanAzem/Cooking_School_MS_ASP.NET_Core/assets/104472357/8f1040a7-6b42-4480-a62a-be71064aae37)
![DB-Model](https://github.com/BurhanAzem/Cooking_School_MS_ASP.NET_Core/assets/104472357/d3a0a6f0-a2ef-41cf-a314-a1bb29640203)
![trainee](https://github.com/BurhanAzem/Cooking_School_MS_ASP.NET_Core/assets/104472357/8bbd623e-9935-4ed7-be01-3437ddc2286c)
![Admin](https://github.com/BurhanAzem/Cooking_School_MS_ASP.NET_Core/assets/104472357/c57110c8-b696-4690-a866-8b358c1667cd)

Here, I will represent the most important aspects of my backend project for a Cooking School application  #ASP.NET Core Web API:

✅ Architecture used:
The architecture I implemented combines Layered Architecture and Unit Of Work & Repository. Check out the attached image for a helpful visual representation.

✅ Database Design:
- I designed the database following best practices such as normalization, indexes, and table hierarchy.
- To resolve the inheritance hierarchy, I used Table Per Hierarchy (TPH) design. TPH allows me to maintain data for all entity types in a single database table, which was the most suitable approach for my project. It offers benefits such as simplicity, overlapping properties, and improved query performance.
- I also included an Audit table with properties like Id, CreatedAt, UpdatedAt, and Deleted. All tables in the database inherit from this table, utilizing Table Per Type (TPT) for tables hierarchy.

✅ Authentication & Authorization:
- Authentication: Implemented JWT Token for authentication, building the functionality from scratch.
- Authorization: Utilized role-based authorization.
- Refresh Token: Incorporated Cookies and a refresh token table.
- Blacklist Token: Implemented a mechanism for logging out.

✅ Store and Access Files on the Cloud:
I leveraged Azure Blob Storage for uploading and accessing various types of files.

✅ Documentation & Logging:
- For testing and documenting the API, I used Swagger and Postman.
- Logging was implemented using Serilog.
- Access the API documentation here: [link to documentation](https://documenter.getpostman.com/view/22968028/2s93sdYBcB)

✅ FrontEnd:
I utilized pre-built Dashboard templates (HTML, CSS, JS) and customized them to align with the functionality of my API. Approximately 30% of the API is covered by this template. If you're a FrontEnd Developer, this is a great opportunity to take this well-documented API and build an interface for it.

✅ Technologies & Tools:
- ASP.NET Core Web API
- DBMS: Microsoft SQL Server
- Source Version Control: Git & GitHub
- ORM: Entity Framework - LINQ
- Testing & Documentation: Swagger & Postman
- Draw.io
- Editors: Visual Studio, VSC

I would love to take feedback from professionals who share a passion for backend development and the technologies used in this project.
#BackendDevelopment #CookingSchool #ASPNETCoreWebAPI #Authentication #Authorization #DatabaseDesign #CloudStorage #Documentation #Logging #FrontEndDevelopment #SoftwareEngineering
