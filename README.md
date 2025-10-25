Here's a detailed and well-structured README for your Hangfire + MongoDB .NET application:

---

# 🛠 Hangfire MongoDB Job Scheduler

This project sets up a background job scheduler using [Hangfire](https://www.hangfire.io/) with [MongoDB](https://www.mongodb.com/) as the storage backend. It includes a recurring job, a dashboard for monitoring, and OpenAPI integration for development environments.

## 🚀 Features

- ⏱ Background job scheduling with Hangfire
- 🗄 MongoDB-backed job storage
- 🔁 Recurring job execution (every 5 minutes)
- 📊 Hangfire Dashboard for job monitoring
- 🔐 HTTPS redirection
- 📘 OpenAPI support for development

## 📦 Dependencies

- [.NET 6+](https://dotnet.microsoft.com/)
- [Hangfire.AspNetCore](https://www.nuget.org/packages/Hangfire.AspNetCore)
- [Hangfire.Mongo](https://www.nuget.org/packages/Hangfire.Mongo)
- [MongoDB.Driver](https://www.nuget.org/packages/MongoDB.Driver)

## 🧰 Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/hangfire-mongo-scheduler.git
cd hangfire-mongo-scheduler
```

### 2. Install Dependencies

Ensure the following NuGet packages are installed:

```bash
dotnet add package Hangfire.AspNetCore
dotnet add package Hangfire.Mongo
dotnet add package MongoDB.Driver
```

### 3. Configure MongoDB

Update the connection string if needed:

```csharp
var mongoUrlBuilder = new MongoUrlBuilder("mongodb://root:admin@localhost:27017/jobs?authSource=admin");
```

Ensure MongoDB is running locally with the appropriate credentials.

### 4. Run the Application

```bash
dotnet run
```

Access the Hangfire Dashboard at:

```
https://localhost:<port>/Hangfire/Dashboard
```

## 📅 Job Configuration

The app registers a recurring job:

```csharp
RecurringJob.AddOrUpdate("first-job", () => Console.Write("Hello World!"), "*/5 * * * *");
```

This job runs every 5 minutes and prints "Hello World!" to the console.

## 🧪 Development Mode

When running in development:

- OpenAPI documentation is available via `MapOpenApi()`
- Hangfire Dashboard is enabled

## 🧯 MongoDB Migration & Backup

The MongoDB storage is configured with:

- `MigrateMongoMigrationStrategy`: Automatically upgrades schema
- `CollectionMongoBackupStrategy`: Backs up collections before migration

## 📁 Folder Structure

```
/Program.cs         # Main application setup
/.csproj            # Project file with dependencies
```
