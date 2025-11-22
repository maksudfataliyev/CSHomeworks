using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetProducts()
        {
            var phones = await _context.Phones.ToListAsync();
            var cases = await _context.Cases.ToListAsync();
            var headphones = await _context.Headphones.ToListAsync();
            var cables = await _context.Cables.ToListAsync();
            var watches = await _context.Watches.ToListAsync();
            var earbuds = await _context.Earbuds.ToListAsync();

            var products = new List<object>();
            products.AddRange(phones.Select(p => new
            {
                id = p.Id,
                title = p.Title,
                price = p.Price,
                designer = p.Designer,
                color = p.Color,
                image = p.Image,
                category = p.Category,
                discount = p.Discount,
                ram = p.Ram,
                storage = p.Storage,
                year = p.Year,
                processor = p.Processor,
                camera = p.Camera,
                operatingSystem = p.OperatingSystem
            }));
            products.AddRange(cases.Select(c => new
            {
                id = c.Id,
                title = c.Title,
                price = c.Price,
                designer = c.Designer,
                color = c.Color,
                image = c.Image,
                category = c.Category,
                discount = c.Discount,
                designedFor = c.DesignedFor
            }));
            products.AddRange(headphones.Select(h => new
            {
                id = h.Id,
                title = h.Title,
                price = h.Price,
                designer = h.Designer,
                color = h.Color,
                image = h.Image,
                category = h.Category,
                discount = h.Discount
            }));
            products.AddRange(cables.Select(c => new
            {
                id = c.Id,
                title = c.Title,
                price = c.Price,
                designer = c.Designer,
                color = c.Color,
                image = c.Image,
                category = c.Category,
                discount = c.Discount,
                type = c.Type
            }));
            products.AddRange(watches.Select(w => new
            {
                id = w.Id,
                title = w.Title,
                price = w.Price,
                designer = w.Designer,
                color = w.Color,
                image = w.Image,
                category = w.Category,
                discount = w.Discount
            }));
            products.AddRange(earbuds.Select(e => new
            {
                id = e.Id,
                title = e.Title,
                price = e.Price,
                designer = e.Designer,
                color = e.Color,
                image = e.Image,
                category = e.Category,
                discount = e.Discount
            }));

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProduct(string id)
        {
            // Try each type
            var phone = await _context.Phones.FirstOrDefaultAsync(p => p.Id == id);
            if (phone != null)
            {
                return Ok(new
                {
                    id = phone.Id,
                    title = phone.Title,
                    price = phone.Price,
                    designer = phone.Designer,
                    color = phone.Color,
                    image = phone.Image,
                    category = phone.Category,
                    discount = phone.Discount,
                    ram = phone.Ram,
                    storage = phone.Storage,
                    year = phone.Year,
                    processor = phone.Processor,
                    camera = phone.Camera,
                    operatingSystem = phone.OperatingSystem
                });
            }

            var phoneCase = await _context.Cases.FirstOrDefaultAsync(p => p.Id == id);
            if (phoneCase != null)
            {
                return Ok(new
                {
                    id = phoneCase.Id,
                    title = phoneCase.Title,
                    price = phoneCase.Price,
                    designer = phoneCase.Designer,
                    color = phoneCase.Color,
                    image = phoneCase.Image,
                    category = phoneCase.Category,
                    discount = phoneCase.Discount,
                    designedFor = phoneCase.DesignedFor
                });
            }

            var headphone = await _context.Headphones.FirstOrDefaultAsync(p => p.Id == id);
            if (headphone != null)
            {
                return Ok(new
                {
                    id = headphone.Id,
                    title = headphone.Title,
                    price = headphone.Price,
                    designer = headphone.Designer,
                    color = headphone.Color,
                    image = headphone.Image,
                    category = headphone.Category,
                    discount = headphone.Discount
                });
            }

            var cable = await _context.Cables.FirstOrDefaultAsync(p => p.Id == id);
            if (cable != null)
            {
                return Ok(new
                {
                    id = cable.Id,
                    title = cable.Title,
                    price = cable.Price,
                    designer = cable.Designer,
                    color = cable.Color,
                    image = cable.Image,
                    category = cable.Category,
                    discount = cable.Discount,
                    type = cable.Type
                });
            }

            var watch = await _context.Watches.FirstOrDefaultAsync(p => p.Id == id);
            if (watch != null)
            {
                return Ok(new
                {
                    id = watch.Id,
                    title = watch.Title,
                    price = watch.Price,
                    designer = watch.Designer,
                    color = watch.Color,
                    image = watch.Image,
                    category = watch.Category,
                    discount = watch.Discount
                });
            }

            var earbud = await _context.Earbuds.FirstOrDefaultAsync(p => p.Id == id);
            if (earbud != null)
            {
                return Ok(new
                {
                    id = earbud.Id,
                    title = earbud.Title,
                    price = earbud.Price,
                    designer = earbud.Designer,
                    color = earbud.Color,
                    image = earbud.Image,
                    category = earbud.Category,
                    discount = earbud.Discount
                });
            }

            return NotFound(new { message = "Product not found" });
        }

        [HttpPost("phone")]
        public async Task<ActionResult<Phone>> AddPhone(Phone phone)
        {
            _context.Phones.Add(phone);
            await _context.SaveChangesAsync();
            return Ok(phone);
        }

        [HttpPost("case")]
        public async Task<ActionResult<Case>> AddCase(Case phoneCase)
        {
            _context.Cases.Add(phoneCase);
            await _context.SaveChangesAsync();
            return Ok(phoneCase);
        }

        [HttpPost("headphone")]
        public async Task<ActionResult<Headphone>> AddHeadphone(Headphone headphone)
        {
            _context.Headphones.Add(headphone);
            await _context.SaveChangesAsync();
            return Ok(headphone);
        }

        [HttpPost("cable")]
        public async Task<ActionResult<Cable>> AddCable(Cable cable)
        {
            _context.Cables.Add(cable);
            await _context.SaveChangesAsync();
            return Ok(cable);
        }

        [HttpPost("watch")]
        public async Task<ActionResult<Watch>> AddWatch(Watch watch)
        {
            _context.Watches.Add(watch);
            await _context.SaveChangesAsync();
            return Ok(watch);
        }

        [HttpPost("earbuds")]
        public async Task<ActionResult<Earbuds>> AddEarbuds(Earbuds earbuds)
        {
            _context.Earbuds.Add(earbuds);
            await _context.SaveChangesAsync();
            return Ok(earbuds);
        }

        [HttpPost("seed")]
        public async Task<ActionResult> SeedData()
        {

            _context.Phones.AddRange(ProductData.Phones);
            _context.Cases.AddRange(ProductData.Cases);
            _context.Headphones.AddRange(ProductData.Headphones);
            _context.Cables.AddRange(ProductData.Cables);
            _context.Watches.AddRange(ProductData.Watches);
            _context.Earbuds.AddRange(ProductData.Earbuds);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Database seeded successfully" });
        }
        // DELETE Phone
        [HttpDelete("phone/{id}")]
        public async Task<IActionResult> DeletePhone(string id)
        {
            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
                return NotFound(new { message = "Phone not found" });

            _context.Phones.Remove(phone);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Phone deleted successfully" });
        }

        // DELETE Case
        [HttpDelete("case/{id}")]
        public async Task<IActionResult> DeleteCase(string id)
        {
            var phoneCase = await _context.Cases.FindAsync(id);
            if (phoneCase == null)
                return NotFound(new { message = "Case not found" });

            _context.Cases.Remove(phoneCase);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Case deleted successfully" });
        }

        // DELETE Headphone
        [HttpDelete("headphone/{id}")]
        public async Task<IActionResult> DeleteHeadphone(string id)
        {
            var headphone = await _context.Headphones.FindAsync(id);
            if (headphone == null)
                return NotFound(new { message = "Headphone not found" });

            _context.Headphones.Remove(headphone);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Headphone deleted successfully" });
        }

        // DELETE Cable
        [HttpDelete("cable/{id}")]
        public async Task<IActionResult> DeleteCable(string id)
        {
            var cable = await _context.Cables.FindAsync(id);
            if (cable == null)
                return NotFound(new { message = "Cable not found" });

            _context.Cables.Remove(cable);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cable deleted successfully" });
        }

        // DELETE Watch
        [HttpDelete("watch/{id}")]
        public async Task<IActionResult> DeleteWatch(string id)
        {
            var watch = await _context.Watches.FindAsync(id);
            if (watch == null)
                return NotFound(new { message = "Watch not found" });

            _context.Watches.Remove(watch);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Watch deleted successfully" });
        }

        // DELETE Earbuds
        [HttpDelete("earbuds/{id}")]
        public async Task<IActionResult> DeleteEarbuds(string id)
        {
            var earbuds = await _context.Earbuds.FindAsync(id);
            if (earbuds == null)
                return NotFound(new { message = "Earbuds not found" });

            _context.Earbuds.Remove(earbuds);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Earbuds deleted successfully" });
        }

    }
}