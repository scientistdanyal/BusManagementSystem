# Bus Management System - Project Documentation

## 📋 Project Overview

**Bus Management System** is a modern, full-stack web application designed to manage bus operations, routes, trips, passengers, and bookings. The system provides a comprehensive dashboard for real-time monitoring of bus operations with a clean, modern SaaS-style user interface.

**Project Type:** ASP.NET Core MVC Web Application  
**Target Framework:** .NET 10.0  
**Database:** SQL Server (LocalDB)  
**Architecture Pattern:** MVC (Model-View-Controller)

---

## 🏗️ Project Structure

```
BusManagementSystem/
│
├── Controllers/                    # MVC Controllers (Business Logic Layer)
│   ├── AdminController.cs         # Admin authentication & session management
│   ├── BookingsController.cs      # Booking CRUD operations
│   ├── BusesController.cs         # Bus fleet management
│   ├── BusRoutesController.cs     # Route management
│   ├── HomeController.cs          # Dashboard & landing page
│   ├── PassengersController.cs     # Passenger management
│   └── TripsController.cs         # Trip scheduling & management
│
├── Data/                           # Data Access Layer
│   └── ApplicationDbContext.cs    # Entity Framework DbContext
│
├── Models/                         # Domain Models & ViewModels
│   ├── Booking.cs                  # Booking entity model
│   ├── Bus.cs                      # Bus entity model
│   ├── BusRoute.cs                 # Route entity model
│   ├── DashboardViewModel.cs       # Dashboard data aggregation model
│   ├── ErrorViewModel.cs           # Error page model
│   ├── Passenger.cs                # Passenger entity model
│   └── Trip.cs                     # Trip entity model
│
├── Migrations/                     # Entity Framework Migrations
│   ├── 20251205182310_InitialCreate.cs
│   ├── 20251205182310_InitialCreate.Designer.cs
│   └── ApplicationDbContextModelSnapshot.cs
│
├── Views/                          # Razor Views (Presentation Layer)
│   ├── Admin/
│   │   └── Login.cshtml
│   ├── Bookings/                   # Booking views (Index, Create, Edit, Delete, Details)
│   ├── Buses/                      # Bus management views
│   ├── BusRoutes/                  # Route management views
│   ├── Home/
│   │   ├── Index.cshtml           # Main dashboard
│   │   └── Privacy.cshtml
│   ├── Passengers/                 # Passenger management views
│   ├── Routes/                     # Alternative route views
│   ├── Shared/
│   │   ├── _Layout.cshtml         # Main layout template
│   │   ├── _Layout.cshtml.css     # Layout-specific styles
│   │   ├── _ValidationScriptsPartial.cshtml
│   │   └── Error.cshtml
│   ├── Trips/                      # Trip management views
│   ├── _ViewImports.cshtml        # Global view imports
│   └── _ViewStart.cshtml          # View initialization
│
├── wwwroot/                        # Static Web Assets
│   ├── css/
│   │   └── site.css               # Custom stylesheet (modern SaaS design)
│   ├── js/
│   │   └── site.js                # Client-side JavaScript (sidebar, interactions)
│   ├── lib/                        # Third-party libraries
│   │   ├── bootstrap/             # Bootstrap 5.x
│   │   ├── jquery/                # jQuery 3.x
│   │   ├── jquery-validation/     # jQuery Validation
│   │   └── jquery-validation-unobtrusive/
│   └── favicon.ico
│
├── Properties/
│   ├── launchSettings.json        # Development launch profiles
│   └── PublishProfiles/           # Deployment profiles
│
├── appsettings.json               # Application configuration
├── appsettings.Development.json  # Development-specific settings
├── Program.cs                      # Application entry point & middleware configuration
└── BusManagementSystem.csproj    # Project file & dependencies

```

---

## 🛠️ Technologies & Tools

### **Backend Technologies**

| Technology | Version | Purpose |
|------------|---------|---------|
| **.NET** | 10.0 | Core framework |
| **ASP.NET Core MVC** | 10.0 | Web application framework |
| **C#** | Latest | Programming language |
| **Entity Framework Core** | 10.0.0 | ORM (Object-Relational Mapping) |
| **SQL Server** | LocalDB | Database engine |
| **Session Management** | Built-in | User authentication state |

### **Frontend Technologies**

| Technology | Version | Purpose |
|------------|---------|---------|
| **HTML5** | - | Markup |
| **CSS3** | - | Styling (custom SaaS design) |
| **JavaScript (ES6+)** | - | Client-side interactivity |
| **Razor Pages** | - | Server-side templating |
| **Bootstrap** | 5.x | CSS framework & components |
| **jQuery** | 3.x | DOM manipulation & AJAX |
| **jQuery Validation** | - | Form validation |

