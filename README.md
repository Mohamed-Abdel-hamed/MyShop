# 🛒 MyShop

A modular e‑commerce platform built with **ASP.NET Core (.NET 9)** that lets administrators manage categories, products, and orders, while customers can browse items, add them to cart, and pay securely via **Stripe**.

---

## 📁 Table of Contents
1. [🧾 Project Description](#-project-description)  
2. [✨ Features](#-features)  
3. [📦 Package Usage](#-package-usage)  
4. [🛠 Architecture Overview](#-architecture-overview)  
5. [💳 Stripe Integration](#-stripe-integration)  
6. [📊 DataTables Usage](#-datatables-usage)

---

## 🧾 Project Description
MyShop provides a full‑stack shopping experience where:
- **Admins** can create categories, upload product images, edit inventory, and track orders.  
- **Customers** can browse products, place orders, and complete payment through Stripe Checkout.  
- The admin panel uses **DataTables** (client‑side & server‑side) for fast, searchable product and order grids.

---

## ✨ Features
- **User Authentication** with ASP.NET Core Identity  
- **Role‑based Admin Panel** for managing categories, products, and orders  
- **Stripe Payments** for secure checkout and webhook handling  
- **Dynamic Tables** using DataTables for both lightweight and large datasets  
- **SQL Server or SQLite** support via Entity Framework Core  
- **Clean Architecture** with separate Application layer (`MyShop.Application`)  
- **Notifications** via Toastr and user dialogs via Bootbox  

---

## 📦 Package Usage

### 🔧 NuGet Packages (Backend)

| Package | Purpose |
|---------|---------|
| **Microsoft.AspNetCore.Identity.UI** | Supplies the ready‑made Identity UI for user registration, login, and account management. |
| **Microsoft.EntityFrameworkCore.Sqlite** | Provides SQLite support for local development or lightweight deployments. |
| **Microsoft.EntityFrameworkCore.SqlServer** | Enables SQL Server integration for staging/production environments. |
| **Microsoft.EntityFrameworkCore.Tools** | Adds EF Core CLI commands for migrations and database updates. |
| **Microsoft.VisualStudio.Web.CodeGeneration.Design** | Supports scaffolding controllers, views, and Razor pages to accelerate development. |
| **Stripe.net** | Integrates Stripe’s payment gateway for processing payments and verifying webhooks. |
| **System.Linq.Dynamic.Core** | Enables dynamic LINQ queries, powering server‑side DataTables sorting, filtering, and paging. |

### 💻 Client‑side Libraries (LibMan)

| Library | Version | Purpose |
|---------|---------|---------|
| **toastr.js** | 2.1.4 | Displays toast notifications for success, error, and info messages. |
| **bootbox.js** | 6.0.4 | Creates Bootstrap‑styled modal dialogs for confirmations and alerts. |
| **datatables** | 1.10.21 | Provides interactive tables with search, sort, pagination, and server‑side processing. |

---

## 🛠 Architecture Overview
- **Presentation Layer:** Razor Pages / MVC views served from the Web project  
- **Application Layer:** `MyShop.Application` project containing business logic and DTOs  
- **Persistence Layer:** Entity Framework Core with SQLite for dev and SQL Server for prod  
- **Authentication:** ASP.NET Core Identity with roles (`Admin`, `Customer`)  
- **Payments:** Stripe SDK handles checkout sessions and webhook events   

---

## 💳 Stripe Integration
- Create a Checkout Session on the server using **Stripe.net**.  
- Redirect customers to Stripe’s hosted payment page.  
- Handle webhook events (e.g., `checkout.session.completed`) to mark orders as **Paid**.  

---

## 📊 DataTables Usage
- **Client‑side mode:** used for small tables such as category lists for instant response.  
- **Server‑side mode:** used for larger datasets (products, orders) to offload paging and filtering to the server.  
- Server handlers leverage `System.Linq.Dynamic.Core` to build `IQueryable` filters on the fly.

