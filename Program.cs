var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster till container
builder.Services.AddControllers();  // Lägg till stöd för controllers (inkl. EncryptionController)
builder.Services.AddOpenApi();      // Lägg till OpenAPI-stöd (Swagger)

var app = builder.Build();

// Om utvecklingsmiljö, visa Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Säkerställ att den lyssnar på rätt port
app.Urls.Add("http://0.0.0.0:5000");  // Lägg till den här raden för att säkerställa att appen lyssnar på port 5000

app.MapGet("/", () => "API is running!");  // En testendpoint som returnerar ett svar för root route

app.MapGet("/yo", () => "Hello World!");  // En testendpoint

app.UseHttpsRedirection();

// Här definieras dina krypterings- och avkrypteringsendpoints
app.MapControllers();  // Gör att din EncryptionController och andra controllers är tillgängliga

app.Run();
