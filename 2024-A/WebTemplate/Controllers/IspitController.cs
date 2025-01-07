namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class IspitController : ControllerBase
{
    public IspitContext Context { get; set; }

    public IspitController(IspitContext context)
    {
        Context = context;
    }

    [HttpPost("DodajKnjigu")]
    public async Task<ActionResult> DodajKnjigu([FromBody] Knjiga k)
    {
        await Context.Knjiga.AddAsync(k);
        await Context.SaveChangesAsync();

        return Ok($"Uspresno dodata knjiga sa id: {k.Id}");
    }

    [HttpPost("DodajBiblioteku")]
    public async Task<ActionResult> DodajBiblioteku([FromBody]Biblioteka b)
    {
        await Context.Biblioteka.AddAsync(b);
        await Context.SaveChangesAsync();

        return Ok($"Uspesno dodata biblioteka sa id: {b.Id}");
    }

    [HttpPost("PocniIzdavanje/{idKnjige}/{idBiblioteke}")]
    public async Task<ActionResult> DodajIzdavanje(int idKnjige,int idBiblioteke)
    {
        DateTime vracanje=new DateTime(3000,01,01);
        Izdavanje? i = null;

        var knjiga = await Context.Knjiga.FindAsync(idKnjige);
        var biblioteka=await Context.Biblioteka.FindAsync(idBiblioteke);

        if (knjiga == null)
            return BadRequest("Knjiga ne postoji");
        if (biblioteka == null)
            return BadRequest("Biblioteka ne postoji");

        var izdate=await Context.Izdavanje.Include(x=>x.Knjiga)
            .Where(x=>x.Knjiga!.Id==idKnjige && DateTime.Compare(x.VremeVracanja,vracanje)==0).ToListAsync();

        if (izdate != null)
            return BadRequest("Knjiga nije vracena");

        i.Knjiga = knjiga;
        i.Biblioteka = biblioteka;
        i.VremeIzdavanja=DateTime.Now;
        i.VremeVracanja = new DateTime(3000, 01, 01);

        await Context.Izdavanje.AddAsync(i);
        await Context.SaveChangesAsync();

        return Ok($"Uspesno zapoceto izdavanje sa id: {i.Id}");
    }

    [HttpPut("ZavrsiIzdavanje/{idKnjige}/{idBiblioteke}")]
    public async Task<ActionResult> ZavrsiIzdavanje(int idBiblioteke,int idKnjige)
    {


        var izdavanje = await Context.Izdavanje.Include(x => x.Knjiga)
            .Include(x => x.Biblioteka)
            .Where(x => x.Knjiga!.Id == idKnjige && x.Biblioteka!.Id == idBiblioteke).FirstOrDefaultAsync();

        if (izdavanje == null)
            return BadRequest("Izdavanje nije pocelo ili ne postoji");

        izdavanje.VremeVracanja=DateTime.Now;

        Context.Izdavanje.Update(izdavanje);
        await Context.SaveChangesAsync();

        return Ok("Izdavanje zavrseno");
    }

    [HttpGet("UkupanBrojIzdatih")]
    public async Task<ActionResult> UkupanBrojIzdatih()
    {
        DateTime vracanje = new DateTime(3000, 01, 01);
        int brojIzdatih=await Context.Izdavanje.Include(x=>x.Knjiga).Where(x=> DateTime.Compare(x.VremeVracanja, vracanje) == 0).CountAsync();

        return Ok($"Broj trenutno izdatih knjiga je: {brojIzdatih}");
    }


}
