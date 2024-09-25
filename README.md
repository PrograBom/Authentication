# Authentication Project

## Overview
This project is a robust authentication system built with ASP.NET Core, utilizing Entity Framework Core for database operations and JWT for secure token-based authentication.

## Features
- User registration and login
- Role-based authorization
- Password reset functionality
- JWT token generation and validation
- MySQL database integration

## Tech Stack
- ASP.NET Core 8.0
- Entity Framework Core
- MySQL
- JWT (JSON Web Tokens)

## Project Structure
- `Controllers/`: Contains the AuthController for handling authentication requests
- `Data/`: Includes the ApplicationDbContext for database operations
- `Migrations/`: Auto-generated EF Core migration files
- `Models/`: Defines the User and Role entities
- `Services/`: Houses custom services for the application

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- MySQL Server

### Setup
1. Clone the repository
2. Set up your MySQL database
3. Update the connection string in `appsettings.json`
4. Run EF Core migrations:
dotnet ef database update
5. Start the application:
dotnet run

## Configuration
The project uses `dotenv.net` for environment variable management. Ensure you have a `.env` file with necessary configurations.

## API Endpoints
- POST /api/auth/register: User registration
- POST /api/auth/login: User login
- POST /api/auth/reset-password: Password reset

## Security
- Passwords are hashed before storage
- JWT tokens are used for secure authentication
- Role-based access control is implemented

## Contributing
Contributions are welcome! Please feel free to submit a Pull Request.

## License
MIT
