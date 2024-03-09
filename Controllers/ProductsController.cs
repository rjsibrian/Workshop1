using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppApi.Models;

namespace Controller_based_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL PRODUCTS: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
             try
            {
                var query = "SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS RowID, * FROM " +
                            "[AdventureWorks2022].[Production].[vProductAndDescription]";

                var products = await _context.Products
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (products.Count == 0)
                {
                    return NotFound();
                }

                return products;
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET PRODUCTS BY NAME: api/Products/ByName/name
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            try
            {
                var query = "SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS RowID, * " +
                            "FROM [AdventureWorks2022].[Production].[vProductAndDescription] " +
                            $"WHERE Name LIKE '%{name}%'";

                var products = await _context.Products
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (products.Count == 0)
                {
                    return NotFound();
                }

                return products;
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET PRODUCTS BY CATEGORY TYPE: api/Products/ByCategoryType/categoryType
        [HttpGet("ByCategoryType/{categoryType}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoryType(string categoryType)
        {
            try
            {
                var query = $"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS RowID, " +
                            "a.ProductID, a.Name, d.Name as ProductModel, e.CultureID , f.Description " +
                            "FROM [AdventureWorks2022].[Production].[Product] a " +
                            "INNER JOIN [AdventureWorks2022].[Production].[ProductSubcategory]  b ON a.ProductSubcategoryID = b.ProductSubcategoryID " +
                            "INNER JOIN [AdventureWorks2022].[Production].[ProductCategory] c ON b.ProductCategoryID = c.ProductCategoryID "+
                            "INNER JOIN [AdventureWorks2022].[Production].[ProductModel] d ON a.ProductModelID = d.ProductModelID " +
                            "INNER JOIN [AdventureWorks2022].[Production].[ProductModelProductDescriptionCulture] e ON d.ProductModelID = e.ProductModelID " +
                            "INNER JOIN [AdventureWorks2022].[Production].[ProductDescription] f ON f.ProductDescriptionID = e.ProductDescriptionID " +
                            $"WHERE c.Name LIKE '%{categoryType}%'";

                var products = await _context.Products
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (products == null)
                {
                    return NotFound();
                }

                return products;
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET PRODUCTS BY NAME AND CATEGORY TYPE: api/Products/ByNameAndCategoryType/name/categoryType
        [HttpGet("ByNameAndCategoryType/{name}/{categoryType}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByNameAndCategoryType(string name, string categoryType)
        {
            try
            {
                var query = $"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS RowID, " +
                            "a.ProductID, a.Name, d.Name as ProductModel, e.CultureID , f.Description " +
                            "FROM [AdventureWorks2022].[Production].[Product] a " +
                            "INNER JOIN [AdventureWorks2022].[Production].[ProductSubcategory]  b ON a.ProductSubcategoryID = b.ProductSubcategoryID " +
                            "INNER JOIN [AdventureWorks2022].[Production].[ProductCategory] c ON b.ProductCategoryID = c.ProductCategoryID "+
                            "INNER JOIN [AdventureWorks2022].[Production].[ProductModel] d ON a.ProductModelID = d.ProductModelID " +
                            "INNER JOIN [AdventureWorks2022].[Production].[ProductModelProductDescriptionCulture] e ON d.ProductModelID = e.ProductModelID " +
                            "INNER JOIN [AdventureWorks2022].[Production].[ProductDescription] f ON f.ProductDescriptionID = e.ProductDescriptionID " +
                            $"WHERE c.Name LIKE '%{categoryType}%' " +
                            "AND " +
                            $"a.Name LIKE '%{name}%'";

                var products = await _context.Products
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (products == null)
                {
                    return NotFound();
                }

                return products;
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
