# Social Network

## Welcome to the Social Network App backend repository! 

## This application is built with .NET 8.0 using Clean Architecture and CQRS principles.

## Features:

### Authentication and Authorization:

✔Utilizes JSON Web Tokens (JWT) for secure authentication.

✔Integrated with Microsoft Identity for user management.

✔Allow users to reset their passwords.

### Database:

✔Initially developed with Microsoft SQL Server.

✔Easily switchable to an in-memory database for development purposes (see appsettings.Development.json).

### User Interaction:

✔Upload photos and create posts with optional photos attached.

✔Comment on posts and like them.

✔Follow other users and interact with posts from followed users.

### Admin Functionality:

✔Manage user reports.

✔Deactivate user accounts and delete posts or user account as necessary.

# Admin credentials:

Email: igor@gmail.com

Password: Pa$$w0rd

## Prerequisites:

.NET 8.0 SDK

## Installation:

Clone this repository.

Open the solution in your preferred IDE.

## Database Setup:

By default, the application uses an in-memory database for development. Set UseInMemory to false in appsettings.Development.json to switch to Microsoft SQL Server.

## Configuration:

Review and modify appsettings.json and appsettings.Development.json as needed.

Ensure database connection strings are correctly configured.

## Build and Run:

Build the solution and run the application.

Navigate to https://localhost:5001/swagger/index.html to access Swagger UI for API documentation and testing or use postman. Postman collection is available.
