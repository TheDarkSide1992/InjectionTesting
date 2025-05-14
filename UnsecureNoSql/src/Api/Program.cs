using Api.Controllers;
using Infastructure;

var builder = WebApplication.CreateBuilder(args);
var mongoDbContext = new MongoContext( Environment.GetEnvironmentVariable("mongoconn"), Environment.GetEnvironmentVariable("dbname"));
// Add services to the container.
builder.Services.AddSingleton<MongoUserRepo>(MongoUserRepo => new MongoUserRepo(mongoDbContext));
builder.Services.AddSingleton<UnsecureController>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
