# 📘 GraphQLDemoApi (.NET Core + HotChocolate + SQL Server)

This is a sample project demonstrating how to build a **GraphQL API** using:
- ✅ .NET 9 Web API
- ✅ HotChocolate (GraphQL server)
- ✅ Entity Framework Core
- ✅ SQL Server (remote or local)
- ✅ Banana Cake Pop UI for GraphQL testing

---

## 📁 Features Implemented

- 🔌 Connection to remote SQL Server database (`WebLineIndia_Backup_15Nov2024`)
- 🔍 GraphQL query to fetch data from `UserLogin` table
- 🧠 GraphQL schema with a sample `hello` query and `userLogins` resolver
- 🛠 Database scaffolding via EF Core CLI
- 🍌 Banana Cake Pop UI for interactive GraphQL testing

---

## 🏗 Project Structure

GraphQLDemoApi/
│
├── Models/ # Data models (e.g., UserLogin.cs)
├── Data/ # EF DbContext
│ └── WebLineIndiaBackup15nov2024Context.cs
├── GraphQL/
│ └── Queries/ # GraphQL Query resolver
│ └── Query.cs
├── appsettings.json # Connection strings
├── Program.cs # App startup and DI config
└── GraphQLDemoApi.csproj # Project file
