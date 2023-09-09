# Blog Post Management API

This repository contains a .NET API for managing blog posts. It is built using .NET 7 and follows a clean architecture pattern. The project leverages various technologies and design patterns to ensure a robust and scalable solution.

## Currently Being Implemented

- **Google Authentication** 🔑

## Features

- **.NET 7:** This project is built on .NET 7.

- **Clean Architecture:** The codebase follows the principles of clean architecture, separating concerns into distinct layers to achieve maintainability and testability.

- **CQRS and Mediatr:** Command Query Responsibility Segregation (CQRS) and Mediatr are used to separate the write and read operations, making the application more scalable and flexible.

- **Authentication:** The API utilizes .NET Core Identity and JWT Tokens for user authentication and authorization.

- **Logging:** Serilog is employed for comprehensive logging, allowing you to monitor and troubleshoot the application effectively.

- **Docker SQL Container:** The project includes a Docker SQL container for easy database setup and management.

- **API Versioning:** API versioning is implemented to ensure backward compatibility and smooth updates.

- **Fluent Validations:** FluentValidations are used for input validation, ensuring data integrity and security.

- **CQRS IPipelineBehavior:** Custom pipeline behaviors are employed to handle cross-cutting concerns such as validation, logging, and error handling in the CQRS pipeline.

## Getting Started

To get started with this project, follow these steps:

1.  Clone the repository to your local machine:

    bash

1.  `git clone https://github.com/hyazbek/SeeSharp.Backend.git`

1.  Open the solution in your preferred IDE (Visual Studio, Visual Studio Code, etc.).

1.  Configure your database connection in the `appsettings.json` file.

1.  Update database using `dotnet ef database update`

1.  Build and run the application.

1.  Access the API endpoints through the provided Swagger documentation or your preferred API client.

## API Endpoints

Here are some of the key API endpoints:

- **POST /api/v1/blogposts:** Create a new blog post.
- **GET /api/v1/blogposts/{id}:** Retrieve a specific blog post by ID.
- **GET /api/v1/blogposts:** Retrieve a list of blog posts.
- **PUT /api/v1/blogposts/{id}:** Update an existing blog post.
- **DELETE /api/v1/blogposts/{id}:** Delete a blog post.

- **POST /api/v1/auth/register:** Register a new user.
- **POST /api/v1/auth/login:** login endpoint.

For a complete list of endpoints and their descriptions, refer to the Swagger documentation.

---

Happy coding! 🚀📝
