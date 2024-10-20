using MassTransit;
using MongoDB.Driver;
using Shared;
using Stock.API.Consumer;
using Stock.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<OrderCreatedEventConsumer>();

    configurator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host(builder.Configuration["RabbitMQ"]);


        _configurator.ReceiveEndpoint(RabbitMQSettings.Stock_OrderCreatedEventQueue, e => e.ConfigureConsumer<OrderCreatedEventConsumer>(context));

    });
});

builder.Services.AddSingleton<MongoDBService>();

//using blo�u neden kulan�ld�; IServiceScope IDisposable �zeli�inde olunca bir kere �al���cak sonro yok edilecek 
//using IServiceScope scope = builder.Services.BuildServiceProvider().CreateScope();
//MongoDBService mongoDbService = scope.ServiceProvider.GetService<MongoDBService>();
//var collection = mongoDbService.GetCollection<Stock.API.Models.Entities.Stock>();

//if (!collection.FindSync(s => true).Any())
//{
//    await collection.InsertOneAsync(new() { ProductId = Guid.NewGuid(), Count = 2000 });
//    await collection.InsertOneAsync(new() { ProductId = Guid.NewGuid(), Count = 1000 });
//    await collection.InsertOneAsync(new() { ProductId = Guid.NewGuid(), Count = 3000 });
//    await collection.InsertOneAsync(new() { ProductId = Guid.NewGuid(), Count = 5000 });
//    await collection.InsertOneAsync(new() { ProductId = Guid.NewGuid(), Count = 500 });
//}

using IServiceScope scope = builder.Services.BuildServiceProvider().CreateScope();
MongoDBService mongoDbService = scope.ServiceProvider.GetService<MongoDBService>();
var collection = mongoDbService.GetCollection<Stock.API.Models.Entities.Stock>();

// Filtre tan�m� (bo� filtre: t�m kay�tlar� getirir)
var filter = Builders<Stock.API.Models.Entities.Stock>.Filter.Empty;

// FindSync kullan�m�
if (!collection.FindSync(filter).Any())
{
    await collection.InsertOneAsync(new() { ProductId = Guid.NewGuid().ToString(), Count = 2000 });
       await collection.InsertOneAsync(new() { ProductId = Guid.NewGuid().ToString(), Count = 1000 });
        await collection.InsertOneAsync(new() { ProductId = Guid.NewGuid().ToString(), Count = 3000 });
        await collection.InsertOneAsync(new() { ProductId = Guid.NewGuid().ToString(), Count = 5000 });
        await collection.InsertOneAsync(new() { ProductId = Guid.NewGuid().ToString(), Count = 500 });
}
else
{
    Console.WriteLine("Veri bulundu.");
}




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
