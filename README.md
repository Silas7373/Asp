# Asp
# Speise
First you have to install following 3 Packages (all in 7.0.15)

EntityFrameworkCore.Design
EntityFrameworkCore.SqLite
EntityFrameworkCore.Tools
Afterwards you create a SqLite Database Then an ApplicationDbContext File and add your Databases + Migration To make the Database complete, add following Lines in the Program.cs:

var connectionString = builder.Configuration.GetConnectionString("Name of Db"); 
builder.Services.AddDbContext(options => options.UseSqlite(connectionString));

[Route("api/[controller]")]
[ApiController]
public class SpeisenController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SpeisenController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("Fill")]
    public void FillData()
    {
        Speise s1 = new Speise { Titel = "Hauptspeise", Notizen = "Meine Notizen dazu", Sterne = 3 };
        Zutat z1 = new Zutat { Beschreibung = "Mehl", Einheit = "g", Menge = 200 };
        Zutat z2 = new Zutat { Beschreibung = "Zucker", Einheit = "g", Menge = 50 };
        Zutat z3 = new Zutat { Beschreibung = "Öl", Einheit = "ml", Menge = 100 };
        s1.Zutaten.Add(z1);
        s1.Zutaten.Add(z2);
        s1.Zutaten.Add(z3);

        _context.Speisen.Add(s1);
        _context.SaveChanges();
    }


    [HttpGet]
[Route("GetAll")]
public IActionResult GetSpeisen()
{
    var speisen = _context.Speisen.Include(x => x.Zutaten).ToList();
    var options = new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };

    var json = JsonSerializer.Serialize(speisen, options);

    return new ContentResult
    {
        Content = json,
        ContentType = "application/json",
        StatusCode = 200
    };
}
[HttpDelete("Delete")]
public async Task<IActionResult> Delete(int id)
{
    if (id == null || _context.Speisen == null)
    {
        return NotFound();
    }

    var speise = await _context.Speisen.Include(speise=>speise.Zutaten).FirstOrDefaultAsync(m=>m.Id == id);

    if (speise == null)
    {
        return NotFound();
    }
    foreach (var zutat in speise.Zutaten)
    {
        _context.Zutaten.Remove(zutat);
    }
    _context.Speisen.Remove(speise);
    await _context.SaveChangesAsync();
    return NoContent();
}
[HttpPost("Create")]
public async Task<IActionResult> Create(Speise speise)
{
    if (speise != null)
    {
        await _context.Add(speise);
    }
    return NoContent();
}
