# Coding Tracking Application

## Overview
This is a C# console-based application for tracking time-based events. Users can create, view, update, and delete events as well as view reports about the data. The application uses a local SQLite database with a rich console UI powered by Spectre.Console.

---

## Features
- CRUD operations
- Start and end time tracking
- Automatic duration calculation
- Interactive console UI
- Storage using SQLite
- Safe database access using Dapper

---

## Technology Stack
- **Language:** C#
- **Runtime:** .NET
- **Database:** SQLite
- **ORM:** Dapper
- **Console UI:** Spectre.Console

---

## Project Structure
High-level overview of folders and responsibilities.

Example:
- `Models/` – Domain models (Event, etc.)
- `Services/` – Business logic and workflows
- `Data/` – Database access and Dapper queries
- `UI/` – Console prompts and output helpers
- `Program.cs` – Application entry point

---

## Getting Started

### Prerequisites
- .NET SDK (version X.X or later)
- SQLite (optional – auto-created if not present)

---

### Installation
Steps to clone and run the application.

```bash
git clone <repo-url>
cd <project-folder>
dotnet run
