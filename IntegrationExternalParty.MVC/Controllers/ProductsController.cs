using IntegrationExternalParty.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace IntegrationExternalParty.Controllers;

public class ProductsController : Controller
{
    // GET: Product
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Import(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var products = new List<Product>();
            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;
                for (int row = 2; row <= rowCount; row++)
                {
                    var product = new Product
                    {
                        Id = int.Parse(worksheet.Cells[row, 1].Text),
                        Name = worksheet.Cells[row, 2].Text,
                        Price = decimal.Parse(worksheet.Cells[row, 3].Text)
                    };
                    products.Add(product);
                }
            }
            // Save products to the database or process them as needed
            // For simplicity, we'll just pass the list to the view
            return View("Index", products);
        }
        return View("Index");
    }
}
