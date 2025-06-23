using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

var mongoUrlBuilder = new MongoUrlBuilder("mongodb://root:admin@localhost:27017/jobs?authSource=admin");
var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

// Add Hangfire services. Hangfire.AspNetCore nuget required
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
    {
        Prefix = "hangfire.mongo",
        CheckConnection = true,
        MigrationOptions = new MongoMigrationOptions
        {
            MigrationStrategy = new MigrateMongoMigrationStrategy(), // faz upgrade do schema!
            BackupStrategy = new CollectionMongoBackupStrategy()     // backup simples da collection
        },
        CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
    })
);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard("/Hangfire/Dashboard");

RecurringJob.AddOrUpdate("first-job", () => Console.Write("Hello World!"), "*/5 * * * *");

var options = new BackgroundJobServerOptions
{
    SchedulePollingInterval = TimeSpan.FromMinutes(1)
};

var server = new BackgroundJobServer(options);

app.Run();
