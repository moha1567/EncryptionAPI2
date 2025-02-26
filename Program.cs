using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

app.MapGet("/yo", () => "Hello World!");

app.UseHttpsRedirection();

// Här definieras dina krypterings- och avkrypteringsendpoints
app.MapControllers();  // Gör att din EncryptionController och andra controllers är tillgängliga

app.Run();

// Detta är den controller som används för kryptering/avkryptering
[ApiController]
[Route("api/encryption")]
public class EncryptionController : ControllerBase
{
    private const int Shift = 3; // Förskjutning för Caesar-chiffer

    // Endpoint för att kryptera text
    [HttpGet("encrypt")]
    public IActionResult Encrypt(string text)
    {
        if (string.IsNullOrEmpty(text))
            return BadRequest("Ingen text angiven!");

        string encrypted = new string(text.Select(c => (char)(c + Shift)).ToArray());
        return Ok(encrypted);
    }

    // Endpoint för att avkryptera text
    [HttpGet("decrypt")]
    public IActionResult Decrypt(string text)
    {
        if (string.IsNullOrEmpty(text))
            return BadRequest("Ingen text angiven!");

        string decrypted = new string(text.Select(c => (char)(c - Shift)).ToArray());
        return Ok(decrypted);
    }
}