### **Development Tools**

| Tool | Purpose |
|------|---------|
| **Visual Studio / Visual Studio Code** | IDE |
| **SQL Server Management Studio (SSMS)** | Database management |
| **Git** | Version control |
| **Entity Framework Tools** | Database migrations |

---

## 📦 Libraries & Dependencies

### **NuGet Packages**

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.0" />
<PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="10.0.0-rc.1.25458.5" />
```

**Package Details:**

1. **Microsoft.EntityFrameworkCore (10.0.0)**
   - Core ORM framework for data access
   - Provides DbContext, DbSet, and LINQ query capabilities

2. **Microsoft.EntityFrameworkCore.SqlServer (10.0.0)**
   - SQL Server database provider for EF Core
   - Enables connection to SQL Server / LocalDB

3. **Microsoft.EntityFrameworkCore.Tools (10.0.0)**
   - Command-line tools for migrations
   - Enables `dotnet ef migrations add` and `dotnet ef database update`

4. **Microsoft.VisualStudio.Web.CodeGeneration.Design (10.0.0-rc.1)**
   - Scaffolding tools for generating controllers and views
   - Used for rapid CRUD generation

### **Client-Side Libraries (wwwroot/lib/)**

1. **Bootstrap 5.x**
   - CSS framework for responsive design
   - Components: buttons, forms, modals, offcanvas, grid system
   - JavaScript components: dropdowns, modals, tooltips

2. **jQuery 3.x**
   - JavaScript library for DOM manipulation
   - AJAX requests
   - Event handling

3. **jQuery Validation**
   - Client-side form validation
   - Works with ASP.NET Core model validation

4. **jQuery Validation Unobtrusive**
   - Unobtrusive validation attributes
   - Integrates with ASP.NET Core validation

---

## 🏛️ Architecture Overview

### **MVC Pattern Implementation**

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│  (Views/ - Razor Views with HTML/CSS/JavaScript)        │
└─────────────────────────────────────────────────────────┘
                          ↕
┌─────────────────────────────────────────────────────────┐
│                    Controller Layer                      │
│  (Controllers/ - Business Logic & Request Handling)      │
└─────────────────────────────────────────────────────────┘
                          ↕
┌─────────────────────────────────────────────────────────┐
│                    Model Layer                           │
│  (Models/ - Domain Entities & ViewModels)                │
└─────────────────────────────────────────────────────────┘
                          ↕
┌─────────────────────────────────────────────────────────┐
│                    Data Access Layer                     │
│  (Data/ApplicationDbContext - Entity Framework Core)     │
└─────────────────────────────────────────────────────────┘
                          ↕
┌─────────────────────────────────────────────────────────┐
│                    Database Layer                        │
│  (SQL Server LocalDB - BusManagementDb)                   │
└─────────────────────────────────────────────────────────┘
```

### **Request Flow**

1. **User Request** → Browser sends HTTP request
2. **Routing** → ASP.NET Core routing maps to Controller/Action
3. **Controller** → Processes request, queries database via DbContext
4. **Model Binding** → Binds request data to model objects
5. **View Rendering** → Controller returns View with model data
6. **Response** → Razor engine renders HTML, sends to browser

---

## 🗄️ Database Schema

### **Entity Relationships**

```
┌─────────────┐         ┌──────────────┐         ┌─────────────┐
│    Bus      │◄────────┤     Trip     ├────────►│  BusRoute    │
│             │         │              │         │             │
│ - Id        │         │ - Id         │         │ - Id        │
│ - RegNumber │         │ - BusId      │         │ - FromCity  │
│ - Capacity  │         │ - RouteId    │         │ - ToCity    │
│ - Desc      │         │ - DepartTime │         │ - Distance  │
└─────────────┘         │ - ArriveTime │         └─────────────┘
                        └──────┬───────┘
                               │
                               │
                        ┌──────▼───────┐
                        │   Booking    │
                        │              │
                        │ - Id         │
                        │ - TripId     │
                        │ - PassengerId│
                        │ - SeatNumber │
                        │ - BookingDate│
                        └──────┬───────┘
                               │
                               │
                        ┌──────▼───────┐
                        │  Passenger   │
                        │              │
                        │ - Id         │
                        │ - FullName   │
                        │ - Phone      │
                        │ - Email      │
                        └──────────────┘
```

### **Tables**

