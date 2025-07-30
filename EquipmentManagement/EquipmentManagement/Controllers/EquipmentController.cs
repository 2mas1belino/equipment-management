using EquipmentManagement.Shared.Models;
using EquipmentManagement.Shared.Dtos;
using EquipmentManagement.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EquipmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/equipment
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<EquipmentDto>>> GetEquipments(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _context.Equipments.Include(e => e.AvailabilityPeriods);

            var totalItems = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<EquipmentDto>
            {
                Data = items.Select(MapToDto).ToList(),  // Using the mapping method here
                TotalCount = totalItems,
                PageNumber = page,
                PageSize = pageSize
            };
        }

        private EquipmentDto MapToDto(Equipment equipment)
        {
            return new EquipmentDto
            {
                Id = equipment.Id,
                Name = equipment.Name,
                Model = equipment.Model,
                Description = equipment.Description,
                ImageData = equipment.ImageData,
                ImageContentType = equipment.ImageContentType,
                AvailabilityPeriods = equipment.AvailabilityPeriods?
                    .Select(a => new AvailabilityPeriodDto
                    {
                        Id = a.Id,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        EquipmentId = a.EquipmentId
                    }).ToList() ?? new()
            };
        }

        // GET: api/equipment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDto>> GetEquipment(int id)
        {
            var equipment = await _context.Equipments
                .Include(e => e.AvailabilityPeriods)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (equipment == null) return NotFound();

            return new EquipmentDto
            {
                Id = equipment.Id,
                Name = equipment.Name,
                Model = equipment.Model,
                Description = equipment.Description,
                ImageData = equipment.ImageData,
                ImageContentType = equipment.ImageContentType,
                AvailabilityPeriods = equipment.AvailabilityPeriods.Select(a => new AvailabilityPeriodDto
                {
                    Id = a.Id,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    EquipmentId = a.EquipmentId
                }).ToList()
            };
        }

        // PUT: api/equipment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipment(int id, EquipmentUpdateDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            equipment.Name = updateDto.Name;
            equipment.Model = updateDto.Model;
            equipment.Description = updateDto.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // POST: api/equipment
        [HttpPost]
        public async Task<ActionResult<EquipmentDto>> PostEquipment(EquipmentCreateDto createDto)
        {
            var equipment = new Equipment
            {
                Name = createDto.Name,
                Model = createDto.Model,
                Description = createDto.Description
            };

            _context.Equipments.Add(equipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEquipment), new { id = equipment.Id },
                new EquipmentDto
                {
                    Id = equipment.Id,
                    Name = equipment.Name,
                    Model = equipment.Model,
                    Description = equipment.Description
                });
        }

        // DELETE: api/equipment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipments.Any(e => e.Id == id);
        }

        // POST api/equipment/{id}/upload-image
        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null) return NotFound();

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                equipment.ImageData = memoryStream.ToArray();
                equipment.ImageContentType = file.ContentType;
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        // GET api/equipment/{id}/image
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImage(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment?.ImageData == null)
                return NotFound();

            return File(equipment.ImageData, equipment.ImageContentType);
        }

        // DELETE api/equipment/{id}/image
        [HttpDelete("{id}/image")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var equipment = await _context.Equipments.FindAsync(id);
            if (equipment == null)
                return NotFound();

            equipment.ImageData = null;
            equipment.ImageContentType = null;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST api/equipment/{id}/availability
        [HttpPost("{equipmentId}/availability")]
        public async Task<ActionResult<AvailabilityPeriod>> AddAvailability(
            int equipmentId,
            [FromBody] AvailabilityPeriod period)
        {
            period.EquipmentId = equipmentId; // Ensure correct equipment association
            _context.AvailabilityPeriods.Add(period);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetAvailability), 
                new { equipmentId = period.EquipmentId },
                period);
        }

        // GET api/equipment/{id}/availability
        [HttpGet("{equipmentId}/availability")]
        public async Task<ActionResult<List<AvailabilityPeriod>>> GetAvailability(int equipmentId)
        {
            return await _context.AvailabilityPeriods
                .Where(a => a.EquipmentId == equipmentId)
                .OrderBy(a => a.StartDate)
                .ToListAsync();
        }

        // DELETE api/equipment/availability/{id}
        [HttpDelete("availability/{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            var period = await _context.AvailabilityPeriods.FindAsync(id);
            if (period == null) return NotFound();

            _context.AvailabilityPeriods.Remove(period);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
