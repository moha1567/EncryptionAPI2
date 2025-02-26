using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster till container
builder.Services.AddEndpointsApiExplorer();  // För Swagger/OpenAPI
builder.Services.AddSwaggerGen();            // För Swagger/OpenAPI

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("add", () => "API is running!");



// Om utvecklingsmiljö, visa Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

const int Shift = 3; // Förskjutning för Caesar-chiffer

// Krypterings- och avkrypteringsendpoints direkt i Program.cs
app.MapGet("/api/encryption/encrypt", (string text) =>
{
    if (string.IsNullOrEmpty(text))
        return Results.BadRequest("Ingen text angiven!");

    string encrypted = new string(text.Select(c => (char)(c + Shift)).ToArray());
    return Results.Ok(encrypted);
});

app.MapGet("/api/encryption/decrypt", (string text) =>
{
    if (string.IsNullOrEmpty(text))
        return Results.BadRequest("Ingen text angiven!");

    string decrypted = new string(text.Select(c => (char)(c - Shift)).ToArray());
    return Results.Ok(decrypted);
});

// Kör appen
app.Run();
