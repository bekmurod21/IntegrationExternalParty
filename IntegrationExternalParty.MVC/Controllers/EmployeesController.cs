using IntegrationExternalParty.Data.Contexts;
using IntegrationExternalParty.Service.DTOs.Employees;
using IntegrationExternalParty.Service.Interfaces;
using IntegrationExternalParty.Service.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TinyCsvParser;

namespace IntegrationExternalParty.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService service;
        private readonly AppDbContext dbContext;
        public EmployeeController(IEmployeeService service, AppDbContext dbContext)
        {
            this.service = service;
            this.dbContext = dbContext;
        }

        public ViewResult Index(string sortOrder)
        {
            var employees = service?.RetrieveAll();

            employees = sortOrder switch
            {
                "surname_asc" => employees?.OrderBy(e => e.LastName).ToList(),
                "surname_desc" => employees?.OrderByDescending(e => e.LastName).ToList(),
                // Add cases for other columns
                _ => employees?.OrderBy(e => e?.LastName)?.ToList(),// Default sorting by surname ascending
            };
            return View(employees);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {

            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file selected for upload.");
                }

                // Define the directory where you want to save the uploaded files
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "file");

                // Ensure the directory exists; create it if not
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                var uniqueFileName = Path.Combine(uploadDirectory, Path.GetRandomFileName() + Path.GetExtension(file.FileName));
                using (var stream = new FileStream(uniqueFileName, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                CsvParserOptions csvParserOptions = new(true, ',');
                CsvUserDetailsMapping csvMapper = new ();
                CsvParser<EmployeeForCreationDto> csvParser = new (csvParserOptions, csvMapper);
                var results = csvParser
                             .ReadFromFile(uniqueFileName, Encoding.ASCII)
                             .ToList();

                string[] bigstr = await System.IO.File.ReadAllLinesAsync(uniqueFileName);
                //dbContext.Add(file.FileName);

                foreach (var result in results)
                {
                    await service.AddAsync(result.Result);

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                ViewBag.Message = $"Error: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
