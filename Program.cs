using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

app.MapGet("/yo", () => "Hello World!");

app.UseHttpsRedirection();

// H�r definieras dina krypterings- och avkrypteringsendpoints
app.MapControllers();  // G�r att din EncryptionController och andra controllers �r tillg�ngliga

app.Run();

// Detta �r den controller som anv�nds f�r kryptering/avkryptering
[ApiController]
[Route("api/encryption")]
public class EncryptionController : ControllerBase
{
    private const int Shift = 3; // F�rskjutning f�r Caesar-chiffer

    // Endpoint f�r att kryptera text
    [HttpGet("encrypt")]
    public IActionResult Encrypt(string text)
    {
        if (string.IsNullOrEmpty(text))
            return BadRequest("Ingen text angiven!");

        string encrypted = new string(text.Select(c => (char)(c + Shift)).ToArray());
        return Ok(encrypted);
    }

    // Endpoint f�r att avkryptera text
    [HttpGet("decrypt")]
    public IActionResult Decrypt(string text)
    {
        if (string.IsNullOrEmpty(text))
            return BadRequest("Ingen text angiven!");

        string decrypted = new string(text.Select(c => (char)(c - Shift)).ToArray());
        return Ok(decrypted);
    }
}
