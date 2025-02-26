var builder = WebApplication.CreateBuilder(args);

// L�gg till tj�nster till container
builder.Services.AddControllers();  // L�gg till st�d f�r controllers (inkl. EncryptionController)
builder.Services.AddOpenApi();      // L�gg till OpenAPI-st�d (Swagger)

var app = builder.Build();

// Om utvecklingsmilj�, visa Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// S�kerst�ll att den lyssnar p� r�tt port
app.Urls.Add("http://0.0.0.0:5000");  // L�gg till den h�r raden f�r att s�kerst�lla att appen lyssnar p� port 5000

app.MapGet("/", () => "API is running!");  // En testendpoint som returnerar ett svar f�r root route

app.MapGet("/yo", () => "Hello World!");  // En testendpoint

app.UseHttpsRedirection();

// H�r definieras dina krypterings- och avkrypteringsendpoints
app.MapControllers();  // G�r att din EncryptionController och andra controllers �r tillg�ngliga

app.Run();
