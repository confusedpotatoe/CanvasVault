🎨 CanvasVault API
A Clean Architecture Digital Art Management System
CanvasVault is a robust .NET Web API built to manage digital art collections. The project implements a Clean Architecture to ensure high maintainability, testability, and a strict separation of concerns.

--------------------------------------------------------------------------------
🏛️ Architecture Overview
The solution follows the Clean Architecture pattern where dependencies point inward toward the Domain layer
. It utilizes CQRS (Command Query Responsibility Segregation) via MediatR to decouple read and write operations
.
📂 Layer Breakdown
The solution is strictly divided into four distinct projects
:
CanvasVault.Domain: The core of the system. Contains domain entities (Artwork, Collection) and interfaces for the repository pattern
.
CanvasVault.Application: The "brain" of the application. Contains all business logic, CQRS Commands/Queries, MediatR Handlers, DTO mappings, and Pipeline Behaviors
.
CanvasVault.Infrastructure: Handles external concerns. Contains the CanvasVaultDbContext for SQL Server, repository implementations, and Entity Framework migrations
.
CanvasVault.API: The entry point. Contains controllers, JWT configuration, and Swagger UI for interactive documentation
.

--------------------------------------------------------------------------------
✨ Core Features
🖼️ Data Modeling & Relationships
Entities: Implements Collection and Artwork domain models
.
Relationships: Enforces a 1-to-Many relationship where one collection can contain multiple artworks
.
Full CRUD: Complete Create, Read, Update, and Delete operations for art assets and collections
.
🛠️ Technical Design Patterns
CQRS with MediatR: Complete separation of read (Queries) and write (Commands) logic
.
Repository Pattern: Data access is decoupled from business logic using interfaces in the Domain layer and implementations in Infrastructure
.
Data Transfer Objects (DTOs): Domain entities are protected and never exposed directly. All data exchange is handled via DTOs for improved security and flexibility
.
Validation Pipeline: Uses MediatR Pipeline Behaviors to automatically validate requests before they reach the handlers
.
🛡️ Security & Authorization
JWT Authentication: Secure login flow returning a Bearer token for authenticated access
.
Role-Based Access Control (RBAC): Implements granular security with Admin and User roles to protect sensitive endpoints via role-based claims
.

--------------------------------------------------------------------------------
💻 Frontend Integration (Vite + TypeScript)
The solution includes a modern frontend client built with Vite.
API Client: Powered by Axios with a custom interceptor to automatically attach JWT Bearer tokens to outgoing requests.
Type Safety: Shared TypeScript interfaces that mirror backend DTOs to ensure data integrity across the entire stack.

--------------------------------------------------------------------------------
⚙️ Technical Setup
🗄️ Database Configuration
Update the DefaultConnection string in appsettings.json to point to your SQL Server instance
.
Apply the migrations to generate the database:
🛠️ MediatR Registration
Handlers are automatically registered in Program.cs from the Application assembly
:
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateArtworkCommand).Assembly));

--------------------------------------------------------------------------------
🛡️ Project Workflow & GitHub
To ensure high code quality and professional standards
:
Branch Protection: Direct pushes to the main branch are disabled
.
Pull Requests: All features and bug fixes are integrated via reviewed PRs
.
Clean History: Maintained with descriptive commit messages to track the project's evolution
.

--------------------------------------------------------------------------------
📊 CQRS Logic Flow
graph TD
    subgraph "API Layer"
        C[Controllers] --> M[MediatR]
    end

    subgraph "Application Layer"
        M -->|Sends| Cmd[Commands / Queries]
        Cmd --> H[Handlers]
        H -.->|Maps| D[DTOs]
    end

    subgraph "Infrastructure Layer"
        H --> R[Repository Implementation]
        R --> DB[(SQL Database / DbContext)]
    end

    subgraph "Domain Layer"
        H -.->|Uses| I[Interfaces]
        R -.->|Implements| I
        E[Entities]
    end

--------------------------------------------------------------------------------
