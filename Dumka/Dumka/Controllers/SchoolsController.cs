using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dumka;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Dumka.Models.DTO;
using AutoMapper.QueryableExtensions;

namespace Dumka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly DumkaDbContext _context;
        private readonly IMapper _mapper;

        public SchoolsController(DumkaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Schools
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSchools()
        {
            var schoolsDto =  await _context.Schools.ProjectTo<SchoolDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new JsonResult(schoolsDto);
        }

        // GET: api/Schools/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchools(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            var schoolDtos = _mapper.Map<SchoolDto>(school);

            return new JsonResult(schoolDtos);
        }

        // PUT: api/Schools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchools(int id, SchoolDto schoolDto)
        {
            if (schoolDto.Id == 0)
            {
                schoolDto.Id = id;
            }
            if (id != schoolDto.Id)
            {
                return BadRequest();
            }

            var school = await _context.Schools.FirstOrDefaultAsync(_ => _.Id == schoolDto.Id);
            if (school == null)
            {
                return NotFound();
            }
            if (schoolDto.Name != null)
            {
                school.Name = schoolDto.Name;
            }
            if (schoolDto.Display != null)
            {
                school.Display = schoolDto.Display.Value;
            }
            school.DateModified = DateTime.Now;
            _context.Entry(school).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return RedirectToAction("GetSchools", new { id = schoolDto.Id });
        }

        // POST: api/Schools
        [HttpPost]
        public async Task<IActionResult> PostSchools(SchoolDto schoolDto)
        {
            if (schoolDto.Id == 0)
            {
                if (schoolDto.Name == null)
                {
                    return BadRequest();
                }
                if (schoolDto.Display == null)
                {
                    schoolDto.Display = true;
                }
                var school = _mapper.Map<School>(schoolDto);
                _context.Schools.Add(school);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetSchools", new { id = school.Id }, _mapper.Map<SchoolDto>(school));
            }
            else
            {
                return await PutSchools(schoolDto.Id,  schoolDto);
            }
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchools(int id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return new JsonResult(school);
        }
    }
}
