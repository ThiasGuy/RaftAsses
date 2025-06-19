# User Info API - .NET Core Application

## Overview

This project is a .NET Core Web API built using Visual Studio 2022. It exposes user-related endpoints through two controllers. The application fetches data from external APIs, caches the results using `IMemoryCache`, and implements resilience using `Polly`. Dependency Injection is used for all services.

## Project Architecture

├── Controllers
│ └── UserController.cs # Contains two user-related endpoints
├── Services
│ ├── UserService.cs # Business logic, communicates with HttpClientService
│ └── HttpClientService.cs # Handles external API calls using HttpClient and Polly
├── Models
│ ├── User.cs # User data model returned from external APIs
│ └── Common.cs # Common models shared across the project
├── Interfaces
│ ├── IUserService.cs # Interface for UserService
│ └── IHttpClientService.cs # Interface for HttpClientService
├── Startup.cs / Program.cs # Configures services, DI, caching, and middleware
└── README.md # Project documentation


## Features

- **.NET Core Web API** using Visual Studio 2022
- **Dependency Injection** using interfaces
- **HttpClient** for external API communication
- **Polly** for retry and failure handling
- **IMemoryCache** to cache external API responses
- **Modular Design** with separation of concerns
- **OpenAPI/Swagger** for testing endpoints (if enabled)

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Visual Studio 2022 (latest version recommended)
- Internet connection for external API calls

## Getting Started

### 1. Clone the Repository

``` bash
git clone https://github.com/ThiasGuy/RaftAsses
cd RaftAssess
dotnet restore
