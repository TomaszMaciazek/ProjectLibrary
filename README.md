# ProjectLibrary

API project for CRUD operations on library Database.

Used technologies:

- ASP.NET Core
- AutoMapper
- Entity Framework Core
- MsSQL database
- Jwt Token
- Authorization based on roles

Api consist of four project layers:

- Domain project contains Entities classes
- Infrastructure project contains repository interfaces and their implementations. In this project exception classes are defined. It is also responsible for database connection.
- Application project contains services interfaces and their implementations. It also contains Dto classes
- Api project uses application services to perform CRUD operations on database.
