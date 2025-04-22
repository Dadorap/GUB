# 💳 BankApp – ASP.NET Core Banking System

**Live Demo (Azure Web App)**: https://greaterbank-d5aefkezfec5d5be.swedencentral-01.azurewebsites.net/ 
**Private GitHub Repo**: https://github.com/Dadorap/GUB  
👤 **Collaborator invited**: `RichardChalk`  
📝 **F5-ready**: The project starts, migrates database (if needed), and runs error-free.

---

## 📘 About the Project

BankApp is a complete **internal banking system** built with **ASP.NET Core Razor Pages**. It is designed for **bank employees only (Cashiers and Admins)** to manage customers, accounts, transactions, and users in a secure and user-friendly way.

The system is connected to a **Database First SQL schema** provided by the course. It features full role-based access via **ASP.NET Identity**, a responsive UI using **Bootstrap templates**, and a console app for detecting suspicious activity.

> 🔒 **Note**: This system is not for customers. Only bank staff may log in.

---

## 🔑 Seeded Users

| Email                                     Password    | Role     |
|-----------------------------------------|-------------|----------|
| richard.chalk@systementor.se            | Hejsan123#  | Admin    |
| richard.chalk@customer.systementor.se   | Hejsan123#  | Cashier  |

These accounts are **automatically seeded** at application startup if they do not exist.

---

## 🚀 Features

### 👤 User & Role Management (Admin)
- Full CRUD for ASP.NET Identity users
- Assign and modify roles (`Admin`, `Cashier`)
- Prevent deletion of last Admin
- Admin-only area secured with role-based access

### 🧍 Customer & Account Management (Cashier)
- Full CRUD for customers (name, address, contact, etc.)
- Auto-generates customer number and default transaction account on creation
- Displays customer profile with all accounts and **total balance**
- Search customers by name or city with **pagination (50 per page)**
- Account view shows transaction history per account (sorted descending)
- AJAX-powered infinite scroll (20 transactions at a time)
- Deposit, withdraw, and transfer between accounts (validated)
- **Prevents overdrafts** and invalid actions
- Both **server-side and client-side validation** (Data Annotations + HTML5)

### 📈 Public Landing Page
- Available **without login**
- Displays:
  - Total number of customers
  - Number of accounts
  - Sum of all balances **grouped by country**
- Clicking a country leads to a **Top 10 richest customers** page (by total balance)
- Response-cached per country for **1 minute** for performance

### 🕵️ Suspicious Transaction Console App
- Standalone `.NET console application`
- Uses the same **Data Access Layer** as the main app (DRY)
- Checks each transaction per user and country using:
  1. Any transaction over 15,000 SEK
  2. Total sum of transactions within 72h > 23,000 SEK
- Saves `.txt` reports grouped by country to `/Reports/`
- Only new suspicious transactions are included each run

---

## 🧱 Architecture & Structure

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

