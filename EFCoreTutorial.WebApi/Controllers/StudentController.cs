using EFCoreTutorial.Data.Context;
using EFCoreTutorial.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorial.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public StudentController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] StudentFilter filter)
        {
            var studentsQuery = applicationDbContext.Students
                .Include(s => s.Address)
                .Include(s => s.Books)
                .Include(s => s.Courses)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.FirstName))
                studentsQuery = studentsQuery.Where(s => s.FirstName == filter.FirstName);

            if (!string.IsNullOrEmpty(filter.LastName))
                studentsQuery = studentsQuery.Where(s => s.LastName == filter.LastName);

            if (filter.Number.HasValue)
                studentsQuery = studentsQuery.Where(s => s.Number == filter.Number);

            var list = await studentsQuery
                .Select(s => new
                {
                    s.Id,
                    s.FirstName,
                    s.LastName,
                    s.BirthDate,
                    s.Number,
                    Address = s.Address == null ? null : new
                    {
                        s.Address.City,
                        s.Address.Country,
                        s.Address.District,
                        s.Address.FullAddress
                    },
                    Books = s.Books.Select(b => new
                    {
                        b.Id,
                        b.Name
                    }),
                    Courses = s.Courses.Select(c => new
                    {
                        c.Id,
                        c.Name
                    })
                })
                .ToListAsync();

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            Student st = new Student()
            {
                FirstName = "Arafat",
                LastName = "Yilmaz",
                Number = 1,
                BirthDate = new DateTime(1995, 1, 1),
                Address = new StudentAddress()
                {
                    City = "İstanbul",
                    Country = "Türkiye",
                    District = "Kadıköy",
                    FullAddress = "X sokak, No: y"
                }
            };

            await applicationDbContext.Students.AddAsync(st);
            await applicationDbContext.SaveChangesAsync();

            return Ok(st);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await applicationDbContext.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            applicationDbContext.Students.Remove(student);
            await applicationDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var student = await applicationDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
                return NotFound();

            student.FirstName = "Arafat";
            student.LastName = "Yilmaz";

            await applicationDbContext.SaveChangesAsync();

            return Ok(student);
        }
    }
}
