# 💳 BankApp

BankApp is a complete web application for managing customers and users in a banking environment. Built with **ASP.NET Core Razor Pages**, it features full CRUD functionality for both **Customers** and **Users**, role-based access with **ASP.NET Identity**, and is styled using **Bootstrap**. The project is connected to an **Azure SQL Database** and also includes a powerful **console application** to detect suspicious transactions.

---

## 📦 Features

### 🧍 Customer Management
- Full CRUD operations
- Search, sort, and paginated listing
- Detailed customer views
- Full profile with address, contact, and national ID
- Account management (with balance, actions, and deletion)

### 👤 User Management (Admins & Cashiers)
- Full CRUD for Identity users
- Role selection on creation (`Admin`, `Cashier`)
- Edit user details and roles
- Prevent deletion of the last Admin
- Secure access via Identity with login/logout

### 🕵️ Suspicious Transaction Detection Console App
- Standalone console application included in the solution
- Scans all transactions and identifies suspicious activity based on logic you define
- Creates report files (`.txt`) grouped **per country**
- Reports saved to a local folder: `/Reports`
- Each file named after its country, e.g., `Sweden.txt`, `Germany.txt`

### 🎨 UI / Styling
- Designed using **Bootstrap 5**
- Uses **4 different admin template layouts**
- Fully responsive and clean design
- Enhanced with **Font Awesome** icons

### ☁️ Database
- Connected to a live **Azure SQL Database**
- Managed via **Entity Framework Core**
- Includes all necessary Identity tables and banking schema

---

## 🛠 Tech Stack

| Tech | Description |
|------|-------------|
| ASP.NET Core Razor Pages | Page-based web UI framework |
| ASP.NET Identity | Secure user management and roles |
| EF Core | ORM for Azure SQL |
| Bootstrap 5 | Frontend CSS framework |
| Console App (.NET) | Headless reporting tool |
| Azure SQL | Cloud-hosted relational database |

---

## 📁 Project Structure

GUB.sln                                # Solution file

├── GUB/                               # 🖥️ Main Razor Pages Web App
│   ├── Areas/                         # Identity pages (Login, Register, etc.)
│   ├── forms/                         # Extra UI components/forms
│   ├── Pages/                         # Razor Pages (Customers, Users, etc.)
│   ├── wwwroot/                       # Static files (CSS, JS, images)
│   ├── appsettings.json              # Config file (Azure SQL connection)
│   └── Program.cs                    # App startup (ASP.NET Core)

├── Common/                            # 🔧 Shared cross-cutting infrastructure
│   └── Infrastructure/               # Helpers, utilities, constants, etc.

├── DataAccessLayer/                   # 🗄️ Entity Framework Core + DB models
│   ├── Models/                       # Account, Customer, Transaction entities
│   ├── Enums/                        # Gender, CountryCode, etc.
│   ├── DTOs/                         # View-safe data transfer objects
│   └── Migrations/                   # EF Core migration history

├── Services/                          # 💼 Business Logic + APIs
│   ├── API/                          # Service interfaces
│   └── BusinessLogic/                # Service implementations (e.g. UserService)

├── ViewModels/                        # 📊 View-focused models
│   └── ViewModel/                    # CustomerViewModel, UserViewModel, etc.

├── MoneyLaundering/                   # 🕵️ Console app for suspicious transaction scanning
│   ├── Reports/                      # Output .txt reports by country
│   └── Program.cs                    # Console app logic (grouping, writing reports)

