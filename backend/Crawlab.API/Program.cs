using Crawlab.DB;
using Crawlab.RPC;
using Crawlab.Service;

// Create the builder with which we will build the app
var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.AddSimpleConsole(options =>
{
    options.IncludeScopes = true;
    options.SingleLine = true;
    options.TimestampFormat = "HH:mm:ss ";
});

// Add services to the container.
builder.Services.AddControllers();

// Add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database context
builder.Services.AddDbContext<CrawlabDbContext>();
builder.Services.AddSingleton<CrawlabDbContextFactory>();

// Add SignalR
builder.Services.AddSignalR();
// .AddJsonProtocol(options => { options.PayloadSerializerOptions.PropertyNamingPolicy = null; });

// Node service
builder.Services.AddHostedService<NodeService>();

// Web application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS
app.UseHttpsRedirection();

// Authentication
app.UseAuthorization();

// Controllers
app.MapControllers();

// SignalR
app.MapHub<RpcServer>("/rpc");

// Run
app.Run();