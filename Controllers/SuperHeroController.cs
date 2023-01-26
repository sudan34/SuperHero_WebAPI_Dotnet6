using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHeroes>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeroes>> Get (int id)
        {
            var heroes = await _context.SuperHeroes.FindAsync(id);
            if (heroes == null)
                return BadRequest("Heroes not found");
            return Ok(heroes);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHeroes>>> AddHero(SuperHeroes heroes)
        {
            _context.SuperHeroes.Add(heroes);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHeroes>>> UpdateHero(SuperHeroes request)
        {
            var dbheroes = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbheroes == null)
                return BadRequest("Heroes not found");

            dbheroes.Name = request.Name;
            dbheroes.FirstName = request.FirstName;
            dbheroes.LastName = request.LastName;
            dbheroes.Place= request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHeroes>> Delete(int id)
        {
            var dbheroes = await _context.SuperHeroes.FindAsync(id);
            if (dbheroes == null)
                return BadRequest("Heroes not found");
            _context.SuperHeroes.Remove(dbheroes);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
