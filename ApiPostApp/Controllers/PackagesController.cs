using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPostApp.Data;
using PostApplication.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ApiPostApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly ApiPostAppContext _context;

        public PackagesController(ApiPostAppContext context)
        {
            _context = context;
        }

        // GET: api/Packages
        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> GetPackages()
        {
            var packages = await _context.Package
                .Include(p => p.Sender)
                .Include(p => p.Receiver)
                .Include(p => p.AssignedCourier)
                .Include(p => p.PostOffice)
                .ToListAsync();

            if (packages == null)
            {
                return NotFound();
            }

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var serializedPackages = JsonSerializer.Serialize(packages, jsonOptions);

            var deserializedPackage = JsonSerializer.Deserialize<JsonElement>(serializedPackages, jsonOptions);


            return Ok(deserializedPackage);
        }


        // GET: api/Packages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Package>> GetPackage(int id)
        {
            var package = _context.Package
                .Include(p => p.Sender)
                .Include(p => p.Receiver)
                .Include(p => p.AssignedCourier)
                .Include(p => p.PostOffice)
                .FirstOrDefault(p => p.ID == id);

            if (package == null)
            {
                return NotFound();
            }

            var jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var serializedPackage = JsonSerializer.Serialize(package, jsonOptions);

            var deserializedPackage = JsonSerializer.Deserialize<JsonElement>(serializedPackage, jsonOptions);

            return Ok(deserializedPackage);
        }

        // PUT: api/Packages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPackage(int id, Package package)
        {
            if (id != package.ID)
            {
                return BadRequest();
            }

            _context.Entry(package).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PackageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("{id}/updatestatus")]
        public async Task<IActionResult> UpdatePackageStatus(int id, [FromBody] string newStatus)
        {
            try
            {
                var existingPackage = await _context.Package.FindAsync(id);

                if (existingPackage == null)
                {
                    return NotFound();
                }

                existingPackage.Status = newStatus;

                _context.Entry(existingPackage).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Packages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Package>> PostPackage(Package package)
        {
            _context.Package.Add(package);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackage", new { id = package.ID }, package);
        }

        // DELETE: api/Packages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(int id)
        {
            var package = await _context.Package.FindAsync(id);
            if (package == null)
            {
                return NotFound();
            }

            _context.Package.Remove(package);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageExists(int id)
        {
            return _context.Package.Any(e => e.ID == id);
        }
    }
}