1. **Buses**
   - Stores bus fleet information
   - Fields: Id, RegistrationNumber, Capacity, Description

2. **BusRoutes**
   - Defines routes between cities
   - Fields: Id, FromCity, ToCity, DistanceKm

3. **Trips**
   - Scheduled trips linking buses to routes
   - Fields: Id, BusId, RouteId, DepartureTime, ArrivalTime
   - Foreign Keys: BusId → Buses, RouteId → BusRoutes

4. **Passengers**
   - Passenger information
   - Fields: Id, FullName, PhoneNumber, Email

5. **Bookings**
   - Seat reservations for trips
   - Fields: Id, TripId, PassengerId, SeatNumber, BookingDate
   - Foreign Keys: TripId → Trips, PassengerId → Passengers

---

## ✨ Key Features

### **1. Dashboard (Home/Index)**
- **Real-time Operations Overview**
  - Upcoming trips timeline with departure/arrival times
  - Routes overview with utilization metrics
  - Available seats per bus with color-coded indicators
  - Route-to-bus mapping with status indicators
- **Modern SaaS Design**
  - Glassmorphism effects
  - Smooth animations and transitions
  - Responsive grid layout
  - Skeleton loaders for async data

### **2. Admin Authentication**
- Session-based authentication
- Admin-only access to sensitive operations
- Secure login/logout functionality

### **3. Bus Management**
- CRUD operations for bus fleet
- Registration number tracking
- Capacity management

### **4. Route Management**
- Create and manage bus routes
- Origin and destination tracking
- Distance calculation

### **5. Trip Scheduling**
- Schedule trips with departure/arrival times
- Link buses to routes
- Admin-only access

### **6. Passenger Management**
- Register and manage passengers
- Contact information storage
- Admin-only access

### **7. Booking System**
- Create bookings for trips
- Seat number assignment
- Link passengers to trips
- Admin-only access

### **8. Modern UI/UX**
- **Persistent Sidebar Navigation**
  - Collapsible sidebar with icons + labels
  - Active state indicators
  - Mobile-responsive drawer
- **Top Bar**
  - Page title display
  - Live status indicator
  - Mobile menu toggle
- **Responsive Design**
  - Desktop, tablet, and mobile optimized
  - Touch-friendly interactions

---

## 🔐 Security Features

- **Session Management**: Secure session-based authentication
- **Admin Protection**: Admin-only routes protected via session checks
- **HTTPS Support**: Configured for secure connections
- **Input Validation**: Server-side and client-side validation
- **Anti-Forgery Tokens**: CSRF protection on forms

---

## 🚀 Deployment Configuration

### **Connection String**
```json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BusManagementDb;Trusted_Connection=True;"
```

### **Launch Profiles**
- **HTTP**: `http://localhost:5255`
- **HTTPS**: `https://localhost:7071`

### **Session Configuration**
- Timeout: 30 minutes
- HttpOnly cookies enabled
- Essential cookies marked

---

## 📊 Project Statistics

- **Controllers**: 7
- **Models**: 7 (6 entities + 1 ViewModel)
- **Views**: ~30+ Razor pages
- **Database Tables**: 5
- **Migrations**: 1 (Initial schema)

---

## 🎨 Design Philosophy

The application follows **modern SaaS design principles** (2024-2025 standards):

- **Minimalist & Clean**: Uncluttered interface with clear information hierarchy
- **Dark Theme**: Professional dark color scheme with glassmorphism
- **Micro-interactions**: Smooth hover effects, transitions, and animations
- **Accessibility**: WCAG-friendly contrast ratios and semantic HTML
- **Responsive**: Mobile-first approach with breakpoint optimization
- **Performance**: Optimized CSS, lazy loading, efficient queries

---

## 🔄 Development Workflow

1. **Database Changes**: Create migration → Update database
2. **Model Updates**: Modify Models → Update DbContext → Create migration
3. **Controller Logic**: Add business logic in Controllers
4. **View Updates**: Modify Razor views for UI changes
5. **Static Assets**: Update CSS/JS in wwwroot

---

## 📝 Notes for Supervisor

- **Production Ready**: The application is fully functional with CRUD operations for all entities
- **Scalable Architecture**: Clean separation of concerns following MVC pattern
- **Modern Stack**: Uses latest .NET 10.0 and Entity Framework Core
- **Professional UI**: Investor-ready dashboard with modern design
- **Extensible**: Easy to add new features or modify existing ones
- **Well-Structured**: Organized folder structure for maintainability

---

**Document Version:** 1.0  
**Last Updated:** December 2025  
**Project Status:** ✅ Production Ready

