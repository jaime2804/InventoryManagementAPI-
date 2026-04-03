# Inventory Management API

REST API developed with ASP.NET Core for inventory management, featuring JWT authentication, role-based authorization, and complete product and movement tracking.

## Tech Stack

- **Backend:** ASP.NET Core (.NET 10)
- **Database:** SQL Server
- **ORM:** Entity Framework Core
- **Authentication:** JWT Bearer Tokens
- **Mapping:** AutoMapper
- **Documentation:** Swagger / OpenAPI

## Features

- JWT authentication with Admin and User roles
- Full product CRUD with category management
- Inventory movement tracking (entries and exits)
- Automatic stock updates on movements
- Pagination and filtering for products and movements
- Soft delete for products
- Global error handling middleware
- Structured logging

## Architecture
```
InventarioAPI/
├── Controllers/     → HTTP endpoints, request/response handling
├── Services/        → Business logic
├── Interfaces/      → Service contracts
├── Models/          → Database entities
├── DTOs/            → Data transfer objects
├── Data/            → DbContext and database config
├── Mappings/        → AutoMapper profiles
├── Middleware/      → Global error handling
└── Enums/           → RolUsuario, TipoMovimiento
```

## Authentication & Authorization

| Role  | Permissions |
|-------|-------------|
| Admin | Full CRUD on products, categories and movements |
| User  | Read-only access to products and categories |

## API Endpoints

### Auth
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| POST | `/api/auth/register` | Public | Register new user |
| POST | `/api/auth/login` | Public | Login and get JWT token |

### Products
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| GET | `/api/product` | Authenticated | Get all products (paginated + filters) |
| GET | `/api/product/{id}` | Authenticated | Get product by ID |
| POST | `/api/product` | Admin | Create product |
| PUT | `/api/product/{id}` | Admin | Update product |
| DELETE | `/api/product/{id}` | Admin | Soft delete product |

### Categories
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| GET | `/api/category` | Authenticated | Get all categories |
| GET | `/api/category/{id}` | Authenticated | Get category by ID |
| POST | `/api/category` | Admin | Create category |

### Inventory Movements
| Method | Endpoint | Access | Description |
|--------|----------|--------|-------------|
| GET | `/api/movement` | Admin | Get all movements (paginated) |
| POST | `/api/movement` | Admin | Register stock entry or exit |

## Product Filters
```
GET /api/producto?pagina=1&tamano=10&buscar=laptop&categoriaId=1&precioMin=100&precioMax=500&stockBajo=true
```

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | Page number (default: 1) |
| size | int | Page size (default: 10, max: 50) |
| search | string | Search by name |
| categoryId | int | Filter by category |
| minPrice | decimal | Minimum price |
| maxPrice | decimal | Maximum price |
| lowStock | bool | Show only low stock products |

## Getting Started

### Prerequisites
- .NET 10 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository
```bash
git clone https://github.com/jaime2804/InventoryManagementApi-.git
cd InventarioAPI
```

2. Create `appsettings.json` based on the example
```bash
cp appsettings.example.json appsettings.json
```

3. Update the connection string and JWT settings in `appsettings.json`

4. Run database migrations
```bash
dotnet ef database update
```

5. Run the project
```bash
dotnet run
```

6. Open Swagger at `https://localhost:{port}/swagger`

## Using JWT in Swagger

1. Use `POST /api/auth/register` to create an account
2. Use `POST /api/auth/login` to get your token
3. Click **Authorize** button in Swagger
4. Enter: `Bearer {your_token}`

## Technical Decisions

- **Soft Delete** instead of hard delete to maintain data history and referential integrity
- **DTOs** to avoid exposing sensitive entity data (passwords, internal fields)
- **Interfaces** on all services for loose coupling and testability
- **AsNoTracking()** on read operations for better performance
- **Global Middleware** for consistent error responses across all endpoints
- **Enum** for roles and movement types to prevent typos and improve type safety
