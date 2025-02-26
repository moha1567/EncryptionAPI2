using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Lägg till tjänster till container
builder.Services.AddEndpointsApiExplorer();  
builder.Services.AddSwaggerGen();            
var app = builder.Build();

app.MapGet("/", () => "Welcome to my Encryption API!" +
"\r\nYou can encrypt and decrypt text using the following endpoints:" +
"\r\n\r\nEncrypt: url/api/encryption/encrypt?text=<your-text>" +
"\r\nDecrypt:  url/api/encryption/decrypt?text=<your-encrypted-text>" +
"\r\nSimply replace <your-text> with the string you want to encrypt, or <your-encrypted-text> to decrypt it. Enjoy!" +
"\r\nCreated by Muhammad");

app.MapGet("add", () => "API is running!");



// Om utvecklingsmiljö
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

const int Shift = 3; // Förskjutning för Caesar-chiffer

// Krypterings- och avkrypteringsendpoints 
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

app.Run();
