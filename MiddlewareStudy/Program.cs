using MiddlewareStudy.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use((context, next) =>//Custom middleware for adding headers it take two parameter httpContext and next method
{
    context.Response.Headers.Add("X-middlewareResponse", "012345");
    return next();
});
app.UseMiddleware<Middleware>();//Add the second middle ware to request processing pipeline.

app.Run(async (context) =>//this is my third middleware to print hellow world to browser.
{
    await context.Response.WriteAsync("hello world");
});



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

