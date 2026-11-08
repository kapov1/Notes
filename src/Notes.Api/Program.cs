var builder = WebApplication.CreateBuilder(args);

#region DI
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
    });

    builder.Services.AddHttpLogging();

    builder.Services.AddProblemDetails();

    builder.Services.AddControllers();
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");  
}
    
app.UseStatusCodePages();

app.UseHttpLogging();
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.MapGet("/error", () => Results.Problem(
    statusCode: 500,
    title: "Internal server error.",
    detail: "Something went wrong on our end. Please try again later."));

app.MapControllers();

app.Run();
