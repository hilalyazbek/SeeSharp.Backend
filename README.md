# Blog Post Management API

This repository contains a .NET API for managing blog posts. It is built using .NET 7 and follows a clean architecture pattern. The project leverages various technologies and design patterns to ensure a robust and scalable solution.


## Features

- **.NET 7:** This project is built on .NET 7.

- **Clean Architecture:** The codebase follows the principles of clean architecture, separating concerns into distinct layers to achieve maintainability and testability.

- **CQRS and Mediatr:** Command Query Responsibility Segregation (CQRS) and Mediatr are used to separate the write and read operations, making the application more scalable and flexible.

- **Docker SQL Container:** The project includes a Docker SQL container for easy database setup and management.

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

- **POST /blogposts:** Create a new blog post.
- **GET /blogposts/{id}:** Retrieve a specific blog post by ID.
- **GET /blogposts:** Retrieve a list of blog posts.
- **PUT /blogposts/{id}:** Update an existing blog post.
- **DELETE /blogposts/{id}:** Delete a blog post.


For a complete list of endpoints and their descriptions, refer to the Swagger documentation.

---

Happy coding! 🚀📝
